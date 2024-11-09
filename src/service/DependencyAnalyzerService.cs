using dotnet_dependency_version_checker.model;

namespace dotnet_dependency_version_checker.service;

public class DependencyAnalyzerService
{
    /// <summary>
    /// Checks each project dependency and its current version to determine if a newer stable version is available.
    /// If a newer version exists, mark the dependency as deprecated.
    /// If the current version is the latest, mark the dependency as up-to-date.
    /// If a newer preview version is available, include it as additional information.
    /// </summary>
    /// <param name="dependencyList">List of project dependencies with version number</param>
    /// <param name="dependencyVersionList">List of dependencies with all available versions</param>
 public List<DependencyInformation> AnalyzeDependencies(
        Dictionary<string, string> dependencyList,
        Dictionary<string, List<string>> dependencyVersionList)
    {
        var analysisResult = new List<DependencyInformation>();

        foreach (var (dependencyName, currentVersion) in dependencyList)
        {
            if (dependencyVersionList.TryGetValue(dependencyName, out var versions))
            {
                // Separate stable and preview versions
                var stableVersions = versions
                    .Where(v => !v.Contains("-preview"))
                    .OrderBy(v => new Version(v))
                    .ToList();
                
                var previewVersions = versions
                    .Where(v => v.Contains("-preview"))
                    .OrderBy(v => new Version(v.Split("-preview")[0]))  // Sort by the major version part
                    .ToList();

                // Determine latest stable and preview versions
                var latestStableVersion = stableVersions.LastOrDefault();
                var latestPreviewVersion = previewVersions.LastOrDefault();

                // Determine if the dependency is up-to-date or outdated
                bool isUpToDate = latestStableVersion != null && currentVersion == latestStableVersion;

                analysisResult.Add(new DependencyInformation
                {
                    DependencyName = dependencyName,
                    CurrentVersion = currentVersion,
                    LatestStableVersion = latestStableVersion,
                    LatestPreviewVersion = latestPreviewVersion,
                    IsUpToDate = isUpToDate
                });
            }
            else
            {
                // If no version list is found for the dependency, mark as outdated
                analysisResult.Add(new DependencyInformation
                {
                    DependencyName = dependencyName,
                    CurrentVersion = currentVersion,
                    LatestStableVersion = "Unknown",
                    LatestPreviewVersion = null,
                    IsUpToDate = false
                });
            }
        }

        return analysisResult;
    }
}