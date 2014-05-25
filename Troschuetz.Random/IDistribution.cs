/*
 * Copyright � 2006 Stefan Trosch�tz (stefan@troschuetz.de)
 * Copyright � 2012-2014 Alessio Parma (alessio.parma@gmail.com)
 * 
 * This file is part of Troschuetz.Random Class Library.
 * 
 * Troschuetz.Random is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA 
 */

namespace Troschuetz.Random
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Contracts;
    using JetBrains.Annotations;

    /// <summary>
    ///   Declares common functionality for all random number distributions.
    /// </summary>
    [ContractClass(typeof(DistributionContract))]
    public interface IDistribution
    {
        /// <summary>
        ///   Gets a <see cref="IGenerator"/> object that can be used as underlying random number generator.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        IGenerator Generator { get; }

        /// <summary>
        ///   Gets a value indicating whether the random number distribution can be reset, 
        ///   so that it produces the same random number sequence again.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        bool CanReset { get; }

        /// <summary>
        ///   Gets the minimum possible value of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        double Minimum { get; }

        /// <summary>
        ///   Gets the maximum possible value of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        double Maximum { get; }

        /// <summary>
        ///   Gets the mean of distributed random numbers.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///   Thrown if mean is not defined for given distribution with some parameters.
        /// </exception>
        [System.Diagnostics.Contracts.Pure]
        double Mean { get; }

        /// <summary>
        ///   Gets the median of distributed random numbers.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///   Thrown if median is not defined for given distribution with some parameters.
        /// </exception>
        [System.Diagnostics.Contracts.Pure]
        double Median { get; }

        /// <summary>
        ///   Gets the variance of distributed random numbers.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///   Thrown if variance is not defined for given distribution with some parameters.
        /// </exception>
        [System.Diagnostics.Contracts.Pure]
        double Variance { get; }

        /// <summary>
        ///   Gets the mode of distributed random numbers.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///   Thrown if mode is not defined for given distribution with some parameters.
        /// </exception>
        [System.Diagnostics.Contracts.Pure]
        double[] Mode { get; }

        /// <summary>
        ///   Resets the random number distribution, so that it produces the same random number sequence again.
        /// </summary>
        /// <returns>
        ///   <see langword="true"/>, if the random number distribution was reset; otherwise, <see langword="false"/>.
        /// </returns>
        bool Reset();

        /// <summary>
        ///   Returns a distributed floating point random number.
        /// </summary>
        /// <returns>A distributed double-precision floating point number.</returns>
        double NextDouble();
    }

    /// <summary>
    ///   Abstract class which implements some features shared across all distributions.
    /// </summary>
    /// <typeparam name="TGen">The type of the generator used by the distribution.</typeparam>
    /// <remarks>
    ///   Generator type is explictly declared to allow better performances.
    /// </remarks>
    public abstract class Distribution<TGen> where TGen : IGenerator
    {
        /// <summary>
        ///   Stores a <see cref="TGen"/> object that can be used as underlying random number generator.
        /// </summary>
        protected readonly TGen Gen;

        internal Distribution(TGen generator)
        {
            Gen = generator;
        }

        #region IDistribution Members

        /// <summary>
        ///   Gets a value indicating whether the random number distribution can be reset, 
        ///   so that it produces the same random number sequence again.
        /// </summary>
        public bool CanReset
        {
            get { return Gen.CanReset; }
        }

        /// <summary>
        ///   Gets a <see cref="IGenerator"/> object that can be used as underlying random number generator.
        /// </summary>
        public IGenerator Generator
        {
            get { return Gen; }
        }

        /// <summary>
        ///   Resets the random number distribution, so that it produces the same random number sequence again.
        /// </summary>
        /// <returns>
        ///   <see langword="true"/>, if the random number distribution was reset; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Reset()
        {
            return Gen.Reset();
        }

        #endregion
    }

    /// <summary>
    ///   Declares common functionality for all continuous random number distributions.
    /// </summary>
    public interface IContinuousDistribution : IDistribution
    {
    }

    /// <summary>
    ///   Declares common functionality for all discrete random number distributions.
    /// </summary>
    public interface IDiscreteDistribution : IDistribution
    {
        /// <summary>
        ///   Returns a distributed random number.
        /// </summary>
        /// <returns>A distributed 32-bit signed integer.</returns>
        int Next();
    }

    /// <summary>
    ///   Models a distribution with an alpha parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(AlphaDistributionContract<>))]
    public interface IAlphaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter alpha which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Alpha { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Alpha"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Alpha"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidAlpha(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a beta parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(BetaDistributionContract<>))]
    public interface IBetaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter beta which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Beta { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Beta"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Beta"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidBeta(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a gamma parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(GammaDistributionContract<>))]
    public interface IGammaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter gamma which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Gamma { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Gamma"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Gamma"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidGamma(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a lambda parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(LambdaDistributionContract<>))]
    public interface ILambdaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter lambda which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Lambda { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Lambda"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Lambda"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidLambda(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a mu parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(MuDistributionContract<>))]
    public interface IMuDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter mu which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Mu { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Mu"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Mu"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidMu(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a nu parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(NuDistributionContract<>))]
    public interface INuDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter nu which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Nu { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Nu"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Nu"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidNu(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a sigma parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(SigmaDistributionContract<>))]
    public interface ISigmaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter sigma which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Sigma { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Sigma"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Sigma"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidSigma(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a theta parameter.
    /// </summary>
    /// <typeparam name="TNum">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(ThetaDistributionContract<>))]
    public interface IThetaDistribution<TNum> where TNum : struct
    {
        /// <summary>
        ///   Gets or sets the parameter theta which is used for generation of distributed random numbers.
        /// </summary>
        [System.Diagnostics.Contracts.Pure]
        TNum Theta { get; set; }

        /// <summary>
        ///   Determines whether the specified value is valid for parameter <see cref="Theta"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Theta"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool IsValidTheta(TNum value);
    }

    /// <summary>
    ///   Models a distribution with a weights parameter.
    /// </summary>
    /// <typeparam name="T">The numeric type of the parameter.</typeparam>
    [ContractClass(typeof(WeightsDistributionContract<>))]
    public interface IWeightsDistribution<T> where T : struct
    {
        /// <summary>
        ///   Gets or sets the parameter weights which is used for generation of distributed random numbers.
        /// </summary>
        [NotNull, System.Diagnostics.Contracts.Pure]
        ICollection<T> Weights { get; set; }

        /// <summary>
        ///   Determines whether specified values are valid for parameter <see cref="Weights"/>.
        /// </summary>
        /// <param name="values">The values to check.</param>
        /// <returns>
        ///   <see langword="true"/> if value is valid for parameter <see cref="Weights"/>;
        ///   otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.Contracts.Pure]
        bool AreValidWeights(IEnumerable<T> values);
    }
}