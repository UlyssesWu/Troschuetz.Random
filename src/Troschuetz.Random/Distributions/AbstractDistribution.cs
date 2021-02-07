// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Distributions
{
    using System;
    using Troschuetz.Random.Core;

    /// <summary>
    ///   Abstract class which implements some features shared across all distributions.
    /// </summary>
    /// <remarks>The thread safety of this class depends on the one of the underlying generator.</remarks>
    [Serializable]
    public abstract class AbstractDistribution
    {
        /// <summary>
        ///   Builds a distribution using given generator.
        /// </summary>
        /// <param name="generator">The generator that will be used by the distribution.</param>
        /// <exception cref="ArgumentNullException">Given generator is null.</exception>
        protected AbstractDistribution(IGenerator generator)
        {
            Generator = generator ?? throw new ArgumentNullException(nameof(generator), ErrorMessages.NullGenerator);
        }

        #region IDistribution members

        /// <summary>
        ///   Gets a value indicating whether the random number distribution can be reset, so that
        ///   it produces the same random number sequence again.
        /// </summary>
        public bool CanReset => Generator.CanReset;

        /// <summary>
        ///   Gets the <see cref="IGenerator"/> object that is used as underlying random number generator.
        /// </summary>
        public IGenerator Generator { get; }

        /// <summary>
        ///   Resets the random number distribution, so that it produces the same random number
        ///   sequence again.
        /// </summary>
        /// <returns>
        ///   <see langword="true"/>, if the random number distribution was reset; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Reset() => Generator.Reset();

        #endregion IDistribution members
    }
}
