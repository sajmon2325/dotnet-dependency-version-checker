using dotnet_dependency_version_checker.model;

namespace dotnet_dependency_version_checker.service;

public class DependencyAnalyzerService {
    /// <summary>
    /// Checks each project dependency and its current version to determine if a newer stable version is available.
    /// If a newer version exists, mark the dependency as outdated.
    /// If the current version is the latest, mark the dependency as up to date.
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
        if (!dependencyVersionList.TryGetValue(dependencyName, out var versions)) continue;

        var stableVersions = versions
            .Where(v => !v.Contains('-'))
            .Select(v => new Version(v))
            .ToList();

        var preReleaseVersions = versions
            .Where(v => v.Contains('-'))
            .Select(v => new Version(v.Split('-')[0])) 
            .ToList();

        // If no stable versions are found, mark the status as "Outdated"
        if (stableVersions.Count > 0)
        {
            // Step 3: Find the latest stable version
            var latestStableVersion = stableVersions.Max();
            var latestPreviewVersion = preReleaseVersions.Count > 0 ? preReleaseVersions.Max() : null;

            // Compare the current version with the latest stable version
            var currentVersionObject = new Version(currentVersion);

            var dependencyInfo = new DependencyInformation
            {
                DependencyName = dependencyName,
                CurrentVersion = currentVersion,
                LatestStableVersion = latestStableVersion?.ToString(),
                LatestPreviewVersion = latestPreviewVersion?.ToString(),
                IsUpToDate = currentVersionObject.CompareTo(latestStableVersion) >= 0
            };

            analysisResult.Add(dependencyInfo);
        }
        else
        {
            analysisResult.Add(new DependencyInformation
            {
                DependencyName = dependencyName,
                CurrentVersion = currentVersion,
                LatestStableVersion = "Version not found",
                LatestPreviewVersion = "Version not found",
                IsUpToDate = false
            });
        }
    }

    return analysisResult;
    }
}