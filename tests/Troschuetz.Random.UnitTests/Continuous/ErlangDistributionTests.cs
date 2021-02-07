// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ErlangDistributionTests : ContinuousDistributionTests<ErlangDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(ErlangDistribution.AreValidParams(i, 1));
            Assert.False(_dist.IsValidAlpha(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = i; });
        }

        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Lambda_WrongValues(double d)
        {
            Assert.False(ErlangDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidLambda(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Lambda = d; });
        }

        protected override ErlangDistribution GetDist(ErlangDistribution other = null)
        {
            return new ErlangDistribution { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override ErlangDistribution GetDist(uint seed, ErlangDistribution other = null)
        {
            return new ErlangDistribution(seed) { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override ErlangDistribution GetDist(IGenerator gen, ErlangDistribution other = null)
        {
            return new ErlangDistribution(gen) { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override ErlangDistribution GetDistWithParams(ErlangDistribution other = null)
        {
            return new ErlangDistribution(GetAlpha(other), GetLambda(other));
        }

        protected override ErlangDistribution GetDistWithParams(uint seed, ErlangDistribution other = null)
        {
            return new ErlangDistribution(seed, GetAlpha(other), GetLambda(other));
        }

        protected override ErlangDistribution GetDistWithParams(IGenerator gen, ErlangDistribution other = null)
        {
            return new ErlangDistribution(gen, GetAlpha(other), GetLambda(other));
        }

        // alpha > 0
        private int GetAlpha(IAlphaDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Alpha;
        }

        // lambda > 0
        private double GetLambda(ILambdaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Lambda;
        }
    }
}
