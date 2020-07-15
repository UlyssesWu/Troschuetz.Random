#!/bin/sh

set -e

# Restore .NET tools:
dotnet tool restore

# Run build script:
dotnet script build.csx -- $@
