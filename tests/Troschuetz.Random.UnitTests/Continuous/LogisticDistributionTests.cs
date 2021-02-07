// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class LogisticDistributionTests : ContinuousDistributionTests<LogisticDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        [TestCase(0.0)]
        public void Sigma_WrongValues(double d)
        {
            Assert.False(LogisticDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidSigma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Sigma = d; });
        }

        protected override LogisticDistribution GetDist(LogisticDistribution other = null)
        {
            return new LogisticDistribution { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LogisticDistribution GetDist(uint seed, LogisticDistribution other = null)
        {
            return new LogisticDistribution(seed) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LogisticDistribution GetDist(IGenerator gen, LogisticDistribution other = null)
        {
            return new LogisticDistribution(gen) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LogisticDistribution GetDistWithParams(LogisticDistribution other = null)
        {
            return new LogisticDistribution(GetMu(other), GetSigma(other));
        }

        protected override LogisticDistribution GetDistWithParams(uint seed, LogisticDistribution other = null)
        {
            return new LogisticDistribution(seed, GetMu(other), GetSigma(other));
        }

        protected override LogisticDistribution GetDistWithParams(IGenerator gen, LogisticDistribution other = null)
        {
            return new LogisticDistribution(gen, GetMu(other), GetSigma(other));
        }

        // any value
        private double GetMu(IMuDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Mu;
        }

        // sigma >= 0, better keep it low
        private double GetSigma(ISigmaDistribution<double> d)
        {
            if (d != null)
            {
                return d.Sigma;
            }
            double s;
            do s = Rand.NextDouble(); while (TMath.IsZero(s));
            return s;
        }
    }
}
