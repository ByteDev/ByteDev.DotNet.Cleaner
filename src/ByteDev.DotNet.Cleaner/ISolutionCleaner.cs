using System.IO;

namespace ByteDev.DotNet.Cleaner
{
    public interface ISolutionCleaner
    {
        /// <summary>
        /// Clean the solution in the specified base directory based on the provided options.
        /// </summary>
        /// <param name="baseDirectory">Base directory of the solution.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="baseDirectory" /> is null.</exception>
        void Clean(DirectoryInfo baseDirectory);

        /// <summary>
        /// Perform a clean on the solution in the specified base directory based on the provided options.
        /// </summary>
        /// <param name="baseDirectoryPath">Path to the base directory.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="baseDirectoryPath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="baseDirectoryPath" /> is empty.</exception>
        void Clean(string baseDirectoryPath);
    }
}