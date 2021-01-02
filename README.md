# Troschuetz.Random

[![License: MIT][project-license-badge]][project-license]
[![Donate][paypal-donations-badge]][paypal-donations]
[![standard-readme compliant][github-standard-readme-badge]][github-standard-readme]
[![Docs][doxygen-docs-badge]][doxygen-docs]
[![Gitlab pipeline status][gitlab-pipeline-status-badge]][gitlab-pipelines]
[![NuGet version][nuget-version-badge]][nuget-package]
[![NuGet downloads][nuget-downloads-badge]][nuget-package]
[![Renovate enabled][renovate-badge]][renovate-website]

Fully managed library providing various random number generators and distributions.

All the hard work behind this library was done by Stefan Troschütz, and for which I thank him very much.
What I have done with his great project, was simply to refactor and improve his code,
while offering a new Python-style random class.

Please visit the [page of the original project][code-project-article]
in order to get an overview of the contents of this library.

## Table of Contents

- [Install](#install)
- [Usage](#usage)
  - [Extensibility](#extensibility)
- [Maintainers](#maintainers)
- [Contributing](#contributing)
- [License](#license)

## Install

NuGet package [Troschuetz.Random][nuget-package] is available for download:

```bash
dotnet add package Troschuetz.Random
```

## Usage

See example below to understand how you can use the library:

```cs

using System;
using System.Linq;
using Troschuetz.Random.Distributions.Continuous;
using Troschuetz.Random.Generators;

namespace Troschuetz.Random.Samples
{
    /// <summary>
    ///   Some examples of what you can do with this library.
    /// </summary>
    static class UsageExamples
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
```

### Extensibility

After a request from a user, the library has been modified in order to allow it to be easily extended or modified.

Starting from version 4.0, these extensibility cases are supported:

- Defining a custom generator by extending the `AbstractGenerator` class.
- Defining a custom distribution, by extending the `AbstractDistribution` class
  and by implementing either `IContinuousDistribution` or `IDiscreteDistribution`.
- Change the core definition of a standard distribution, by redefining the static `Sample` delegate,
  used to generate distributed numbers, and the static `IsValidParam`/`AreValidParams` delegates, used to validate parameters.

For your convenience, below you can find an example of how you can implement all features above
(the code is also present in the samples folder):

```cs

using System;
using System.Linq;
using Troschuetz.Random.Distributions;
using Troschuetz.Random.Distributions.Continuous;
using Troschuetz.Random.Generators;

namespace Troschuetz.Random.Samples
{
    /// <summary>
    ///   Examples on how the library can be extended or modified.
    /// </summary>
    static class ExtensibilityExamples
    {
        public static void Main()
        {
            // 1) Use SuperSillyGenerator to generate a few numbers.
            Console.WriteLine("Super silly generator in action!");
            var ssg = new SuperSillyGenerator(21U);
            foreach (var x in ssg.Doubles().Take(5)) Console.WriteLine(x);

            Console.WriteLine();

            // 2) Use SuperSillyContinuousDistribution to generate a few numbers.
            Console.WriteLine("Super silly distribution in action!");
            var ssd = new SuperSillyContinuousDistribution(ssg);
            Console.WriteLine(ssd.NextDouble());
            Console.WriteLine(ssd.NextDouble());
            Console.WriteLine(ssd.NextDouble());

            Console.WriteLine();

            // 3) Use SuperSillyGenerator with a normal distribution.
            Console.WriteLine("Super silly generator with a normal distribution");
            var normal = new NormalDistribution(ssg, 0, 1);
            Console.WriteLine(normal.NextDouble());
            Console.WriteLine(normal.NextDouble());
            Console.WriteLine(normal.NextDouble());

            Console.WriteLine();

            // 4) Change the core logic of normal distribution with a... Silly one.
            Console.WriteLine("Super silly redefinition of a normal distribution");
            NormalDistribution.Sample = (generator, mu, sigma) =>
            {
                // Silly method!!!
                return generator.NextDouble() + mu + sigma + mu * sigma;
            };
            Console.WriteLine(normal.NextDouble());
            Console.WriteLine(normal.NextDouble());
            Console.WriteLine(normal.NextDouble());

            Console.WriteLine();

            // 5) Use the new logic, even through TRandom.
            Console.WriteLine("Super silly redefinition of a normal distribution - via TRandom");
            var trandom = TRandom.New();
            Console.WriteLine(trandom.Normal(5, 0.5));
            Console.WriteLine(trandom.Normal(5, 0.5));
            Console.WriteLine(trandom.Normal(5, 0.5));

            Console.WriteLine();

            // 6) Use the extension method you defined for SuperSillyContinuousDistribution.
            Console.WriteLine("Super silly distribution via TRandom");
            Console.WriteLine(trandom.SuperSilly());
            Console.WriteLine(trandom.SuperSilly());
            Console.WriteLine(trandom.SuperSilly());
        }
    }

    // Super silly generator, which is provided as an example on how one can build a new generator.
    // Of course, never use this generator in production... It is super silly, after all.
    class SuperSillyGenerator : AbstractGenerator
    {
        // The state of the generaror, which usually consists of one or more variables. The initial
        // state, maybe depending on the seed, should be set by overriding the Reset method.
        uint _state;

        // Just a simple constructor which passes the seed to the base constructor.
        public SuperSillyGenerator(uint seed) : base(seed)
        {
        }

        // Should return true if your generator can reset, false otherwise.
        public override bool CanReset => true;

        // Here you should handle the state of your generator. ALWAYS remember to call
        // base.Reset(seed), since it is necessary to correctly reset the AbstractGenerator.
        public override bool Reset(uint seed)
        {
            base.Reset(seed);
            _state = seed;
            return true;
        }

        // You must provide only three generation methods; from these methods, the AbstractGenerator
        // returns all other necessary objects.
        public override int NextInclusiveMaxValue() => (int) (++_state >> 1);
        public override double NextDouble() => ToDouble(++_state);
        public override uint NextUInt() => ++_state;
    }

    // Super silly continuous distribution which is provided as an example on how one can build a
    // new distribution. Of course, never use this distribution in production... It is super silly,
    // after all.
    class SuperSillyContinuousDistribution : AbstractDistribution, IContinuousDistribution
    {
        // Just a simple constructor which passes the generator to the base constructor.
        public SuperSillyContinuousDistribution(IGenerator generator) : base(generator)
        {
        }

        public double Minimum => 0.0;

        public double Maximum => 1.0;

        public double Mean => 0.5;

        public double Median => 0.5;

        public double[] Mode
        {
            get { throw new NotSupportedException(); }
        }

        public double Variance => 1.0 / 12.0;

        // The generation method, in which you define the logic of your distribution.
        public double NextDouble() => Sample(Generator);

        // Optional: Define your logic in a delegate, so that you can reuse it in TRandom or let
        //           others change it, if necessary. All standard distributions do this.
        public static Func<IGenerator, double> Sample { get; set; } = generator => generator.NextDouble();
    }

    // An example of how to enrich the TRandom class with your custom distribution.
    static class TRandomExtensions
    {
        // Simply call the static Sample you defined above. In the Main function, you can see that
        // this method can be used as other methods defined in TRandom.
        public static double SuperSilly(this TRandom r) => SuperSillyContinuousDistribution.Sample(r.Generator);
    }
}
```

## Maintainers

[@pomma89][gitlab-pomma89].

## Contributing

PRs accepted.

Small note: If editing the README, please conform to the [standard-readme][github-standard-readme] specification.

## License

MIT © 2012-2021 [Alessio Parma][personal-website]

[code-project-article]: https://www.codeproject.com/articles/15102/net-random-number-generators-and-distributions
[doxygen-docs]: https://pomma89.gitlab.io/troschuetz-random/
[doxygen-docs-badge]: https://img.shields.io/badge/Doxygen-OK-green?style=flat-square
[github-standard-readme]: https://github.com/RichardLitt/standard-readme
[github-standard-readme-badge]: https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square
[gitlab-pipeline-status-badge]: https://gitlab.com/pomma89/troschuetz-random/badges/main/pipeline.svg?style=flat-square
[gitlab-pipelines]: https://gitlab.com/pomma89/troschuetz-random/pipelines
[gitlab-pomma89]: https://gitlab.com/pomma89
[nuget-downloads-badge]: https://img.shields.io/nuget/dt/Troschuetz.Random?style=flat-square
[nuget-package]: https://www.nuget.org/packages/Troschuetz.Random/
[nuget-version-badge]: https://img.shields.io/nuget/v/Troschuetz.Random?style=flat-square
[paypal-donations]: https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=ELJWKEYS9QGKA
[paypal-donations-badge]: https://img.shields.io/badge/Donate-PayPal-important.svg?style=flat-square
[personal-website]: https://alessioparma.xyz/
[project-license]: https://gitlab.com/pomma89/troschuetz-random/-/blob/main/LICENSE
[project-license-badge]: https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square
[renovate-badge]: https://img.shields.io/badge/renovate-enabled-brightgreen.svg?style=flat-square
[renovate-website]: https://renovate.whitesourcesoftware.com/
