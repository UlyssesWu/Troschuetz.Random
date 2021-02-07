// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class FisherSnedecorDistributionTests : ContinuousDistributionTests<FisherSnedecorDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(FisherSnedecorDistribution.AreValidParams(i, 1));
            Assert.False(_dist.IsValidAlpha(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = i; });
        }

        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Beta_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(FisherSnedecorDistribution.AreValidParams(1, i));
            Assert.False(_dist.IsValidBeta(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = i; });
        }

        protected override FisherSnedecorDistribution GetDist(FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override FisherSnedecorDistribution GetDist(uint seed, FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution(seed) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override FisherSnedecorDistribution GetDist(IGenerator gen, FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution(gen) { Alpha = GetAlpha(other), Beta = GetBeta(other) };
        }

        protected override FisherSnedecorDistribution GetDistWithParams(FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override FisherSnedecorDistribution GetDistWithParams(uint seed, FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override FisherSnedecorDistribution GetDistWithParams(IGenerator gen, FisherSnedecorDistribution other = null)
        {
            return new FisherSnedecorDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha > 0
        private int GetAlpha(IAlphaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Alpha;
        }

        // beta > 0
        private int GetBeta(IBetaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Beta;
        }
    }
}
