// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class FisherTippettDistributionTests : ContinuousDistributionTests<FisherTippettDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(FisherTippettDistribution.AreValidParams(d, 1));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        protected override FisherTippettDistribution GetDist(FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override FisherTippettDistribution GetDist(uint seed, FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution(seed) { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override FisherTippettDistribution GetDist(IGenerator gen, FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution(gen) { Alpha = GetAlpha(other), Mu = GetMu(other) };
        }

        protected override FisherTippettDistribution GetDistWithParams(FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution(GetAlpha(other), GetMu(other));
        }

        protected override FisherTippettDistribution GetDistWithParams(uint seed, FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution(seed, GetAlpha(other), GetMu(other));
        }

        protected override FisherTippettDistribution GetDistWithParams(IGenerator gen, FisherTippettDistribution other = null)
        {
            return new FisherTippettDistribution(gen, GetAlpha(other), GetMu(other));
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
