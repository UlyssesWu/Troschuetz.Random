
#r "nuget: Bullseye, 3.5.0"
#r "nuget: CommandLineParser, 2.8.0"
#r "nuget: SimpleExec, 6.3.0"

using System.IO;
using CommandLine;
using static Bullseye.Targets;
using static SimpleExec.Command;

////////////////////////////////////////////////////////////////////////////////
// PREPARATION
////////////////////////////////////////////////////////////////////////////////

var artifactsDir = "./artifacts";
var nugetPackagesDir = $"./{artifactsDir}/nuget-packages";
var testResultsDir = $"../../{artifactsDir}/test-results";

////////////////////////////////////////////////////////////////////////////////
// OPTIONS
////////////////////////////////////////////////////////////////////////////////

public sealed class Options
{
    [Option('c', "configuration", Required = false, Default = "release")]
    public string Configuration { get; set; }

    public string ConfigurationFlag => !string.IsNullOrWhiteSpace(Configuration) ? $"-c {Configuration}" : string.Empty;

    [Option('t', "target", Required = false, Default = "run-unit-tests")]
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
    Run("dotnet", "format --check --verbosity detailed");
});

Target("restore-nuget-packages", DependsOn("check-source-code-style"), () =>
{
    Run("dotnet", $"restore");
});

Target("build-solution", DependsOn("restore-nuget-packages"), () =>
{
    Run("dotnet", $"build {opts.ConfigurationFlag} --no-restore");
});

Target("run-unit-tests", DependsOn("build-solution"), () =>
{
    var logger = $"junit;LogFilePath={testResultsDir}/{{assembly}}-{{framework}}.xml";
    Run("dotnet", $"test {opts.ConfigurationFlag} -a . -l {logger} --no-build");
});

Target("pack-sources", DependsOn("build-solution"), () =>
{
    Run("dotnet", $"pack {opts.ConfigurationFlag} -o {nugetPackagesDir} --no-build");
});

Target("publish-on-myget", DependsOn("pack-sources"), () =>
{
    var mygetApiKey = Environment.GetEnvironmentVariable("MYGET_API_KEY");
    Run("dotnet", $"nuget push {nugetPackagesDir}/*.nupkg -k {mygetApiKey}");
});

////////////////////////////////////////////////////////////////////////////////
// EXECUTION
////////////////////////////////////////////////////////////////////////////////

Parser.Default.ParseArguments<Options>(Args).WithParsed<Options>(o =>
{
    opts = o;
    RunTargetsAndExit(new[] { opts.Target });
});
