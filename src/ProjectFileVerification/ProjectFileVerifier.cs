namespace dotnet_dependency_version_checker.ProjectFileVerification;

public class ProjectFileVerifier
{
    public static bool IsValidCsprojPath(string? path)
    { 
        return File.Exists(path) && Path.GetExtension(path).Equals(".csproj", StringComparison.OrdinalIgnoreCase);
    }
}