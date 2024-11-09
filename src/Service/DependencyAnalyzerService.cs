using System.Text.RegularExpressions;
using dotnet_dependency_version_checker.model;

namespace dotnet_dependency_version_checker.service;

public class DependencyAnalyzerService
{
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
            .Select(v => new Version(v.Split('-')[0]))  // Take the main version part for comparison
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
                LatestStableVersion = "N/A",
                LatestPreviewVersion = "N/A",
                IsUpToDate = false
            });
        }
    }

    return analysisResult;
}

        // Helper method to compare version strings (returns -1 if v1 < v2, 0 if equal, 1 if v1 > v2)
        private int CompareVersions(string version1, string version2)
        {
            var v1Parts = version1.Split('.').Select(int.Parse).ToArray();
            var v2Parts = version2.Split('.').Select(int.Parse).ToArray();

            int length = Math.Max(v1Parts.Length, v2Parts.Length);
            for (int i = 0; i < length; i++)
            {
                int part1 = i < v1Parts.Length ? v1Parts[i] : 0;
                int part2 = i < v2Parts.Length ? v2Parts[i] : 0;

                if (part1 < part2) return -1;
                if (part1 > part2) return 1;
            }
            return 0;
        }
}