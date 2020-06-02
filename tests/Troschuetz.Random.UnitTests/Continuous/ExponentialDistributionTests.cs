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