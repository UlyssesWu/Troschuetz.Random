// Copyright (c) Alessio Parma <alessio.parma@gmail.com>. All rights reserved.
//
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using NUnitLite;

namespace Troschuetz.Random.UnitTests
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
#if !HAS_AUTORUN
            return new AutoRun(System.Reflection.Assembly.GetEntryAssembly()).Execute(args, new NUnit.Common.ColorConsoleWriter(), System.Console.In);
#else
            return new AutoRun().Execute(args);
#endif
        }
    }
}
