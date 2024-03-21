namespace ByteDev.DotNet.Cleaner
{
    /// <summary>
    /// Represents options for the <see cref="T:ByteDev.DotNet.Cleaner.UserCleaner" />.
    /// </summary>
    public class UserCleanerOptions
    {
        /// <summary>
        /// Empties the NuGet manager's HTTP downloaded cache at the user level. False by default.
        /// </summary>
        public bool DeleteNugetHttpCache { get; set; }

        /// <summary>
        /// Empties the NuGet plugin cache at the user level. False by default.
        /// </summary>
        public bool DeleteNugetPluginsCache { get; set; }
    }
}