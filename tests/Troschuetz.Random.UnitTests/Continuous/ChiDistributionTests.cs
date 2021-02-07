// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ChiDistributionTests : ContinuousDistributionTests<ChiDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(ChiDistribution.IsValidParam(i));
            Assert.False(_dist.IsValidAlpha(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = i; });
        }

        protected override ChiDistribution GetDist(ChiDistribution other = null)
        {
            return new ChiDistribution { Alpha = GetAlpha(other) };
        }

        protected override ChiDistribution GetDist(uint seed, ChiDistribution other = null)
        {
            return new ChiDistribution(seed) { Alpha = GetAlpha(other) };
        }

        protected override ChiDistribution GetDist(IGenerator gen, ChiDistribution other = null)
        {
            return new ChiDistribution(gen) { Alpha = GetAlpha(other) };
        }

        protected override ChiDistribution GetDistWithParams(ChiDistribution other = null)
        {
            return new ChiDistribution(GetAlpha(other));
        }

        protected override ChiDistribution GetDistWithParams(uint seed, ChiDistribution other = null)
        {
            return new ChiDistribution(seed, GetAlpha(other));
        }

        protected override ChiDistribution GetDistWithParams(IGenerator gen, ChiDistribution other = null)
        {
            return new ChiDistribution(gen, GetAlpha(other));
        }

        // alpha > 0
        private int GetAlpha(IAlphaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Alpha;
        }
    }
}
