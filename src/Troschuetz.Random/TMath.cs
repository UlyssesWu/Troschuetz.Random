// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Troschuetz.Random
{
    using System;

    /// <summary>
    ///   Simple math utilities used inside the library. They are also public, should you need them
    ///   for custom generators and distributions.
    /// </summary>
    public static class TMath
    {
        /// <summary>
        ///   The delta used when comparing doubles.
        /// </summary>
        public const double Tolerance = 1E-6;

        /// <summary>
        ///   Safely checks if given doubles are equal.
        /// </summary>
        /// <param name="d1">A double.</param>
        /// <param name="d2">A double.</param>
        /// <returns>True if given doubles are safely equal, false otherwise.</returns>
        public static bool AreEqual(double d1, double d2) => IsZero(d1 - d2);

        /// <summary>
        ///   Safely checks if given double is zero.
        /// </summary>
        /// <param name="d">A double.</param>
        /// <returns>True if given double is near zero, false otherwise.</returns>
        public static bool IsZero(double d) => d > -Tolerance && d < Tolerance;

        /// <summary>
        ///   Generates a new seed, using all information available, including time.
        /// </summary>
        public static uint Seed()
        {
            unchecked
            {
                const uint Factor = 19U;
                var seed = 1777771U;

                seed = Factor * seed + (uint)Environment.TickCount;

                var guid = Guid.NewGuid().ToByteArray();
                seed = Factor * seed + BitConverter.ToUInt32(guid, 0);
                seed = Factor * seed + BitConverter.ToUInt32(guid, 8);

#if HAS_THREAD && HAS_PROCESS
                seed = Factor * seed + (uint)System.Threading.Thread.CurrentThread.ManagedThreadId;
                seed = Factor * seed + (uint)System.Diagnostics.Process.GetCurrentProcess().Id;
#endif

                return seed;
            }
        }

        /// <summary>
        ///   Fast square power.
        /// </summary>
        /// <param name="d">A double.</param>
        /// <returns>The square of given double.</returns>
        public static double Square(double d) => IsZero(d) ? 0.0 : d * d;
    }
}
