$ErrorActionPreference = "Stop";

# Restore .NET tools:
& dotnet tool restore

# Run build script:
& dotnet script build.csx -- $args
