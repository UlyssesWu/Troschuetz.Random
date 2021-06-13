
#r "nuget: Bullseye, 3.7.0"
#r "nuget: CommandLineParser, 2.8.0"
#r "nuget: SimpleExec, 7.0.0"

using System.IO;
using CommandLine;
using static Bullseye.Targets;
using static SimpleExec.Command;

////////////////////////////////////////////////////////////////////////////////
// PREPARATION
////////////////////////////////////////////////////////////////////////////////

var artifactsDir = "./artifacts";
var nugetPackagesDir = $"./{artifactsDir}/nuget-packages";
var testResultsDir = $"./{artifactsDir}/test-results";
var codeCoverageDir = $"./{artifactsDir}/code-coverage";

////////////////////////////////////////////////////////////////////////////////
// OPTIONS
////////////////////////////////////////////////////////////////////////////////

public sealed class Options
{
    [Option('c', "configuration", Required = false, Default = "release")]
    public string Configuration { get; set; }

    public string ConfigurationFlag => !string.IsNullOrWhiteSpace(Configuration) ? $"--configuration {Configuration}" : string.Empty;

    [Option('f', "framework", Required = false, Default = "")]
    public string Framework { get; set; }

    public string FrameworkFlag => !string.IsNullOrWhiteSpace(Framework) ? $"--framework {Framework}" : string.Empty;

    [Option('t', "target", Required = false, Default = "run-tests")]
    public string Target { get; set; }
}

Options opts;

////////////////////////////////////////////////////////////////////////////////
// TARGETS
////////////////////////////////////////////////////////////////////////////////

Target("clean-solution", () =>
{
    Run("dotnet", $"clean {opts.ConfigurationFlag}");

    if (Directory.Exists(artifactsDir))
    {
        Directory.Delete(artifactsDir, recursive: true);
    }
});

Target("check-source-code-style", DependsOn("clean-solution"), () =>
{
    Run("dotnet", "format --check --verbosity normal");
});

Target("restore-nuget-packages", DependsOn("check-source-code-style"), () =>
{
    Run("dotnet", $"restore");
});

Target("build-solution", DependsOn("restore-nuget-packages"), () =>
{
    Run("dotnet", $"build {opts.ConfigurationFlag} --no-restore");
});

Target("run-tests", DependsOn("build-solution"), () =>
{
    var loggerFlag = $"--logger \"junit;LogFileName={{assembly}}-{{framework}}.xml;MethodFormat=Class;FailureBodyFormat=Verbose\"";
    var codeCoverageFlags = $"--collect \"XPlat Code Coverage\" --results-directory {testResultsDir}";
    Run("dotnet", $"test {opts.ConfigurationFlag} {opts.FrameworkFlag} --test-adapter-path . {loggerFlag} {codeCoverageFlags} --no-build");
    Run("dotnet", $"reportgenerator \"-reports:{testResultsDir}/*/*.xml\" \"-targetdir:{codeCoverageDir}\" -reporttypes:Html");
});

Target("pack-sources", DependsOn("build-solution"), () =>
{
    Run("dotnet", $"pack {opts.ConfigurationFlag} --output {nugetPackagesDir} --no-build");
});

Target("publish-on-myget", DependsOn("pack-sources"), () =>
{
    var mygetApiKey = Environment.GetEnvironmentVariable("MYGET_API_KEY");
    Run("dotnet", $"nuget push {nugetPackagesDir}/*.nupkg --api-key {mygetApiKey}");
});

////////////////////////////////////////////////////////////////////////////////
// EXECUTION
////////////////////////////////////////////////////////////////////////////////

Parser.Default.ParseArguments<Options>(Args).WithParsed<Options>(o =>
{
    opts = o;
    RunTargetsAndExit(new[] { opts.Target });
});
