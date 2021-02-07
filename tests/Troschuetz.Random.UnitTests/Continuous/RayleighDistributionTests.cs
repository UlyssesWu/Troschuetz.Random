// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class RayleighDistributionTests : ContinuousDistributionTests<RayleighDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Sigma_WrongValues(double d)
        {
            Assert.False(RayleighDistribution.IsValidParam(d));
            Assert.False(_dist.IsValidSigma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Sigma = d; });
        }

        protected override RayleighDistribution GetDist(RayleighDistribution other = null)
        {
            return new RayleighDistribution { Sigma = GetSigma(other) };
        }

        protected override RayleighDistribution GetDist(uint seed, RayleighDistribution other = null)
        {
            return new RayleighDistribution(seed) { Sigma = GetSigma(other) };
        }

        protected override RayleighDistribution GetDist(IGenerator gen, RayleighDistribution other = null)
        {
            return new RayleighDistribution(gen) { Sigma = GetSigma(other) };
        }

        protected override RayleighDistribution GetDistWithParams(RayleighDistribution other = null)
        {
            return new RayleighDistribution(GetSigma(other));
        }

        protected override RayleighDistribution GetDistWithParams(uint seed, RayleighDistribution other = null)
        {
            return new RayleighDistribution(seed, GetSigma(other));
        }

        protected override RayleighDistribution GetDistWithParams(IGenerator gen, RayleighDistribution other = null)
        {
            return new RayleighDistribution(gen, GetSigma(other));
        }

        // sigma > 0
        private double GetSigma(ISigmaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Sigma;
        }
    }
}
