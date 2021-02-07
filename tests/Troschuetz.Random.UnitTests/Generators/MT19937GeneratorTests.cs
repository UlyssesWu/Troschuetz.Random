// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Generators
{
    using Random.Generators;

    public sealed class MT19937GeneratorTests : GeneratorTests
    {
        protected override IGenerator GetGenerator()
        {
            return new MT19937Generator();
        }

        protected override IGenerator GetGenerator(int seed)
        {
            return new MT19937Generator(seed);
        }

        protected override IGenerator GetGenerator(uint seed)
        {
            return new MT19937Generator(seed);
        }
    }
}
