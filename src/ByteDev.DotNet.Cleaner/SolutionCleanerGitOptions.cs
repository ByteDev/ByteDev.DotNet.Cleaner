namespace ByteDev.DotNet.Cleaner
{
    public class SolutionCleanerGitOptions
    {
        /// <summary>
        /// Deletes ".git" directory from the base directory if it exists. False by default.
        /// </summary>
        public bool DeleteGitDirectory { get; set; }

        /// <summary>
        /// Deletes ".gitattributes" file from the base directory if it exists. False by default.
        /// </summary>
        public bool DeleteGitAttributesFile { get; set; }

        /// <summary>
        /// Deletes ".gitignore" file from the base directory if it exists. False by default.
        /// </summary>
        public bool DeleteGitIgnoreFile { get; set; }
    }
}