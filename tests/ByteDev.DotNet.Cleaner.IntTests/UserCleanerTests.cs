using System.IO;
using ByteDev.Testing.Builders;
using ByteDev.Testing.NUnit;
using NSubstitute;
using NUnit.Framework;

namespace ByteDev.DotNet.Cleaner.IntTests;

[TestFixture]
public class UserCleanerTests
{
    private DirectoryInfo _baseDir;

    private UserCleaner _sut;
    private UserCleanerOptions _options;
    private IEnvironmentVariableProvider _envProvider;

    [SetUp]
    public void SetUp()
    {
        _baseDir = DirectoryBuilder.InFileSystem
            .WithPath(TestDirs.TestBasePath)
            .EmptyIfExists()
            .Build();

        _options = new UserCleanerOptions();

        _envProvider = Substitute.For<IEnvironmentVariableProvider>();

        _sut = new UserCleaner(_options, _envProvider);
    }

    [TestFixture]
    public class Clean_NugetHttpCache : UserCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndNugetHttpCacheDirExists_ThenDoNothing()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcache.txt")).Build();

            _options.DeleteNugetHttpCache = false;
            WhenNugetHttpCachePathExists();
            
            _sut.Clean();

            AssertDir.Exists(dir);
            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsFalse_AndNugetHttpCacheDirNotExists_ThenDoNothing()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcache.txt")).Build();

            _options.DeleteNugetHttpCache = true;
            WhenNugetHttpCachePathNotExists();

            _sut.Clean();

            AssertDir.Exists(dir);
            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsTrue_AndNugetHttpCacheDirExists_ThenEmptyDir()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugethttpcache.txt")).Build();

            _options.DeleteNugetHttpCache = true;
            WhenNugetHttpCachePathExists();

            _sut.Clean();

            AssertDir.NotExists(dir);
            AssertFile.NotExists(file);
        }
    }

    [TestFixture]
    public class Clean_NugetPluginsCache : UserCleanerTests
    {
        [Test]
        public void WhenOptionIsFalse_AndNugetPluginsCacheDirExists_ThenDoNothing()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscache.txt")).Build();

            _options.DeleteNugetPluginsCache = false;
            WhenNugetPluginsCachePathExists();

            _sut.Clean();

            AssertDir.Exists(dir);
            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsFalse_AndNugetPluginsCacheDirNotExists_ThenDoNothing()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscache.txt")).Build();

            _options.DeleteNugetPluginsCache = true;
            WhenNugetPluginsCachePathNotExists();

            _sut.Clean();

            AssertDir.Exists(dir);
            AssertFile.Exists(file);
        }

        [Test]
        public void WhenOptionIsTrue_AndNugetPluginsCacheDirExists_ThenEmptyDir()
        {
            var dir = DirectoryBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscachedir")).Build();
            var file = FileBuilder.InFileSystem.WithPath(Path.Combine(_baseDir.FullName, "nugetpluginscache.txt")).Build();

            _options.DeleteNugetPluginsCache = true;
            WhenNugetPluginsCachePathExists();

            _sut.Clean();

            AssertDir.NotExists(dir);
            AssertFile.NotExists(file);
        }
    }

    private void WhenNugetHttpCachePathExists()
    {
        _envProvider.GetNugetHttpCachePath().Returns(TestDirs.TestBasePath);
    }

    private void WhenNugetHttpCachePathNotExists()
    {
        _envProvider.GetNugetHttpCachePath().Returns(null as string);
    }

    private void WhenNugetPluginsCachePathExists()
    {
        _envProvider.GetNugetPluginsCachePath().Returns(TestDirs.TestBasePath);
    }

    private void WhenNugetPluginsCachePathNotExists()
    {
        _envProvider.GetNugetPluginsCachePath().Returns(null as string);
    }
}