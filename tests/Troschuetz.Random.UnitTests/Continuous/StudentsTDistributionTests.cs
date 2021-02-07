// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using System;
    using Distributions.Continuous;
    using NUnit.Framework;

    public sealed class StudentsTDistributionTests : ContinuousDistributionTests<StudentsTDistribution>
    {
        [TestCase(double.NaN)]
        [TestCase(0)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Nu_WrongValues(double d)
        {
            var i = (int)d;
            Assert.False(StudentsTDistribution.IsValidParam(i));
            Assert.False(_dist.IsValidNu(i));
            Assert.Throws<ArgumentOutOfRangeException>(() => { _dist.Nu = i; });
        }

        protected override StudentsTDistribution GetDist(StudentsTDistribution other = null)
        {
            return new StudentsTDistribution { Nu = GetNu(other) };
        }

        protected override StudentsTDistribution GetDist(uint seed, StudentsTDistribution other = null)
        {
            return new StudentsTDistribution(seed) { Nu = GetNu(other) };
        }

        protected override StudentsTDistribution GetDist(IGenerator gen, StudentsTDistribution other = null)
        {
            return new StudentsTDistribution(gen) { Nu = GetNu(other) };
        }

        protected override StudentsTDistribution GetDistWithParams(StudentsTDistribution other = null)
        {
            return new StudentsTDistribution(GetNu(other));
        }

        protected override StudentsTDistribution GetDistWithParams(uint seed, StudentsTDistribution other = null)
        {
            return new StudentsTDistribution(seed, GetNu(other));
        }

        protected override StudentsTDistribution GetDistWithParams(IGenerator gen, StudentsTDistribution other = null)
        {
            return new StudentsTDistribution(gen, GetNu(other));
        }

        // nu > 0
        private int GetNu(INuDistribution<int> d)
        {
            return d == null ? Rand.Next(1, 10) : d.Nu;
        }
    }
}
