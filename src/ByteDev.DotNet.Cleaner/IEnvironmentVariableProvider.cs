namespace ByteDev.DotNet.Cleaner
{
    public interface IEnvironmentVariableProvider
    {
        string GetNugetHttpCachePath();

        string GetNugetPluginsCachePath();
    }
}