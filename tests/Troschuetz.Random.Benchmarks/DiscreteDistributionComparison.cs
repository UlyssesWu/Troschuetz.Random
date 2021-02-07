// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Troschuetz.Random.Distributions.Discrete;

namespace Troschuetz.Random.Benchmarks
{
    [Config(typeof(Program.Config))]
    public class DiscreteDistributionComparison : AbstractComparison
    {
        private readonly Dictionary<string, Dictionary<string, IDiscreteDistribution>> _distributions = new Dictionary<string, Dictionary<string, IDiscreteDistribution>>
        {
            [N<BernoulliDistribution>()] = Generators.ToDictionary(x => x.Key, x => new BernoulliDistribution(x.Value) as IDiscreteDistribution),
            [N<BinomialDistribution>()] = Generators.ToDictionary(x => x.Key, x => new BinomialDistribution(x.Value) as IDiscreteDistribution),
            [N<CategoricalDistribution>()] = Generators.ToDictionary(x => x.Key, x => new CategoricalDistribution(x.Value) as IDiscreteDistribution),
            [N<DiscreteUniformDistribution>()] = Generators.ToDictionary(x => x.Key, x => new DiscreteUniformDistribution(x.Value) as IDiscreteDistribution),
            [N<GeometricDistribution>()] = Generators.ToDictionary(x => x.Key, x => new GeometricDistribution(x.Value) as IDiscreteDistribution),
            [N<PoissonDistribution>()] = Generators.ToDictionary(x => x.Key, x => new PoissonDistribution(x.Value) as IDiscreteDistribution),
        };

        [Params("Bernoulli", "Binomial", "Categorical", "DiscreteUniform", "Geometric", "Poisson")]
        public string Distribution { get; set; }

        [Benchmark]
        public int Next() => _distributions[Distribution][Generator].Next();

        [Benchmark]
        public double NextDouble() => _distributions[Distribution][Generator].NextDouble();
    }
}
