[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.DotNet.Cleaner?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-DotNet-Cleaner/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.DotNet.Cleaner.svg)](https://www.nuget.org/packages/ByteDev.DotNet.Cleaner)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.DotNet.Cleaner/blob/master/LICENSE)

# ByteDev.DotNet.Cleaner

Provides functionality to quickly clean .NET solutions, git repositories, user info, etc.

## Installation

ByteDev.DotNet.Cleaner is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.DotNet.Cleaner`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.DotNet.Cleaner/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.DotNet.Cleaner/blob/master/docs/RELEASE-NOTES.md).

## Usage

### SolutionCleaner

The `SolutionCleaner` type can be used to clean various files and directories at the (.NET) solution level.

Example:

```csharp
var options = new SolutionCleanerOptions
{
	DeleteDotSettingsUserFiles = true,
	DeleteNCrunchSolutionFiles = true,
	DeleteBinDirectories = true,
	DeleteObjDirectories = true,
	DeleteDotVsDirectories = true,
	DeleteNugetPackagesDirectories = true,
	Git = new SolutionCleanerGitOptions
	{
		DeleteGitDirectory = true,
		DeleteGitAttributesFile = true,
		DeleteGitIgnoreFile = true
	}
};

var cleaner = new DotNetSolutionCleaner(options);

cleaner.Clean(@"C:\MyDotNetApp");
```

### UserCleaner

The `UserCleaner` type can be used to clean various files and directories at the user level.

```csharp
var options = new UserCleanerOptions
{
	DeleteNugetHttpCache = true,
	DeleteNugetPluginsCache = true
};

var clean = new UserCleaner(options);

cleaner.Clean();
```