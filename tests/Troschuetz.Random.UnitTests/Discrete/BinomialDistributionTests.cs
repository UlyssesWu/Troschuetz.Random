// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class BinomialDistributionTests : DiscreteDistributionTests<BinomialDistribution>
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
            Assert.False(BinomialDistribution.AreValidParams(d, 1));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        [TestCase(double.NaN)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Beta_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(BinomialDistribution.AreValidParams(0.5, i));
            Assert.False(_dist.IsValidBeta(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = i; });
        }

        protected override BinomialDistribution GetDist(BinomialDistribution other = null)
        {
            return new BinomialDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BinomialDistribution GetDist(uint seed, BinomialDistribution other = null)
        {
            return new BinomialDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BinomialDistribution GetDist(IGenerator gen, BinomialDistribution other = null)
        {
            return new BinomialDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BinomialDistribution GetDistWithParams(BinomialDistribution other = null)
        {
            return new BinomialDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override BinomialDistribution GetDistWithParams(uint seed, BinomialDistribution other = null)
        {
            return new BinomialDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override BinomialDistribution GetDistWithParams(IGenerator gen, BinomialDistribution other = null)
        {
            return new BinomialDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha >= 0 && alpha <= 1
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0, 1) : d.Alpha;
        }

        // beta >= 0
        private int GetBeta(IBetaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Beta;
        }
    }
}
