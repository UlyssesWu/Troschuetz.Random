// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class LognormalDistributionTests : ContinuousDistributionTests<LognormalDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Sigma_WrongValues(double d)
        {
            Assert.False(LognormalDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidSigma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Sigma = d; });
        }

        protected override LognormalDistribution GetDist(LognormalDistribution other = null)
        {
            return new LognormalDistribution { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDist(uint seed, LognormalDistribution other = null)
        {
            return new LognormalDistribution(seed) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDist(IGenerator gen, LognormalDistribution other = null)
        {
            return new LognormalDistribution(gen) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDistWithParams(LognormalDistribution other = null)
        {
            return new LognormalDistribution(GetMu(other), GetSigma(other));
        }

        protected override LognormalDistribution GetDistWithParams(uint seed, LognormalDistribution other = null)
        {
            return new LognormalDistribution(seed, GetMu(other), GetSigma(other));
        }

        protected override LognormalDistribution GetDistWithParams(IGenerator gen, LognormalDistribution other = null)
        {
            return new LognormalDistribution(gen, GetMu(other), GetSigma(other));
        }

        // any value
        private double GetMu(IMuDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Mu;
        }

        // sigma >= 0, better keep it low
        private double GetSigma(ISigmaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble() : d.Sigma;
        }
    }
}
