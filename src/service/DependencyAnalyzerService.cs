namespace dotnet_dependency_version_checker.service;

public class DependencyAnalyzerService
{
    /// <summary>
    /// Checks each project dependency and its current version to determine if a newer stable version is available.
    /// If a newer version exists, mark the dependency as deprecated.
    /// If the current version is the latest, mark the dependency as up-to-date.
    /// If a newer minor version is available, mark the dependency as needing a minor update.
    /// </summary>
    /// <param name="dependencyList">List of project dependencies with version number</param>
    /// <param name="dependencyVersionList">List of dependencies with all available versions</param>
    public void AnalyzeDependencies(Dictionary<string, string> dependencyList, Dictionary<string, List<string>> dependencyVersionList)
    {
    }
}