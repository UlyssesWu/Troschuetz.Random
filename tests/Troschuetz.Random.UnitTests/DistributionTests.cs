﻿// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using Random.Generators;
    using Shouldly;

    public abstract class ContinuousDistributionTests<TDist> : DistributionTests<TDist>
        where TDist : class, IContinuousDistribution
    {
        /*=============================================================================
            DistributedDoubles
        =============================================================================*/

        [Test]
        [Repeat(RepetitionCount)]
        public void Doubles_ManyRand()
        {
            var doubles = _dist.DistributedDoubles().GetEnumerator();
            doubles.MoveNext();
            for (var i = 0; i < Iterations; ++i, doubles.MoveNext())
            {
                Results[i] = doubles.Current;
            }
            AssertDist(_dist);
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Doubles_SameOutput()
        {
            var doubles = _dist.DistributedDoubles().GetEnumerator();
            doubles.MoveNext();
            for (var i = 0; i < Iterations; ++i, doubles.MoveNext())
            {
                Assert.AreEqual(_otherDist.NextDouble(), doubles.Current);
            }
        }

        /*=============================================================================
            NextDouble
        =============================================================================*/

        [Test]
        [Repeat(RepetitionCount)]
        public void NextDouble_ManyRand()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.NextDouble();
            }
            AssertDist(_dist);
        }

        /*=============================================================================
            Reset
        =============================================================================*/

        [Test]
        [Repeat(RepetitionCount)]
        public void NextDouble_Serialization_AfterManyRand()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                _dist.NextDouble();
            }
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, _dist);
                ms.Position = 0;

                var otherDist = bf.Deserialize(ms) as TDist;
                for (var i = 0; i < Iterations; ++i)
                {
                    Assert.AreEqual(_dist.NextDouble(), otherDist.NextDouble());
                }
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_AfterManyRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.NextDouble();
            }
            AssertDist(_dist);
            Assert.True(_dist.Reset());
            for (var i = 0; i < Iterations; ++i)
            {
                Assert.AreEqual(Results[i], _dist.NextDouble());
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_AfterOneRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            var d = _dist.NextDouble();
            Assert.True(_dist.Reset());
            Assert.AreEqual(d, _dist.NextDouble());
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_DoubleCall_AfterManyRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.NextDouble();
            }
            AssertDist(_dist);
            Assert.True(_dist.Reset());
            Assert.True(_dist.Reset());
            for (var i = 0; i < Iterations; ++i)
            {
                Assert.AreEqual(Results[i], _dist.NextDouble());
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_DoubleCall_AfterOneRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            var d = _dist.NextDouble();
            Assert.True(_dist.Reset());
            Assert.True(_dist.Reset());
            Assert.AreEqual(d, _dist.NextDouble());
        }

        /*=============================================================================
            Serialization
        =============================================================================*/
    }

    public abstract class DiscreteDistributionTests<TDist> : DistributionTests<TDist>
        where TDist : class, IDiscreteDistribution
    {
        /*=============================================================================
            Next
        =============================================================================*/

        /*=============================================================================
            DistributedIntegers
        =============================================================================*/

        [Test]
        [Repeat(RepetitionCount)]
        public void Integers_ManyRand()
        {
            var integers = _dist.DistributedIntegers().GetEnumerator();
            integers.MoveNext();
            for (var i = 0; i < Iterations; ++i, integers.MoveNext())
            {
                Results[i] = integers.Current;
            }
            AssertDist(_dist);
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Integers_Next_SameOutput()
        {
            var integers = _dist.DistributedIntegers().GetEnumerator();
            for (var i = 0; i < Iterations; ++i)
            {
                integers.MoveNext();
                Assert.AreEqual(integers.Current, _otherDist.Next());
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Integers_SameOutput()
        {
            var integers = _dist.DistributedIntegers().GetEnumerator();
            integers.MoveNext();
            for (var i = 0; i < Iterations; ++i, integers.MoveNext())
            {
                _otherDist.Next().ShouldBe(integers.Current);
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Next_ManyRand()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.Next();
            }
            AssertDist(_dist);
        }

        /*=============================================================================
            Reset
        =============================================================================*/

        [Test]
        [Repeat(RepetitionCount)]
        public void Next_Serialization_AfterManyRand()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                _dist.Next();
            }
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, _dist);
                ms.Position = 0;

                var otherDist = bf.Deserialize(ms) as TDist;
                for (var i = 0; i < Iterations; ++i)
                {
                    Assert.AreEqual(_dist.NextDouble(), otherDist.NextDouble());
                }
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_AfterManyRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.Next();
            }
            AssertDist(_dist);
            Assert.True(_dist.Reset());
            for (var i = 0; i < Iterations; ++i)
            {
                Assert.AreEqual(Results[i], _dist.Next());
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_AfterOneRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            var d = _dist.Next();
            Assert.True(_dist.Reset());
            Assert.AreEqual(d, _dist.Next());
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_DoubleCall_AfterManyRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            for (var i = 0; i < Iterations; ++i)
            {
                Results[i] = _dist.Next();
            }
            AssertDist(_dist);
            Assert.True(_dist.Reset());
            Assert.True(_dist.Reset());
            for (var i = 0; i < Iterations; ++i)
            {
                Assert.AreEqual(Results[i], _dist.Next());
            }
        }

        [Test]
        [Repeat(RepetitionCount)]
        public void Reset_DoubleCall_AfterOneRand()
        {
            if (!_dist.CanReset)
            {
                Assert.Pass();
            }
            var d = _dist.Next();
            Assert.True(_dist.Reset());
            Assert.True(_dist.Reset());
            Assert.AreEqual(d, _dist.Next());
        }

        /*=============================================================================
            Serialization
        =============================================================================*/

#if HAS_SERIALIZABLE
#endif
    }

    public abstract class DistributionTests<TDist> : TestBase where TDist : IDistribution
    {
        protected const int RepetitionCount = 6;

        protected TDist _dist;

        protected TDist _otherDist;

        private int _currDist;

        [Test]
        public void Construction_NullGenerator()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                GetDist(null);
            });
        }

        [SetUp]
        public void SetUp()
        {
            switch (_currDist)
            {
                case 0:
                    _dist = GetDist();
                    _otherDist = GetDist(_dist.Generator.Seed, _dist);
                    break;

                case 1:
                    var s = (uint)Rand.Next();
                    _dist = GetDist(s);
                    _otherDist = GetDist(s, _dist);
                    break;

                case 2:
                    var g1 = new StandardGenerator(Rand.Next());
                    var g2 = new StandardGenerator((int)g1.Seed);
                    _dist = GetDist(g1);
                    _otherDist = GetDist(g2, _dist);
                    break;

                case 3:
                    _dist = GetDistWithParams();
                    _otherDist = GetDistWithParams(_dist.Generator.Seed, _dist);
                    break;

                case 4:
                    s = (uint)Rand.Next();
                    _dist = GetDistWithParams(s);
                    _otherDist = GetDistWithParams(s, _dist);
                    break;

                case 5:
                    g1 = new StandardGenerator(Rand.Next());
                    g2 = new StandardGenerator((int)g1.Seed);
                    _dist = GetDistWithParams(g1);
                    _otherDist = GetDistWithParams(g2, _dist);
                    break;

                default:
                    throw new Exception();
            }
            _currDist = (_currDist + 1) % RepetitionCount;
        }

        protected abstract TDist GetDist(TDist other = default);

        protected abstract TDist GetDist(uint seed, TDist other = default);

        protected abstract TDist GetDist(IGenerator gen, TDist other = default);

        protected abstract TDist GetDistWithParams(TDist other = default);

        protected abstract TDist GetDistWithParams(uint seed, TDist other = default);

        protected abstract TDist GetDistWithParams(IGenerator gen, TDist other = default);

        /*=============================================================================
            Construction
        =============================================================================*/
    }
}
