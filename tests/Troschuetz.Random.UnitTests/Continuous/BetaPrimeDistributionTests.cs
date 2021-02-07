// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class BetaPrimeDistributionTests : ContinuousDistributionTests<BetaPrimeDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(1 + TinyNeg)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(BetaPrimeDistribution.AreValidParams(d, 2));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        [TestCase(double.NaN)]
        [TestCase(1 + TinyNeg)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Beta_WrongValues(double d)
        {
            Assert.False(BetaPrimeDistribution.AreValidParams(2, d));
            Assert.False(_dist.IsValidBeta(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = d; });
        }

        protected override BetaPrimeDistribution GetDist(BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaPrimeDistribution GetDist(uint seed, BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaPrimeDistribution GetDist(IGenerator gen, BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override BetaPrimeDistribution GetDistWithParams(BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override BetaPrimeDistribution GetDistWithParams(uint seed, BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override BetaPrimeDistribution GetDistWithParams(IGenerator gen, BetaPrimeDistribution other = null)
        {
            return new BetaPrimeDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha > 1
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(1.1, 10) : d.Alpha;
        }

        // beta > 1
        private double GetBeta(IBetaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(1.1, 10) : d.Beta;
        }
    }
}
