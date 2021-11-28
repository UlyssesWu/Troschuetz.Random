// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Troschuetz.Random.Distributions.Continuous;

namespace Troschuetz.Random.Benchmarks
{
    [Config(typeof(Program.Config))]
    public class ContinuousDistributionComparison : AbstractComparison
    {
        private readonly Dictionary<string, Dictionary<string, IContinuousDistribution>> _distributions = new Dictionary<string, Dictionary<string, IContinuousDistribution>>
        {
            [N<BetaDistribution>()] = Generators.ToDictionary(x => x.Key, x => new BetaDistribution(x.Value) as IContinuousDistribution),
            [N<BetaPrimeDistribution>()] = Generators.ToDictionary(x => x.Key, x => new BetaPrimeDistribution(x.Value) as IContinuousDistribution),
            [N<CauchyDistribution>()] = Generators.ToDictionary(x => x.Key, x => new CauchyDistribution(x.Value) as IContinuousDistribution),
            [N<ChiDistribution>()] = Generators.ToDictionary(x => x.Key, x => new ChiDistribution(x.Value) as IContinuousDistribution),
            [N<ChiSquareDistribution>()] = Generators.ToDictionary(x => x.Key, x => new ChiSquareDistribution(x.Value) as IContinuousDistribution),
            [N<ContinuousUniformDistribution>()] = Generators.ToDictionary(x => x.Key, x => new ContinuousUniformDistribution(x.Value) as IContinuousDistribution),
            [N<ExponentialDistribution>()] = Generators.ToDictionary(x => x.Key, x => new ExponentialDistribution(x.Value) as IContinuousDistribution),
            [N<NormalDistribution>()] = Generators.ToDictionary(x => x.Key, x => new NormalDistribution(x.Value) as IContinuousDistribution),
        };

        [Params("Beta", "BetaPrime", "Cauchy", "Chi", "ChiSquare", "ContinuousUniform", "Exponential", "Normal")]
        public string Distribution { get; set; }

        [Benchmark]
        public double NextDouble() => _distributions[Distribution][Generator].NextDouble();
    }
}
