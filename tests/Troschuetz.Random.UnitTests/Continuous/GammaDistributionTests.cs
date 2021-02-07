// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class GammaDistributionTests : ContinuousDistributionTests<GammaDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(GammaDistribution.AreValidParams(d, 1));
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
            Assert.False(GammaDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidBeta(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = d; });
        }

        protected override GammaDistribution GetDist(GammaDistribution other = null)
        {
            return new GammaDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override GammaDistribution GetDist(uint seed, GammaDistribution other = null)
        {
            return new GammaDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override GammaDistribution GetDist(IGenerator gen, GammaDistribution other = null)
        {
            return new GammaDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override GammaDistribution GetDistWithParams(GammaDistribution other = null)
        {
            return new GammaDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override GammaDistribution GetDistWithParams(uint seed, GammaDistribution other = null)
        {
            return new GammaDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override GammaDistribution GetDistWithParams(IGenerator gen, GammaDistribution other = null)
        {
            return new GammaDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha > 0
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Alpha;
        }

        // beta > 0
        private double GetBeta(IBetaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Beta;
        }
    }
}
