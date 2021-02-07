// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class BetaDistributionTests : ContinuousDistributionTests<BetaDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(BetaDistribution.AreValidParams(d, 1));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Beta_WrongValues(double d)
        {
            Assert.False(BetaDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidBeta(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = d; });
        }

        protected override BetaDistribution GetDist(BetaDistribution other = null)
        {
            return new BetaDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaDistribution GetDist(uint seed, BetaDistribution other = null)
        {
            return new BetaDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaDistribution GetDist(IGenerator gen, BetaDistribution other = null)
        {
            return new BetaDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaDistribution GetDistWithParams(BetaDistribution other = null)
        {
            return new BetaDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override BetaDistribution GetDistWithParams(uint seed, BetaDistribution other = null)
        {
            return new BetaDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override BetaDistribution GetDistWithParams(IGenerator gen, BetaDistribution other = null)
        {
            return new BetaDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha > 0
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(10) : d.Alpha;
        }

        // beta > 0
        private double GetBeta(IBetaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(10) : d.Beta;
        }
    }
}
