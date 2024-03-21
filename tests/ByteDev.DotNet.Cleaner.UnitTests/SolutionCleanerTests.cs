using System;
using System.IO;
using NUnit.Framework;

namespace ByteDev.DotNet.Cleaner.UnitTests;

[TestFixture]
public class SolutionCleanerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void WhenOptionsIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new SolutionCleaner(null));
        }
    }

    [TestFixture]
    public class Clean : SolutionCleanerTests
    {
        private SolutionCleaner _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new SolutionCleaner(new SolutionCleanerOptions());
        }

        [Test]
        public void WhenDirInfoIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Clean(null as DirectoryInfo));
        }

        [Test]
        public void WhenIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Clean(null as string));
        }

        [Test]
        public void WhenIsEmpty_ThenThrowException()
        {
            Assert.Throws<ArgumentException>(() => _sut.Clean(string.Empty));
        }
    }
}