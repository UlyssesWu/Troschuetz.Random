// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Troschuetz.Random.Distributions.Continuous;
using Troschuetz.Random.Generators;

namespace Troschuetz.Random.Samples
{
    /// <summary>
    ///   Some examples of what you can do with this library.
    /// </summary>
    internal static class UsageExamples
    {
        public static void Main()
        {
            // 1) Use TRandom to generate a few random numbers - via IGenerator methods.
            Console.WriteLine("TRandom in action, used as an IGenerator");
            var trandom = new TRandom();
            Console.WriteLine(trandom.Next() - trandom.Next(5) + trandom.Next(3, 5));
            Console.WriteLine(trandom.NextDouble() * trandom.NextDouble(5.5) * trandom.NextDouble(10.1, 21.9));
            Console.WriteLine(trandom.NextBoolean());

            Console.WriteLine();

            // 2) Use TRandom to generate a few random numbers - via extension methods.
            Console.WriteLine("TRandom in action, used as an IGenerator augmented with extension methods");
            Console.WriteLine(string.Join(", ", trandom.Integers().Take(10)));
            Console.WriteLine(string.Join(", ", trandom.Doubles().Take(10)));
            Console.WriteLine(string.Join(", ", trandom.Booleans().Take(10)));

            Console.WriteLine();

            // 3) Use TRandom to generate a few distributed numbers.
            Console.WriteLine("TRandom in action, used as to get distributed numbers");
            Console.WriteLine(trandom.Normal(1.0, 0.1));
            Console.WriteLine(string.Join(", ", trandom.NormalSamples(1.0, 0.1).Take(20)));
            Console.WriteLine(trandom.Poisson(5));
            Console.WriteLine(string.Join(", ", trandom.PoissonSamples(5).Take(20)));

            Console.WriteLine();

            // 4) There are many generators available - XorShift128 is the default.
            var alf = new ALFGenerator(TMath.Seed());
            var nr3 = new NR3Generator();
            var std = new StandardGenerator(127);

            // 5) You can also use distribution directly, even with custom generators.
            Console.WriteLine("Showcase of some distributions");
            Console.WriteLine("Static sample for Normal: " + NormalDistribution.Sample(alf, 1.0, 0.1));
            Console.WriteLine("New instance for Normal: " + new NormalDistribution(1.0, 0.1).NextDouble());

            Console.WriteLine();
        }
    }
}
