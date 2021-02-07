// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class CategoricalDistributionTests : DiscreteDistributionTests<CategoricalDistribution>
    {
        [Test]
        public void Median_EvenEquiWeights()
        {
            _dist = new CategoricalDistribution(new double[] { 1, 1, 1, 1, 1, 1 });
            for (var i = 0; i < Iterations; ++i)
                Results[i] = _dist.Next();
            AssertDist(_dist);
        }

        [Test]
        public void Median_OddEquiWeights()
        {
            _dist = new CategoricalDistribution(new double[] { 1, 1, 1, 1, 1 });
            for (var i = 0; i < Iterations; ++i)
                Results[i] = _dist.Next();
            AssertDist(_dist);
        }

        [TestCase(double.NaN, 1, 2)]
        [TestCase(SmallNeg, 1, 2)]
        [TestCase(1, LargeNeg, 2)]
        [TestCase(1, 2, TinyNeg)]
        [TestCase(0, -2, 2)]
        [TestCase(-3, -2, -1)]
        public void Weights_WrongValues(double d1, double d2, double d3)
        {
            var w = new List<double> { d1, d2, d3 };
            Assert.False(CategoricalDistribution.IsValidParam(w));
            Assert.False(_dist.AreValidWeights(w));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Weights = w; });
        }

        protected override CategoricalDistribution GetDist(CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(GetValueCount(other));
        }

        protected override CategoricalDistribution GetDist(uint seed, CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(seed, GetValueCount(other));
        }

        protected override CategoricalDistribution GetDist(IGenerator gen, CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(gen, GetValueCount(other));
        }

        protected override CategoricalDistribution GetDistWithParams(CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(GetWeights(other));
        }

        protected override CategoricalDistribution GetDistWithParams(uint seed, CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(seed, GetWeights(other));
        }

        protected override CategoricalDistribution GetDistWithParams(IGenerator gen, CategoricalDistribution other = null)
        {
            return new CategoricalDistribution(gen, GetWeights(other));
        }

        // value count > 0
        private int GetValueCount(IWeightsDistribution<double> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Weights.Count();
        }

        // all weights > 0
        private ICollection<double> GetWeights(IWeightsDistribution<double> d)
        {
            double R() => Rand.NextDouble(0.1, 10);
            return d == null ? new List<double> { R(), R(), R(), R(), R() } : d.Weights;
        }
    }
}
