// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class NormalDistributionTests : ContinuousDistributionTests<NormalDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Sigma_WrongValues(double d)
        {
            Assert.False(NormalDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidSigma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Sigma = d; });
        }

        protected override NormalDistribution GetDist(NormalDistribution other = null)
        {
            return new NormalDistribution { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override NormalDistribution GetDist(uint seed, NormalDistribution other = null)
        {
            return new NormalDistribution(seed) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override NormalDistribution GetDist(IGenerator gen, NormalDistribution other = null)
        {
            return new NormalDistribution(gen) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override NormalDistribution GetDistWithParams(NormalDistribution other = null)
        {
            return new NormalDistribution(GetMu(other), GetSigma(other));
        }

        protected override NormalDistribution GetDistWithParams(uint seed, NormalDistribution other = null)
        {
            return new NormalDistribution(seed, GetMu(other), GetSigma(other));
        }

        protected override NormalDistribution GetDistWithParams(IGenerator gen, NormalDistribution other = null)
        {
            return new NormalDistribution(gen, GetMu(other), GetSigma(other));
        }

        // any value
        private double GetMu(IMuDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Mu;
        }

        // sigma > 0, better keep it low
        private double GetSigma(ISigmaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 1) : d.Sigma;
        }
    }
}
