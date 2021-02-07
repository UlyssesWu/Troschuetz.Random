// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Generators
{
    using Random.Generators;

    public sealed class NR3GeneratorTests : GeneratorTests
    {
        protected override IGenerator GetGenerator() => new NR3Generator();

        protected override IGenerator GetGenerator(int seed) => new NR3Generator(seed);

        protected override IGenerator GetGenerator(uint seed) => new NR3Generator(seed);
    }

    public sealed class NR3Q1GeneratorTests : GeneratorTests
    {
        protected override IGenerator GetGenerator() => new NR3Q1Generator();

        protected override IGenerator GetGenerator(int seed) => new NR3Q1Generator(seed);

        protected override IGenerator GetGenerator(uint seed) => new NR3Q1Generator(seed);
    }

    public sealed class NR3Q2GeneratorTests : GeneratorTests
    {
        protected override IGenerator GetGenerator() => new NR3Q2Generator();

        protected override IGenerator GetGenerator(int seed) => new NR3Q2Generator(seed);

        protected override IGenerator GetGenerator(uint seed) => new NR3Q2Generator(seed);
    }
}
