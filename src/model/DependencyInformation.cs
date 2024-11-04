namespace dotnet_dependency_version_checker.model;

public class DependencyInformation(
    Dictionary<string, string> projectDependencies,
    Dictionary<string, List<string>> dependenciesVersions)
{
    private Dictionary<string, string> _projectDependencies = projectDependencies;
    private Dictionary<string, List<string>> _dependenciesVersions = dependenciesVersions;
}