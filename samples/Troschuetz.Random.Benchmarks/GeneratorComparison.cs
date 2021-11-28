// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using BenchmarkDotNet.Attributes;

namespace Troschuetz.Random.Benchmarks
{
    [Config(typeof(Program.Config))]
    public class GeneratorComparison : AbstractComparison
    {
        private readonly byte[] _bytes128 = new byte[128];
        private readonly byte[] _bytes64 = new byte[64];

        [Benchmark]
        public bool NextBoolean() => Generators[Generator].NextBoolean();

        [Benchmark]
        public void NextBytes128() => Generators[Generator].NextBytes(_bytes128);

        [Benchmark]
        public void NextBytes64() => Generators[Generator].NextBytes(_bytes64);

        [Benchmark]
        public double NextDouble() => Generators[Generator].NextDouble();

        [Benchmark]
        public uint NextUInt() => Generators[Generator].NextUInt();
    }
}
