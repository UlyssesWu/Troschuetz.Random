﻿// The MIT License (MIT)
//
// Copyright (c) 2006-2007 Stefan Troschütz <stefan@troschuetz.de>
//
// Copyright (c) 2012-2020 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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