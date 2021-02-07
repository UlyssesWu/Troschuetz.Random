// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class BernoulliDistributionTests : DiscreteDistributionTests<BernoulliDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        [TestCase(1 + TinyPos)]
        [TestCase(1 + SmallPos)]
        [TestCase(1 + LargePos)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(BernoulliDistribution.IsValidParam(d));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        protected override BernoulliDistribution GetDist(BernoulliDistribution other = null)
        {
            return new BernoulliDistribution { Alpha = GetAlpha(other) };
        }

        protected override BernoulliDistribution GetDist(uint seed, BernoulliDistribution other = null)
        {
            return new BernoulliDistribution(seed) { Alpha = GetAlpha(other) };
        }

        protected override BernoulliDistribution GetDist(IGenerator gen, BernoulliDistribution other = null)
        {
            return new BernoulliDistribution(gen) { Alpha = GetAlpha(other) };
        }

        protected override BernoulliDistribution GetDistWithParams(BernoulliDistribution other = null)
        {
            return new BernoulliDistribution(GetAlpha(other));
        }

        protected override BernoulliDistribution GetDistWithParams(uint seed, BernoulliDistribution other = null)
        {
            return new BernoulliDistribution(seed, GetAlpha(other));
        }

        protected override BernoulliDistribution GetDistWithParams(IGenerator gen, BernoulliDistribution other = null)
        {
            return new BernoulliDistribution(gen, GetAlpha(other));
        }

        // alpha >= 0.0 && alpha <= 1.0
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0, 1) : d.Alpha;
        }
    }
}
