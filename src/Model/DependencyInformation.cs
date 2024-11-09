namespace dotnet_dependency_version_checker.model;

public class DependencyInformation
{
        public string DependencyName;
        public string CurrentVersion;
        public string? LatestStableVersion;
        public string? LatestPreviewVersion;
        public bool IsUpToDate;

        public DependencyInformation()
        {
        }

        public DependencyInformation(string dependencyName, string currentVersion, string latestStableVersion, string latestPreviewVersion, bool isUpToDate)
        {
                DependencyName = dependencyName;
                CurrentVersion = currentVersion;
                LatestStableVersion = latestStableVersion;
                LatestPreviewVersion = latestPreviewVersion;
                IsUpToDate = isUpToDate;
        }
}