using System;
using System.IO;

namespace ByteDev.DotNet.Cleaner
{
    /// <summary>
    /// Represents a provider for reading environment variables.
    /// </summary>
    public class EnvironmentVariableProvider : IEnvironmentVariableProvider
    {
        public string GetNugetHttpCachePath()
        {
            var localAppDataPath = GetAppDataLocal();

            return localAppDataPath == null ? null : Path.Combine(localAppDataPath, @"NuGet\v3-cache");
            // C:\Users\<USER>\AppData\Local\NuGet\v3-cache
        }

        public string GetNugetPluginsCachePath()
        {
            var localAppDataPath = GetAppDataLocal();

            return localAppDataPath == null ? null : Path.Combine(localAppDataPath, @"NuGet\plugins-cache");
            // C:\Users\<USER>\AppData\Local\NuGet\plugins-cache
        }

        private static string GetAppDataLocal()
        {
            return Environment.GetEnvironmentVariable("localappdata");
            // C:\Users\<USER>\AppData\Local
        }
    }
}