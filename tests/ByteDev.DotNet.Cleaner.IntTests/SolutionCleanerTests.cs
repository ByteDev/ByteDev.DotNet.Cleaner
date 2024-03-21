using System.IO;
using ByteDev.Testing.Builders;
using ByteDev.Testing.NUnit;
using NUnit.Framework;

namespace ByteDev.DotNet.Cleaner.IntTests;

[TestFixture]
public class SolutionCleanerTests
{
    private const string BaseDirectoryPathNotExist = @"C:\a36e74f8faff45669a16569421ff6b6e";

    private SolutionCleaner _sut;
    private SolutionCleanerOptions _options;

    private DirectoryInfo _baseDir;
    private DirectoryInfo _subDir1;

    [SetUp]
    public void SetUp()
    {
        _baseDir = DirectoryBuilder.InFileSystem
            .WithPath(TestDirs.TestBasePath)
            .EmptyIfExists()
            .Build();

        _subDir1 = DirectoryBuilder.InFileSystem
            .WithPath(Path.Combine(_baseDir.FullName, "SubDir1"))
            .EmptyIfExists()
            .Build();

        _options = new SolutionCleanerOptions();

        _sut = new SolutionCleaner(_options);
    }

    [TestFixture]
    public class Clean_DotNetSettingsUserFile : SolutionCleanerTests
    {
        [Test]
        public void WhenBaseDirDoesNotExist_AndOptionIsTrue_ThenThrowException()
        {
            _options.DeleteDotSettingsUserFiles = true;

            Assert.Throws<DirectoryNotFoundException>(() => _sut.Clean(BaseDirectoryPathNotExist));
        }

        [Test]
        public void WhenOptionIsFalse_AndDotNetSettingsUserFileExists_ThenDoNotDelete()
        {
            var file = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, "Test1.DotSettings.user"))
                .Build();

            _options.DeleteDotSettingsUserFiles = false;

            _sut.Clean(_baseDir);

            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsTrue_AndDotNetSettingsUserFilesExistInBaseDir_ThenDelete()
        {
            var file1 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, "Test1.DotSettings.user"))
                .Build();

            var file2 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, "Test2.sln.DotSettings.user"))
                .Build();
            
            _options.DeleteDotSettingsUserFiles = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(file1);
            AssertFile.NotExists(file2);
        }

        [Test]
        public void WhenOptionIsTrue_AndDotNetSettingsUserFilesExistInSubDir_ThenDelete()
        {
            var file1 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, "Test1.DotSettings.user"))
                .Build();

            var file2 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, _subDir1.FullName, "Test2.DotSettings.user"))
                .Build();
            
            _options.DeleteDotSettingsUserFiles = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(file1);
            AssertFile.NotExists(file2);
        }
    }

    [TestFixture]
    public class Clean_NCrunchSolutionFile : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndNCrunchSolutionFileExists_ThenDoNotDelete()
        {
            var file = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, ".ncrunchsolution"))
                .Build();

            _options.DeleteNCrunchSolutionFiles = false;

            _sut.Clean(_baseDir);

            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsTrue_AndNCrunchSolutionFilesExistInBaseDir_ThenDelete()
        {
            var file1 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, ".ncrunchsolution"))
                .Build();

            var file2 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, ".v3.ncrunchsolution"))
                .Build();

            _options.DeleteNCrunchSolutionFiles = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(file1);
            AssertFile.NotExists(file2);
        }

        [Test]
        public void WhenOptionIsTrue_AndNCrunchSolutionFilesExistInSubDir_ThenDelete()
        {
            var file1 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, ".ncrunchsolution"))
                .Build();

            var file2 = FileBuilder.InFileSystem
                .WithPath(Path.Combine(_baseDir.FullName, _subDir1.FullName, ".v3.ncrunchsolution"))
                .Build();

            _options.DeleteNCrunchSolutionFiles = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(file1);
            AssertFile.NotExists(file2);
        }
    }

    [TestFixture]
    public class Clean_BinDir : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndBinDirsExist_ThenDoNotDeleteBinDirs()
        {
            var binDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "bin")).Build();
            var binDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\bin")).Build();
            var binDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\bin")).Build();

            _options.DeleteBinDirectories = false;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(binDir1);
            AssertDir.Exists(binDir2);
            AssertDir.Exists(binDir3);
        }

        [Test]
        public void WhenOptionIsTrue_AndBinDirsExist_ThenDeleteBinDirs()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();
            var binDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "bin")).Build();
            var binDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\bin")).Build();
            var binDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\bin")).Build();

            _options.DeleteBinDirectories = true;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(other);
            AssertDir.NotExists(binDir1);
            AssertDir.NotExists(binDir2);
            AssertDir.NotExists(binDir3);
        }
    }

    [TestFixture]
    public class Clean_ObjDir : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndObjDirsExist_ThenDoNotDeleteObjDirs()
        {
            var objDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "obj")).Build();
            var objDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\obj")).Build();
            var objDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\obj")).Build();

            _options.DeleteObjDirectories = false;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(objDir1);
            AssertDir.Exists(objDir2);
            AssertDir.Exists(objDir3);
        }

        [Test]
        public void WhenOptionIsTrue_AndObjDirsExist_ThenDeleteObjDirs()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();
            var objDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "obj")).Build();
            var objDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\obj")).Build();
            var objDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\obj")).Build();

            _options.DeleteObjDirectories = true;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(other);
            AssertDir.NotExists(objDir1);
            AssertDir.NotExists(objDir2);
            AssertDir.NotExists(objDir3);
        }
    }

    [TestFixture]
    public class Clean_DotVsDir : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndDotVsDirsExist_ThenDoNotDeleteDotVsDirs()
        {
            var dotVs1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".vs")).Build();
            var dotVs2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\.vs")).Build();
            var dotVs3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\.vs")).Build();

            _options.DeleteDotVsDirectories = false;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(dotVs1);
            AssertDir.Exists(dotVs2);
            AssertDir.Exists(dotVs3);
        }

        [Test]
        public void WhenOptionIsTrue_AndDotVsDirsExist_ThenDeleteDotVsDirs()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();
            var dotVs1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".vs")).Build();
            var dotVs2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\.vs")).Build();
            var dotVs3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\.vs")).Build();

            _options.DeleteDotVsDirectories = true;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(other);
            AssertDir.NotExists(dotVs1);
            AssertDir.NotExists(dotVs2);
            AssertDir.NotExists(dotVs3);
        }
    }

    [TestFixture]
    public class Clean_GitDir : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsTrue_AndGitDirDoesNotExist_ThenDoNothing()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();

            _options.Git.DeleteGitDirectory = true;

            _sut.Clean(_baseDir);

            AssertDir.Exists(other);
        }

        [Test]
        public void WhenOptionIsTrue_AndGitDirExists_ThenDeleteGitDir()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();
            var gitDir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".git")).Build();
            var gitFile = FileBuilder.InFileSystem.WithPath(Path.Combine(gitDir.FullName, "something.txt")).Build();

            _options.Git.DeleteGitDirectory = true;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(other);
            AssertDir.NotExists(gitDir);
            AssertFile.NotExists(gitFile);
        }

        [Test]
        public void WhenOptionsIsFalse_AndGitDirExists_ThenDoNotDeleteGitDir()
        {
            var gitDir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".git")).Build();
            var gitFile = FileBuilder.InFileSystem.WithPath(Path.Combine(gitDir.FullName, "something.txt")).Build();

            _options.Git.DeleteGitDirectory = false;

            _sut.Clean(_baseDir);
            
            AssertDir.Exists(gitDir);
            AssertFile.Exists(gitFile);
        }
    }

    [TestFixture]
    public class Clean_GitAttributesFile : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsTrue_AndGitAttributesDoesNotExist_ThenDoNothing()
        {
            var other = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "something.txt")).Build();

            _options.Git.DeleteGitAttributesFile = true;

            _sut.Clean(_baseDir);

            AssertFile.Exists(other);
        }

        [Test]
        public void WhenOptionIsTrue_AndGitAttributesExists_ThenDeleteGitAttributesFile()
        {
            var gitAttributesFile = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".gitattributes")).Build();

            _options.Git.DeleteGitAttributesFile = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(gitAttributesFile);
        }

        [Test]
        public void WhenOptionIsFalse_AndGitAttributesExists_ThenDoNotDeleteGitAttributesFile()
        {
            var gitAttributesFile = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".gitattributes")).Build();

            _options.Git.DeleteGitAttributesFile = false;

            _sut.Clean(_baseDir);

            AssertFile.Exists(gitAttributesFile);
        }
    }

    [TestFixture]
    public class Clean_GitIgnoreFile : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsTrue_AndGitIgnoreDoesNotExist_ThenDoNothing()
        {
            var other = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "something.txt")).Build();

            _options.Git.DeleteGitIgnoreFile = true;

            _sut.Clean(_baseDir);

            AssertFile.Exists(other);
        }

        [Test]
        public void WhenOptionIsTrue_AndGitIgnoreExists_ThenDeleteGitIgnoreFile()
        {
            var gitIgnoreFile = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".gitignore")).Build();

            _options.Git.DeleteGitIgnoreFile = true;

            _sut.Clean(_baseDir);

            AssertFile.NotExists(gitIgnoreFile);
        }

        [Test]
        public void WhenOptionIsFalse_AndGitIgnoreExists_ThenDoNotDeleteGitIgnoreFile()
        {
            var gitIgnoreFile = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, ".gitignore")).Build();

            _options.Git.DeleteGitIgnoreFile = false;

            _sut.Clean(_baseDir);

            AssertFile.Exists(gitIgnoreFile);
        }
    }

    [TestFixture]
    public class Clean_NugetPackagesDir : SolutionCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndNugetPackagesDirsExist_ThenDoNotDeleteBinDirs()
        {
            var binDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "packages")).Build();
            var binDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\packages")).Build();
            var binDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\packages")).Build();

            _options.DeleteNugetPackagesDirectories = false;

            _sut.Clean(_baseDir);

            AssertDir.Exists(binDir1);
            AssertDir.Exists(binDir2);
            AssertDir.Exists(binDir3);
        }

        [Test]
        public void WhenOptionIsTrue_AndNugetPackagesDirsExist_ThenDeleteBinDirs()
        {
            var other = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "other")).Build();
            var binDir1 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "packages")).Build();
            var binDir2 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj1\packages")).Build();
            var binDir3 = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, @"MyProj2\Something\packages")).Build();

            _options.DeleteNugetPackagesDirectories = true;

            _sut.Clean(_baseDir);

            AssertDir.Exists(other);
            AssertDir.NotExists(binDir1);
            AssertDir.NotExists(binDir2);
            AssertDir.NotExists(binDir3);
        }
    }
}