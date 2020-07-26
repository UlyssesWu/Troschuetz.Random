# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [5.0.0] - 2020-07-26

### Added

- Added `ToDouble` helper methods to `AbstractGenerator`. They can be used to convert `uint` and `ulong` to `double`.

### Changed

- Improved quality of generated double random numbers (issue #10).

### Removed

- Removed `IntToDoubleMultiplier`, `UIntToDoubleMultiplier` and `ULongToDoubleMultiplier` from `AbstractGenerator`.

## [4.4.0] - 2020-07-22

### Changed

- Removed absolute values for integer seeds (issue #9).

## [4.3.3] - 2020-07-21

### Changed

- Support for Span of bytes changed as default interface method (issue #8).

## [4.3.1] - 2019-08-09

### Changed

- Added support for Span of bytes (PR #8 by Konstantin Safonov).
- Fixed issue #6.

## [4.3.0] - 2018-03-11

### Changed

- Removed DLL compiled for .NET 4.7.1, it is not necessary.
- Removed dependency on Thrower.
- Added NextUIntInclusiveMaxValue to IGenerator (issue #5).
- Marked IGenerator.NextUIntExclusiveMaxValue as obsolete (issue #5).
- Changed behavior of IGenerator.NextUInt, now it does not return uint.MaxValue (issue #5).

## [4.2.0] - 2017-10-28

### Changed

- Added support for .NET Standard 2.0 and .NET Framework 4.7.1.
- Removed graphical tester from NuGet package.

## [4.1.3] - 2017-01-08

### Changed

- Updated Thrower to v4.0.6.
- Added unit tests for Portable and .NET Standard 1.1.
- The behavior of TRandom.Next(int, int) was different from the built-in's (ISSUE#4 by Zhouxing-Su).
- Changed behavior of NextDouble, NextUInt, Doubles, Integers, UnsignedIntegers according to issue #4.

## [4.1.1] - 2016-12-17

### Changed

- Updated Thrower to v4.

## [4.0.8] - 2016-12-04

### Changed

- Updated dependencies and reduced .NET Standard 1.1 references.
- Relicensed source code under MIT license (ISSUE#2 by Corniel Nobel).

## [4.0.7] - 2016-10-30

### Changed

- Updated Thrower to v3.0.4 (PR#1 by Corniel Nobel).
- Updated NUnit to v3.5.0 (PR#1 by Corniel Nobel).
- Default seed now takes into account process ID.
- Default seed now [uses a GUID](http://stackoverflow.com/a/18267477/1880086) to improve randomness.
- Updated broken Google-hosted links.
- Fixed a bug in CategoricalDistribution. Weights were not normalized correctly.
- Changed how ParetoDistribution is computed. Now a transformation based on exponential is used.

## [4.0.5] - 2016-09-18

### Changed

- Added new .NET Standard 1.1 library.

[5.0.0]: https://gitlab.com/pomma89/troschuetz-random/-/compare/4.4.0...5.0.0
[4.4.0]: https://gitlab.com/pomma89/troschuetz-random/-/compare/4.3.3...4.4.0
[4.3.3]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.3.1...4.3.3
[4.3.1]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.3.0...v4.3.1
[4.3.0]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.2.0...v4.3.0
[4.2.0]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.1.3...v4.2.0
[4.1.3]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.1.1...v4.1.3
[4.1.1]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.0.8...v4.1.1
[4.0.8]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.0.7...v4.0.8
[4.0.7]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.0.5...v4.0.7
[4.0.5]: https://gitlab.com/pomma89/troschuetz-random/-/compare/v4.0.4...v4.0.5
