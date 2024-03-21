using NUnit.Framework;

namespace ByteDev.DotNet.Cleaner.UnitTests;

[TestFixture]
public class SolutionCleanerOptionsTests
{
    [Test]
    public void WhenGitPropertySetToNull_ThenReturnNotNull()
    {
        var sut = new SolutionCleanerOptions
        {
            Git = null
        };

        Assert.That(sut.Git, Is.Not.Null);
    }
}