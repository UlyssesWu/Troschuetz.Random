﻿// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random.Generators
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///   Represents a simple pseudo-random number generator.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     The <see cref="StandardGenerator"/> type internally uses an instance of the
    ///     <see cref="Random"/> type to generate pseudo-random numbers.
    ///   </para>
    ///   <para>This generator is NOT thread safe.</para>
    /// </remarks>
    [Serializable]
    [SuppressMessage("Security", "SCS0005:Weak random number generator.", Justification = "The goal of this library is to generate random numbers quickly")]
    public sealed class StandardGenerator : AbstractGenerator
    {
        #region Fields

        /// <summary>
        ///   Stores an instance of <see cref="Random"/> type that is used to generate random numbers.
        /// </summary>
        private Random _generator;

        /// <summary>
        ///   Stores a byte array used to compute the result of
        ///   <see cref="NextUIntInclusiveMaxValue()"/>, starting from the output of <see cref="AbstractGenerator.NextBytes(byte[])"/>.
        /// </summary>
        private byte[] _uintBuffer;

        #endregion Fields

        #region Construction

        /// <summary>
        ///   Initializes a new instance of the <see cref="StandardGenerator"/> class, using a
        ///   time-dependent default seed value.
        /// </summary>
        public StandardGenerator() : base(TMath.Seed())
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="StandardGenerator"/> class, using the
        ///   specified seed value.
        /// </summary>
        /// <param name="seed">
        ///   A number used to calculate a starting value for the pseudo-random number sequence. If
        ///   a negative number is specified, the absolute value of the number is used.
        /// </param>
        public StandardGenerator(int seed) : base((uint)seed)
        {
        }

        #endregion Construction

        #region IGenerator members

        /// <summary>
        ///   Gets a value indicating whether the random number generator can be reset, so that it
        ///   produces the same random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        ///   Returns a nonnegative floating point random number less than 1.0.
        /// </summary>
        /// <returns>
        ///   A double-precision floating point number greater than or equal to 0.0, and less than
        ///   1.0; that is, the range of return values includes 0.0 but not 1.0.
        /// </returns>
        public override double NextDouble()
        {
            var result = _generator.NextDouble();

            // Postconditions
            Debug.Assert(result >= 0.0 && result < 1.0);
            return result;
        }

        /// <summary>
        ///   Returns a nonnegative random number less than or equal to <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>
        ///   A 32-bit signed integer greater than or equal to 0, and less than or equal to
        ///   <see cref="int.MaxValue"/>; that is, the range of return values includes 0 and <see cref="int.MaxValue"/>.
        /// </returns>
        public override int NextInclusiveMaxValue()
        {
            var result = _generator.Next();

            // Postconditions
            Debug.Assert(result >= 0 && result < int.MaxValue);
            return result;
        }

        /// <summary>
        ///   Returns an unsigned random number.
        /// </summary>
        /// <returns>
        ///   A 32-bit unsigned integer greater than or equal to 0, and less than or equal to
        ///   <see cref="uint.MaxValue"/>; that is, the range of return values includes 0 and <see cref="uint.MaxValue"/>.
        /// </returns>
        public override uint NextUIntInclusiveMaxValue()
        {
            _generator.NextBytes(_uintBuffer);
            return BitConverter.ToUInt32(_uintBuffer, 0);
        }

        /// <summary>
        ///   Resets the random number generator using the specified seed, so that it produces the
        ///   same random number sequence again. To understand whether this generator can be reset,
        ///   you can query the <see cref="CanReset"/> property.
        /// </summary>
        /// <param name="seed">The seed value used by the generator.</param>
        /// <returns>True if the random number generator was reset; otherwise, false.</returns>
        public override bool Reset(uint seed)
        {
            base.Reset(seed);

            // Create a new Random object using the specified seed.
            _generator = new Random((int)seed); // Safe cast, seed is always positive.

            // Initialize the buffer used to store the bytes required to create an unsigned integer.
            _uintBuffer = new byte[4];

            return true;
        }

        #endregion IGenerator members
    }
}
