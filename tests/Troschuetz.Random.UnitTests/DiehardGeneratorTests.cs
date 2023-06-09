﻿// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Troschuetz.Random.Tests
{
    /// <summary>
    ///   Unit tests taken from the DIEHARD test suite.
    /// </summary>
    public abstract partial class GeneratorTests
    {
        /// <summary>
        ///   Choose random points on a large interval. The spacings between the points should be
        ///   asymptotically exponentially distributed. The name is based on the birthday paradox.
        /// </summary>
        [Test]
        public void Diehard_BirthdaySpacings()
        {
            const int Days = 365;
            const int SampleCount = 1000;

            var samples = _generator.Integers(Days).Take(SampleCount).ToArray();
            var distances = new List<double>(SampleCount * SampleCount);

            //Parallel.For(0, sampleCount, i =>
            //{
            //    Parallel.For(0, sampleCount, j =>
            //    {
            //        distances.Add(Math.Abs(samples[i] - samples[j]));
            //    });
            //});

            for (var i = 0; i < samples.Length; ++i)
            {
                for (var j = 0; j < samples.Length; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    distances.Add(Math.Abs(samples[i] - samples[j]));
                }
            }

            distances.Sort();

            var mean = ComputeMean(distances);
            var lambda = (distances.Count - 2) / (mean * distances.Count);
            var lambdaLow = lambda * (1 - 1.96 / Math.Sqrt(distances.Count));
            var lambdaUpp = lambda * (1 + 1.96 / Math.Sqrt(distances.Count));

            var median = ComputeMedian(distances);
            var medianLow = Math.Log(2.0) / lambdaUpp;
            var medianUpp = Math.Log(2.0) / lambdaLow;

            const double Adj = 1.28; // Factor found while testing...
            Assert.True(ApproxEquals(median / Adj, medianLow), $"Generator {_generator.GetType().Name} failed: {median} < {medianLow}");
            Assert.True(ApproxEquals(median / Adj, medianUpp), $"Generator {_generator.GetType().Name} failed: {median} > {medianUpp}");
        }
    }
}
