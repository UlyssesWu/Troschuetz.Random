// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class CauchyDistributionTests : ContinuousDistributionTests<CauchyDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Gamma_WrongValues(double d)
        {
            Assert.False(CauchyDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidGamma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Gamma = d; });
        }

        protected override CauchyDistribution GetDist(CauchyDistribution other = null)
        {
            return new CauchyDistribution { Alpha = GetAlpha(other), Gamma = GetGamma(other) };
        }

        protected override CauchyDistribution GetDist(uint seed, CauchyDistribution other = null)
        {
            return new CauchyDistribution(seed) { Alpha = GetAlpha(other), Gamma = GetGamma(other) };
        }

        protected override CauchyDistribution GetDist(IGenerator gen, CauchyDistribution other = null)
        {
            return new CauchyDistribution(gen) { Alpha = GetAlpha(other), Gamma = GetGamma(other) };
        }

        protected override CauchyDistribution GetDistWithParams(CauchyDistribution other = null)
        {
            return new CauchyDistribution(GetAlpha(other), GetGamma(other));
        }

        protected override CauchyDistribution GetDistWithParams(uint seed, CauchyDistribution other = null)
        {
            return new CauchyDistribution(seed, GetAlpha(other), GetGamma(other));
        }

        protected override CauchyDistribution GetDistWithParams(IGenerator gen, CauchyDistribution other = null)
        {
            return new CauchyDistribution(gen, GetAlpha(other), GetGamma(other));
        }

        // any value
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Alpha;
        }

        // gamma > 0
        private double GetGamma(IGammaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Gamma;
        }
    }
}
