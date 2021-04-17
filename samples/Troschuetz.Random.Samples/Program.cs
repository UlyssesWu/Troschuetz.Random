// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;

namespace Troschuetz.Random.Samples
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("### Usage examples ###");
            UsageExamples.Main();
            Console.WriteLine();

            Console.WriteLine("### Extensibility examples ###");
            ExtensibilityExamples.Main();
            Console.WriteLine();

            Console.Write("Press any key to exit...");
            Console.Read();
        }
    }
}
