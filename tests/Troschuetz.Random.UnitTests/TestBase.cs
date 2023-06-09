﻿// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public abstract class TestBase
    {
        protected const int Iterations = 200000;
        protected const double LargeNeg = -100;
        protected const double LargePos = -LargeNeg;
        protected const double SmallNeg = -1;
        protected const double SmallPos = -SmallNeg;
        protected const double TinyNeg = -0.01;
        protected const double TinyPos = -TinyNeg;
        protected readonly TRandom Rand = new TRandom();
        protected readonly double[] Results = new double[Iterations];
        private const double Epsilon = 0.20; // Relative error: less than 20%

        protected static bool ApproxEquals(double expected, double observed)
        {
            return ApproxEquals(expected, observed, Epsilon);
        }

        protected static bool ApproxEquals(double expected, double observed, double epsilon)
        {
            if (double.IsNaN(expected))
            {
                Assert.Fail("NaN should not be returned");
            }
            if (expected.Equals(0))
            {
                return Math.Abs(expected - observed) < epsilon;
            }
            return Math.Abs((expected - observed) / expected) < epsilon;
        }

        protected static double ComputeMean(ICollection<double> series)
        {
            return series.Select(x => x / series.Count).Sum();
        }

        protected static double ComputeMedian(IList<double> series)
        {
            var hc = series.Count / 2;
            if (hc % 2 == 0)
                return series[hc - 1] / 2.0 + series[hc] / 2.0;
            return series[hc];
        }

        protected static double ComputeVariance(ICollection<double> series, double mean)
        {
            return series.Select(x => Math.Pow(x - mean, 2) / (series.Count - 1)).Sum();
        }

        protected void AssertDist(IDistribution dist)
        {
            var filtered = FilterSeries(Results, dist);
            var mean = ComputeMean(filtered);
            var median = ComputeMedian(filtered);
            var variance = ComputeVariance(filtered, mean);

            try
            {
                Assert.NotNull(dist.Mode);
                Assert.False(dist.Mode.Any(double.IsNaN));
            }
            catch (NotSupportedException)
            {
                // Mode may be undefined
            }

            try
            {
                var c = ApproxEquals(dist.Mean, mean);
                Assert.True(c, "Wrong mean! Expected ({0}), found ({1})", dist.Mean, mean);
            }
            catch (NotSupportedException)
            {
                // Mean may be undefined
            }

            try
            {
                var c = ApproxEquals(dist.Median, median);
                Assert.True(c, "Wrong median! Expected ({0}), found ({1})", dist.Median, median);
            }
            catch (NotSupportedException)
            {
                // Median may be undefined
            }

            try
            {
                var c = ApproxEquals(dist.Variance, variance);
                Assert.True(c, "Wrong variance! Expected ({0}), found ({1})", dist.Variance, variance);
            }
            catch (NotSupportedException)
            {
                // Variance may be undefined
            }
        }

        private static IList<double> FilterSeries(double[] series, IDistribution dist)
        {
            Assert.True(series.All(x => x >= dist.Minimum && x <= dist.Maximum));
            Func<double, bool> filter = (d => !double.IsPositiveInfinity(d) && !double.IsNegativeInfinity(d));
            var filtered = series.Where(filter).ToList();
            filtered.Sort();
            return filtered;
        }
    }
}
