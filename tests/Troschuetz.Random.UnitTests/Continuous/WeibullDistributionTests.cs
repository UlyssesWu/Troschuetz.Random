// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class WeibullDistributionTests : ContinuousDistributionTests<WeibullDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(WeibullDistribution.AreValidParams(d, 1));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Lambda_WrongValues(double d)
        {
            Assert.False(WeibullDistribution.AreValidParams(1, d));
            Assert.False(_dist.IsValidLambda(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Lambda = d; });
        }

        protected override WeibullDistribution GetDist(WeibullDistribution other = null)
        {
            return new WeibullDistribution { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override WeibullDistribution GetDist(uint seed, WeibullDistribution other = null)
        {
            return new WeibullDistribution(seed) { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override WeibullDistribution GetDist(IGenerator gen, WeibullDistribution other = null)
        {
            return new WeibullDistribution(gen) { Alpha = GetAlpha(other), Lambda = GetLambda(other) };
        }

        protected override WeibullDistribution GetDistWithParams(WeibullDistribution other = null)
        {
            return new WeibullDistribution(GetAlpha(other), GetLambda(other));
        }

        protected override WeibullDistribution GetDistWithParams(uint seed, WeibullDistribution other = null)
        {
            return new WeibullDistribution(seed, GetAlpha(other), GetLambda(other));
        }

        protected override WeibullDistribution GetDistWithParams(IGenerator gen, WeibullDistribution other = null)
        {
            return new WeibullDistribution(gen, GetAlpha(other), GetLambda(other));
        }

        // alpha > 0
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Alpha;
        }

        // lambda > 0
        private double GetLambda(ILambdaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Lambda;
        }
    }
}
