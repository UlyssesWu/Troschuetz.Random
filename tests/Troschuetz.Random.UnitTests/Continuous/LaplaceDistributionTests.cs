// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class LaplaceDistributionTests : ContinuousDistributionTests<LaplaceDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(LaplaceDistribution.AreValidParams(d, 1));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        protected override LaplaceDistribution GetDist(LaplaceDistribution other = null)
        {
            return new LaplaceDistribution { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override LaplaceDistribution GetDist(uint seed, LaplaceDistribution other = null)
        {
            return new LaplaceDistribution(seed) { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override LaplaceDistribution GetDist(IGenerator gen, LaplaceDistribution other = null)
        {
            return new LaplaceDistribution(gen) { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override LaplaceDistribution GetDistWithParams(LaplaceDistribution other = null)
        {
            return new LaplaceDistribution(GetAlpha(other), GetMu(other));
        }

        protected override LaplaceDistribution GetDistWithParams(uint seed, LaplaceDistribution other = null)
        {
            return new LaplaceDistribution(seed, GetAlpha(other), GetMu(other));
        }

        protected override LaplaceDistribution GetDistWithParams(IGenerator gen, LaplaceDistribution other = null)
        {
            return new LaplaceDistribution(gen, GetAlpha(other), GetMu(other));
        }

        // alpha > 0
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Alpha;
        }

        // any value
        private double GetMu(IMuDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Mu;
        }
    }
}
