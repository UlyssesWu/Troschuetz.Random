// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class PowerDistributionTests : ContinuousDistributionTests<PowerDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(PowerDistribution.AreValidParams(d, 1));
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
            Assert.False(PowerDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidBeta(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = d; });
        }

        protected override PowerDistribution GetDist(PowerDistribution other = null)
        {
            return new PowerDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override PowerDistribution GetDist(uint seed, PowerDistribution other = null)
        {
            return new PowerDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override PowerDistribution GetDist(IGenerator gen, PowerDistribution other = null)
        {
            return new PowerDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override PowerDistribution GetDistWithParams(PowerDistribution other = null)
        {
            return new PowerDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override PowerDistribution GetDistWithParams(uint seed, PowerDistribution other = null)
        {
            return new PowerDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override PowerDistribution GetDistWithParams(IGenerator gen, PowerDistribution other = null)
        {
            return new PowerDistribution(gen, GetAlpha(other), GetBeta(other));
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
