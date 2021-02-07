// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ChiSquareDistributionTests : ContinuousDistributionTests<ChiSquareDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(ChiSquareDistribution.IsValidParam(i));
            Assert.False(_dist.IsValidAlpha(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = i; });
        }

        protected override ChiSquareDistribution GetDist(ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution { Alpha = GetAlpha(other) };
        }

        protected override ChiSquareDistribution GetDist(uint seed, ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution(seed) { Alpha = GetAlpha(other) };
        }

        protected override ChiSquareDistribution GetDist(IGenerator gen, ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution(gen) { Alpha = GetAlpha(other) };
        }

        protected override ChiSquareDistribution GetDistWithParams(ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution(GetAlpha(other));
        }

        protected override ChiSquareDistribution GetDistWithParams(uint seed, ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution(seed, GetAlpha(other));
        }

        protected override ChiSquareDistribution GetDistWithParams(IGenerator gen, ChiSquareDistribution other = null)
        {
            return new ChiSquareDistribution(gen, GetAlpha(other));
        }

        // alpha > 0
        private int GetAlpha(IAlphaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Alpha;
        }
    }
}
