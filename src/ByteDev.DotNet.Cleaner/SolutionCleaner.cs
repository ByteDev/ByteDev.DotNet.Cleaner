using System;
using System.IO;
using ByteDev.Io;

namespace ByteDev.DotNet.Cleaner
{
    /// <summary>
    /// Represents a type to clean various files and directories at the (.NET) solution level.
    /// </summary>
    public class SolutionCleaner : ISolutionCleaner
    {
        private readonly SolutionCleanerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.DotNet.Cleaner.SolutionCleaner" /> class.
        /// </summary>
        /// <param name="options">Cleaner options.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="options" /> is null.</exception>
        public SolutionCleaner(SolutionCleanerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Clean the solution in the specified base directory based on the provided options.
        /// </summary>
        /// <param name="baseDirectory">Base directory of the solution.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="baseDirectory" /> is null.</exception>
        public void Clean(DirectoryInfo baseDirectory)
        {
            if (baseDirectory == null)
                throw new ArgumentNullException(nameof(baseDirectory));

            Clean(baseDirectory.FullName);
        }

        /// <summary>
        /// Perform a clean on the solution in the specified base directory based on the provided options.
        /// </summary>
        /// <param name="baseDirectoryPath">Path to the base directory.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="baseDirectoryPath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="baseDirectoryPath" /> is empty.</exception>
        public void Clean(string baseDirectoryPath)
        {
            var baseDir = new DirectoryInfo(baseDirectoryPath);

            if (_options.DeleteDotSettingsUserFiles)
                baseDir.DeleteFiles(FileExtensions.DotSettingsUser, true);

            if (_options.DeleteNCrunchSolutionFiles)
                baseDir.DeleteFiles(FileExtensions.NCrunchSolution, true);

            if (_options.DeleteBinDirectories)
                baseDir.DeleteDirectoriesWithName("bin");

            if (_options.DeleteObjDirectories)
                baseDir.DeleteDirectoriesWithName("obj");

            if (_options.DeleteDotVsDirectories)
                baseDir.DeleteDirectoriesWithName(".vs");

            if (_options.DeleteNugetPackagesDirectories)
                baseDir.DeleteDirectoriesWithName("packages");

            CleanGit(baseDir);
        }

        private void CleanGit(DirectoryInfo baseDir)
        {
            if (_options.Git.DeleteGitDirectory)
            {
                var gitDir = new DirectoryInfo(Path.Combine(baseDir.FullName, ".git"));
                gitDir.DeleteIfExists();
            }

            if (_options.Git.DeleteGitAttributesFile)
            {
                var gitAttributesFile = new FileInfo(Path.Combine(baseDir.FullName, ".gitattributes"));
                gitAttributesFile.DeleteIfExists();
            }

            if (_options.Git.DeleteGitIgnoreFile)
            {
                var gitIgnoreFile = new FileInfo(Path.Combine(baseDir.FullName, ".gitignore"));
                gitIgnoreFile.DeleteIfExists();
            }
        }
    }
}