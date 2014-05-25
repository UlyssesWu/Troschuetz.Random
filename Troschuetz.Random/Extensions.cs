﻿/*
 * Copyright © 2012-2014 Alessio Parma (alessio.parma@gmail.com)
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
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using JetBrains.Annotations;

    /// <summary>
    ///   Module containing extension methods for many interfaces exposed by this library.
    /// </summary>
    [PublicAPI]
    public static class Extensions
    {
        #region IContinuousDistribution Extensions

        /// <summary>
        ///   Returns an infinites series of random double numbers, by repeating calls to NextDouble.
        ///   Therefore, the series obtained will follow given distribution.
        /// </summary>
        /// <returns>An infinites series of random double numbers, following given distribution.</returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<double> DistributedDoubles<T>(this T dist) where T : IContinuousDistribution
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(dist, null), ErrorMessages.NullDistribution);
            while (true)
            {
                yield return dist.NextDouble();
            }
        }

        #endregion

        #region IDiscreteDistribution Extensions

        /// <summary>
        ///   Returns an infinites series of random numbers, by repeating calls to Next.
        ///   Therefore, the series obtained will follow given distribution.
        /// </summary>
        /// <returns>An infinites series of random numbers, following given distribution.</returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<int> DistributedIntegers<T>(this T dist) where T : IDiscreteDistribution
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(dist, null), ErrorMessages.NullDistribution);
            while (true)
            {
                yield return dist.Next();
            }
        }

        #endregion

        #region IGenerator Extensions

        /// <summary>
        ///   Returns an infinite sequence random Boolean values.
        /// </summary>
        /// <remarks>
        ///   Buffers 31 random bits for future calls, so the random number generator
        ///   is only invoked once in every 31 calls.
        /// </remarks>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <returns>An infinite sequence random Boolean values.</returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<bool> Booleans<TGen>(this TGen generator) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            while (true)
            {
                yield return generator.NextBoolean();
            }
        }

        /// <summary>
        ///   Repeatedly fills the elements of a specified array of bytes with random numbers. 
        /// </summary>
        /// <remarks>
        ///   Each element of the array of bytes is set to a random number greater than or equal to 0, 
        ///   and less than or equal to <see cref="byte.MaxValue"/>.
        /// </remarks>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        /// <returns>An infinite sequence of true values.</returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<bool> Bytes<TGen>(this TGen generator, byte[] buffer) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentNullException>(buffer != null, ErrorMessages.NullBuffer);
            while (true)
            {
                generator.NextBytes(buffer);
                yield return true;
            }
        }

        /// <summary>
        ///   Returns a random item from given list, according to a uniform distribution.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <typeparam name="TItem">The type of the elements of the list.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="list">The list from which an item should be randomly picked.</param>
        /// <returns>A random item belonging to given list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="list"/> is empty.</exception>
        [CanBeNull, System.Diagnostics.Contracts.Pure]
        public static TItem Choice<TGen, TItem>(this TGen generator, IList<TItem> list) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentNullException>(list != null, ErrorMessages.NullList);
            Contract.Requires<InvalidOperationException>(list.Count > 0, ErrorMessages.EmptyList);
            Contract.Ensures(list.Contains(Contract.Result<TItem>()));
            var idx = generator.Next(list.Count);
            Debug.Assert(idx >= 0 && idx < list.Count);
            return list[idx];
        }

        /// <summary>
        ///   Returns an infinite sequence of random items from given list, according to a uniform distribution.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <typeparam name="TItem">The type of the elements of the list.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="list">The list from which items should be randomly picked.</param>
        /// <returns>An infinite sequence of random items belonging to given list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="list"/> is empty.</exception>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<TItem> Choices<TGen, TItem>(this TGen generator, IList<TItem> list) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentNullException>(list != null, ErrorMessages.NullList);
            Contract.Requires<InvalidOperationException>(list.Count > 0, ErrorMessages.EmptyList);
            Contract.Ensures(Contract.Result<IEnumerable<TItem>>() != null);
            while (true)
            {
                var idx = generator.Next(list.Count);
                Debug.Assert(idx >= 0 && idx < list.Count);
                yield return list[idx];
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of nonnegative floating point random numbers less than 1.0.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <returns>
        ///   An infinite sequence of double-precision floating point numbers greater than or equal to 0.0, 
        ///   and less than 1.0; that is, the range of return values includes 0.0 but not 1.0. 
        /// </returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<double> Doubles<TGen>(this TGen generator) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            while (true)
            {
                yield return generator.NextDouble();
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of nonnegative floating point random numbers less than the specified maximum.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="maxValue">
        ///   The exclusive upper bound of the random number to be generated.
        /// </param>
        /// <returns>
        ///   An infinite sequence of double-precision floating point numbers greater than or equal to 0.0, 
        ///   and less than <paramref name="maxValue"/>; that is, the range of return values
        ///   includes 0 but not <paramref name="maxValue"/>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="maxValue"/> must be greater than or equal to 0.0.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   <paramref name="maxValue"/> cannot be <see cref="double.PositiveInfinity"/>.
        /// </exception>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<double> Doubles<TGen>(this TGen generator, double maxValue) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentOutOfRangeException>(maxValue >= 0, ErrorMessages.NegativeMaxValue);
            Contract.Requires<ArgumentException>(!double.IsPositiveInfinity(maxValue));
            while (true)
            {
                yield return generator.NextDouble(maxValue);
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of floating point random numbers within the specified range. 
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="minValue">
        ///   The inclusive lower bound of the random number to be generated. 
        /// </param>
        /// <param name="maxValue">
        ///   The exclusive upper bound of the random number to be generated. 
        /// </param>
        /// <returns>
        ///   Returns an infinite sequence of double-precision floating point numbers
        ///   greater than or equal to <paramref name="minValue"/>,
        ///   and less than <paramref name="maxValue"/>; that is, the range of return values 
        ///   includes <paramref name="minValue"/> but not <paramref name="maxValue"/>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The range between <paramref name="minValue"/> and <paramref name="maxValue"/> 
        ///   must be less than or equal to <see cref="double.PositiveInfinity"/>.
        /// </exception>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<double> Doubles<TGen>(this TGen generator, double minValue, double maxValue) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentOutOfRangeException>(maxValue >= minValue, ErrorMessages.MinValueGreaterThanMaxValue);
            Contract.Requires<ArgumentException>(!double.IsPositiveInfinity(maxValue - minValue));
            while (true)
            {
                yield return generator.NextDouble(minValue, maxValue);
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of nonnegative random numbers less than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <returns>
        ///   An infinite sequence of 32-bit signed integers greater than or equal to 0,
        ///   and less than <see cref="int.MaxValue"/>; that is, the range of return values
        ///   includes 0 but not <see cref="int.MaxValue"/>.
        /// </returns>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<int> Integers<TGen>(this TGen generator) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            while (true)
            {
                yield return generator.Next();
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of nonnegative random numbers less than the specified maximum.
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="maxValue">
        ///   The exclusive upper bound of the random numbers to be generated.
        /// </param>
        /// <returns>
        ///   An infinite sequence of 32-bit signed integers greater than or equal to 0,
        ///   and less than <paramref name="maxValue"/>; that is, the range of return values
        ///   includes 0 but not <paramref name="maxValue"/>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="maxValue"/> must be greater than or equal to 0. 
        /// </exception>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<int> Integers<TGen>(this TGen generator, int maxValue) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentOutOfRangeException>(maxValue >= 0, ErrorMessages.NegativeMaxValue);
            while (true)
            {
                yield return generator.Next(maxValue);
            }
        }

        /// <summary>
        ///   Returns an infinite sequence of random numbers within the specified range. 
        /// </summary>
        /// <typeparam name="TGen">The type of the random numbers generator.</typeparam>
        /// <param name="generator">The generator from which random numbers are drawn.</param>
        /// <param name="minValue">
        ///   The inclusive lower bound of the random number to be generated. 
        /// </param>
        /// <param name="maxValue">
        ///   The exclusive upper bound of the random number to be generated. 
        /// </param>
        /// <returns>
        ///   An infinite sequence of 32-bit signed integers greater than or equal to <paramref name="minValue"/>, 
        ///   and less than <paramref name="maxValue"/>; that is, the range of return values includes 
        ///   <paramref name="minValue"/> but not <paramref name="maxValue"/>. 
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.
        /// </exception>
        [NotNull, System.Diagnostics.Contracts.Pure]
        public static IEnumerable<int> Integers<TGen>(this TGen generator, int minValue, int maxValue) where TGen : IGenerator
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(generator, null), ErrorMessages.NullGenerator);
            Contract.Requires<ArgumentOutOfRangeException>(maxValue >= minValue, ErrorMessages.MinValueGreaterThanMaxValue);
            while (true)
            {
                yield return generator.Next(minValue, maxValue);
            }
        }

        #endregion
    }
}
