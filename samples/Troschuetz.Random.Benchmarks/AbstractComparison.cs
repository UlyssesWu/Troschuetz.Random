// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Troschuetz.Random.Generators;

namespace Troschuetz.Random.Benchmarks
{
    public abstract class AbstractComparison
    {
        protected static readonly Dictionary<string, IGenerator> Generators = new Dictionary<string, IGenerator>
        {
            [N<XorShift128Generator>()] = new XorShift128Generator(),
            [N<MT19937Generator>()] = new MT19937Generator(),
            [N<NR3Generator>()] = new NR3Generator(),
            [N<NR3Q1Generator>()] = new NR3Q1Generator(),
            [N<NR3Q2Generator>()] = new NR3Q2Generator(),
            [N<ALFGenerator>()] = new ALFGenerator(),
            [N<StandardGenerator>()] = new StandardGenerator()
        };

        [Params("XorShift128", "MT19937", "NR3", "NR3Q1", "NR3Q2", "ALF", "Standard")]
        public string Generator { get; set; }

        protected static string N<T>() => typeof(T).Name.Replace(nameof(Generator), string.Empty).Replace("Distribution", string.Empty);
    }
}
