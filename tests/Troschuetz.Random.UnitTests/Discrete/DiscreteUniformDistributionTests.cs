// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Discrete
{
    using System;
    using Distributions.Discrete;
    using NUnit.Framework;

    public sealed class DiscreteUniformDistributionTests : DiscreteDistributionTests<DiscreteUniformDistribution>
    {
        [Test]
        public void InvalidParameters1()
        {
            Assert.False(DiscreteUniformDistribution.AreValidParams(50, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = 1; _dist.Alpha = 50; });
        }

        [Test]
        public void InvalidParameters2()
        {
            Assert.False(DiscreteUniformDistribution.AreValidParams(_dist.Alpha, _dist.Alpha - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = _dist.Alpha - 1; });
        }

        [Test]
        public void InvalidParameters3()
        {
            Assert.False(DiscreteUniformDistribution.AreValidParams(50, int.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Beta = int.MaxValue; });
        }

        protected override DiscreteUniformDistribution GetDist(DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override DiscreteUniformDistribution GetDist(uint seed, DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution(seed) { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override DiscreteUniformDistribution GetDist(IGenerator gen, DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution(gen) { Beta = GetBeta(other), Alpha = GetAlpha(other) };
        }

        protected override DiscreteUniformDistribution GetDistWithParams(DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution(GetAlpha(other), GetBeta(other));
        }

        protected override DiscreteUniformDistribution GetDistWithParams(uint seed, DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution(seed, GetAlpha(other), GetBeta(other));
        }

        protected override DiscreteUniformDistribution GetDistWithParams(IGenerator gen, DiscreteUniformDistribution other = null)
        {
            return new DiscreteUniformDistribution(gen, GetAlpha(other), GetBeta(other));
        }

        // alpha <= beta && beta < int.MaxValue
        private int GetAlpha(IAlphaDistribution<int> d)
        {
            return d == null ? Rand.Next(10) : d.Alpha;
        }

        // alpha <= beta && beta < int.MaxValue
        private int GetBeta(IBetaDistribution<int> d)
        {
            return d == null ? Rand.Next(10, 100) : d.Beta;
        }
    }
}
