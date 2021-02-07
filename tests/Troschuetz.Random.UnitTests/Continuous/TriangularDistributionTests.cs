// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Tests.Continuous
{
    using Distributions.Continuous;

    public sealed class TriangularDistributionTests : ContinuousDistributionTests<TriangularDistribution>
    {
        protected override TriangularDistribution GetDist(TriangularDistribution other = null)
        {
            return new TriangularDistribution { Beta = GetBeta(other), Gamma = GetGamma(other), Alpha = GetAlpha(other) };
        }

        protected override TriangularDistribution GetDist(uint seed, TriangularDistribution other = null)
        {
            return new TriangularDistribution(seed) { Beta = GetBeta(other), Gamma = GetGamma(other), Alpha = GetAlpha(other) };
        }

        protected override TriangularDistribution GetDist(IGenerator gen, TriangularDistribution other = null)
        {
            return new TriangularDistribution(gen) { Beta = GetBeta(other), Gamma = GetGamma(other), Alpha = GetAlpha(other) };
        }

        protected override TriangularDistribution GetDistWithParams(TriangularDistribution other = null)
        {
            return new TriangularDistribution(GetAlpha(other), GetBeta(other), GetGamma(other));
        }

        protected override TriangularDistribution GetDistWithParams(uint seed, TriangularDistribution other = null)
        {
            return new TriangularDistribution(seed, GetAlpha(other), GetBeta(other), GetGamma(other));
        }

        protected override TriangularDistribution GetDistWithParams(IGenerator gen, TriangularDistribution other = null)
        {
            return new TriangularDistribution(gen, GetAlpha(other), GetBeta(other), GetGamma(other));
        }

        // alpha < beta && alpha <= gamma && beta >= gamma
        private double GetAlpha(IAlphaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-2, 2) : d.Alpha;
        }

        // alpha < beta && alpha <= gamma && beta >= gamma
        private double GetBeta(IBetaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(4, 6) : d.Beta;
        }

        // alpha < beta && alpha <= gamma && beta >= gamma
        private double GetGamma(IGammaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(2, 4) : d.Gamma;
        }
    }
}
