using System;
using System.IO;
using ByteDev.Io;

namespace ByteDev.DotNet.Cleaner
{
    /// <summary>
    /// Represents a type to clean various files and directories at the user level.
    /// </summary>
    public class UserCleaner : IUserCleaner
    {
        private readonly UserCleanerOptions _options;
        private readonly IEnvironmentVariableProvider _envVarProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.DotNet.Cleaner.UserCleaner" /> class.
        /// </summary>
        /// <param name="options">Cleaner options.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="options" /> is null.</exception>
        public UserCleaner(UserCleanerOptions options) : this(options, new EnvironmentVariableProvider())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.DotNet.Cleaner.UserCleaner" /> class.
        /// </summary>
        /// <param name="options">Cleaner options.</param>
        /// <param name="envVarProvider">Environment variable provider.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="options" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="envVarProvider" /> is null.</exception>
        public UserCleaner(UserCleanerOptions options, 
            IEnvironmentVariableProvider envVarProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _envVarProvider = envVarProvider ?? throw new ArgumentNullException(nameof(envVarProvider));
        }

        /// <summary>
        /// Perform a clean for the user based on the provided options.
        /// </summary>
        public void Clean()
        {
            if (_options.DeleteNugetHttpCache)
                DeleteNugetHttpCache();

            if (_options.DeleteNugetPluginsCache)
                DeleteNugetPluginsCache();
        }

        private void DeleteNugetHttpCache()
        {
            var nugetHttpCachePath = _envVarProvider.GetNugetHttpCachePath();

            if (!string.IsNullOrEmpty(nugetHttpCachePath))
                new DirectoryInfo(nugetHttpCachePath).EmptyIfExists();
        }

        private void DeleteNugetPluginsCache()
        {
            var nugetPluginsCachePath = _envVarProvider.GetNugetPluginsCachePath();

            if (!string.IsNullOrEmpty(nugetPluginsCachePath))
                new DirectoryInfo(nugetPluginsCachePath).EmptyIfExists();
        }
    }
}