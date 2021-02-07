// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ParetoDistributionTests : ContinuousDistributionTests<ParetoDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(ParetoDistribution.AreValidParams(d, 1));
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
            Assert.False(ParetoDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidBeta(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = d; });
        }

        protected override ParetoDistribution GetDist(ParetoDistribution other = null)
        {
            return new ParetoDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override ParetoDistribution GetDist(uint seed, ParetoDistribution other = null)
        {
            return new ParetoDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override ParetoDistribution GetDist(IGenerator gen, ParetoDistribution other = null)
        {
            return new ParetoDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override ParetoDistribution GetDistWithParams(ParetoDistribution other = null)
        {
            return new ParetoDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override ParetoDistribution GetDistWithParams(uint seed, ParetoDistribution other = null)
        {
            return new ParetoDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override ParetoDistribution GetDistWithParams(IGenerator gen, ParetoDistribution other = null)
        {
            return new ParetoDistribution(gen, GetAlpha(other), GetBeta(other));
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
