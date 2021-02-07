// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Troschuetz.Random.Benchmarks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(GeneratorComparison),
                typeof(DiscreteDistributionComparison),
                typeof(ContinuousDistributionComparison)
            });
            switcher.Run(args);
        }

        public class Config : ManualConfig
        {
            public Config()
            {
                AddJob(Job.RyuJitX64);
                AddExporter(CsvExporter.Default, HtmlExporter.Default, MarkdownExporter.GitHub, PlainExporter.Default, CsvMeasurementsExporter.Default, RPlotExporter.Default);
                AddDiagnoser(MemoryDiagnoser.Default);
                AddAnalyser(EnvironmentAnalyser.Default);
            }
        }
    }
}
