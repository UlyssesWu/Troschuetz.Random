// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class GeometricDistributionTests : DiscreteDistributionTests<GeometricDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        [TestCase(1 + TinyPos)]
        [TestCase(1 + SmallPos)]
        [TestCase(1 + LargePos)]
        public void Alpha_WrongValues(double d)
        {
            Assert.False(GeometricDistribution.IsValidParam(d));
            Assert.False(_dist.IsValidAlpha(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Alpha = d; });
        }

        protected override GeometricDistribution GetDist(GeometricDistribution other = null)
        {
            return new GeometricDistribution { Alpha = GetAlpha(other) };
        }

        protected override GeometricDistribution GetDist(uint seed, GeometricDistribution other = null)
        {
            return new GeometricDistribution(seed) { Alpha = GetAlpha(other) };
        }

        protected override GeometricDistribution GetDist(IGenerator gen, GeometricDistribution other = null)
        {
            return new GeometricDistribution(gen) { Alpha = GetAlpha(other) };
        }

        protected override GeometricDistribution GetDistWithParams(GeometricDistribution other = null)
        {
            return new GeometricDistribution(GetAlpha(other));
        }

        protected override GeometricDistribution GetDistWithParams(uint seed, GeometricDistribution other = null)
        {
            return new GeometricDistribution(seed, GetAlpha(other));
        }

        protected override GeometricDistribution GetDistWithParams(IGenerator gen, GeometricDistribution other = null)
        {
            return new GeometricDistribution(gen, GetAlpha(other));
        }

        // alpha > 0 && alpha <= 1
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 1) : d.Alpha;
        }
    }
}
