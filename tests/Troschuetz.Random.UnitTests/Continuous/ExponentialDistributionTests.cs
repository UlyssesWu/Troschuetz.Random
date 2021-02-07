// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class ExponentialDistributionTests : ContinuousDistributionTests<ExponentialDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Lambda_WrongValues(double d)
        {
            Assert.False(ExponentialDistribution.IsValidParam(d));
            Assert.False(_dist.IsValidLambda(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Lambda = d; });
        }

        protected override ExponentialDistribution GetDist(ExponentialDistribution other = null)
        {
            return new ExponentialDistribution { Lambda = GetLambda(other) };
        }

        protected override ExponentialDistribution GetDist(uint seed, ExponentialDistribution other = null)
        {
            return new ExponentialDistribution(seed) { Lambda = GetLambda(other) };
        }

        protected override ExponentialDistribution GetDist(IGenerator gen, ExponentialDistribution other = null)
        {
            return new ExponentialDistribution(gen) { Lambda = GetLambda(other) };
        }

        protected override ExponentialDistribution GetDistWithParams(ExponentialDistribution other = null)
        {
            return new ExponentialDistribution(GetLambda(other));
        }

        protected override ExponentialDistribution GetDistWithParams(uint seed, ExponentialDistribution other = null)
        {
            return new ExponentialDistribution(seed, GetLambda(other));
        }

        protected override ExponentialDistribution GetDistWithParams(IGenerator gen, ExponentialDistribution other = null)
        {
            return new ExponentialDistribution(gen, GetLambda(other));
        }

        // lambda > 0
        private double GetLambda(ILambdaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(0.1, 10) : d.Lambda;
        }
    }
}
