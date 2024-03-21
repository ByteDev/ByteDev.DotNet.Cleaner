namespace ByteDev.DotNet.Cleaner
{
    /// <summary>
    /// Represents options for the <see cref="T:ByteDev.DotNet.Cleaner.SolutionCleaner" />.
    /// </summary>
    public class SolutionCleanerOptions
    {
        private SolutionCleanerGitOptions _git;

        /// <summary>
        /// Delete all files with ".DotSettings.user" extension. False by default.
        /// </summary>
        public bool DeleteDotSettingsUserFiles { get; set; }

        /// <summary>
        /// Delete all files with ".ncrunchsolution" extension. False by default.
        /// (.ncrunchsolution store NCrunch solution settings).
        /// </summary>
        public bool DeleteNCrunchSolutionFiles { get; set; }

        /// <summary>
        /// Delete all "bin" directories. False by default.
        /// ("bin" directories contain binary files (linked executable code for your application)).
        /// </summary>
        public bool DeleteBinDirectories { get; set; }

        /// <summary>
        /// Delete all "obj" directories. False by default.
        /// ("obj" directories contain object files (compiled binary files that have yet to be linked)).
        /// </summary>
        public bool DeleteObjDirectories { get; set; }

        /// <summary>
        /// Delete all ".vs" directories. False by default.
        /// (".vs" directories are Visual Studio user specific files (.suo and intellisense DB files etc.)).
        /// </summary>
        public bool DeleteDotVsDirectories { get; set; }

        /// <summary>
        /// Delete all nuget local cache "packages" directories. False by default.
        /// ("packages" directories are generally used by older .NET Framework applications).
        /// </summary>
        public bool DeleteNugetPackagesDirectories { get; set; }

        /// <summary>
        /// Git related options.
        /// </summary>
        public SolutionCleanerGitOptions Git
        {
            get => _git ?? (_git = new SolutionCleanerGitOptions());
            set => _git = value;
        }
    }
}