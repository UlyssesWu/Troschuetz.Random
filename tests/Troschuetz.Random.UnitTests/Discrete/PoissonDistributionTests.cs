// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class PoissonDistributionTests : DiscreteDistributionTests<PoissonDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Lambda_WrongValues(double d)
        {
            Assert.False(PoissonDistribution.IsValidParam(d));
            Assert.False(_dist.IsValidLambda(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Lambda = d; });
        }

        protected override PoissonDistribution GetDist(PoissonDistribution other = null)
        {
            return new PoissonDistribution { Lambda = GetLambda(other) };
        }

        protected override PoissonDistribution GetDist(uint seed, PoissonDistribution other = null)
        {
            return new PoissonDistribution(seed) { Lambda = GetLambda(other) };
        }

        protected override PoissonDistribution GetDist(IGenerator gen, PoissonDistribution other = null)
        {
            return new PoissonDistribution(gen) { Lambda = GetLambda(other) };
        }

        protected override PoissonDistribution GetDistWithParams(PoissonDistribution other = null)
        {
            return new PoissonDistribution(GetLambda(other));
        }

        protected override PoissonDistribution GetDistWithParams(uint seed, PoissonDistribution other = null)
        {
            return new PoissonDistribution(seed, GetLambda(other));
        }

        protected override PoissonDistribution GetDistWithParams(IGenerator gen, PoissonDistribution other = null)
        {
            return new PoissonDistribution(gen, GetLambda(other));
        }

        // lambda > 0
        private double GetLambda(ILambdaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(1, 10) : d.Lambda;
        }
    }
}
