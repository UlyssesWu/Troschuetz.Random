// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ContinuousUniformDistributionTests : ContinuousDistributionTests<ContinuousUniformDistribution>
    {
        [TestCase(double.NaN, 1.0)]
        [TestCase(1.0, double.NaN)]
        [TestCase(TinyPos, TinyNeg)]
        [TestCase(SmallPos, SmallNeg)]
        [TestCase(LargePos, LargeNeg)]
        public void AlphaBeta_WrongValues(double a, double b)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Assert.False(ContinuousUniformDistribution.AreValidParams(a, b));
                _dist.Alpha = _dist.Beta = a;
                Assert.False(_dist.IsValidBeta(b));
                _dist.Beta = b;
            });
        }

        protected override ContinuousUniformDistribution GetDist(ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override ContinuousUniformDistribution GetDist(uint seed, ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution(seed) { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override ContinuousUniformDistribution GetDist(IGenerator gen, ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution(gen) { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override ContinuousUniformDistribution GetDistWithParams(ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override ContinuousUniformDistribution GetDistWithParams(uint seed, ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override ContinuousUniformDistribution GetDistWithParams(IGenerator gen, ContinuousUniformDistribution other = null)
        {
            return new ContinuousUniformDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha <= beta
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(5) : d.Alpha;
        }

        // alpha <= beta
        private double GetBeta(IBetaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(5.1, 10) : d.Beta;
        }
    }
}
