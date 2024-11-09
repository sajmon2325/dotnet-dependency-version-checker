
using System.Text.Json;

namespace dotnet_dependency_version_checker.service;

public class NugetApiClient
{
    private const string NugetBaseUrl = "https://api.nuget.org/v3/index.json";
    private const string ServiceIndexUrlSuffix = "/index.json";

    /// <summary>
    /// Fetches the service index url from Nuget API endpoint
    /// </summary>
    private async Task<string?> GetServiceIndexUrl()
    {
        try
        {
            using var client = new HttpClient();
            var serviceIndexResponse = await client.GetStringAsync(NugetBaseUrl);

            var jsonDocument = JsonDocument.Parse(serviceIndexResponse);
            var rootElement = jsonDocument.RootElement;

            //Loop through "resources" array to find PackageBaseAddress
            if (rootElement.TryGetProperty("resources", out JsonElement resources))
            {
                foreach (var resource in resources.EnumerateArray())
                {
                    if (!resource.TryGetProperty("@type", out JsonElement type) ||
                        type.GetString() != "PackageBaseAddress/3.0.0") continue;

                    if (!resource.TryGetProperty("@id", out JsonElement id)) continue;

                    return @id.GetString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching Service Index URL: {ex.Message}");
        }

        return null;
    }
    /// <summary>
    /// Takes the collection of project dependency names and fetches version numbers from Nuget API
    /// </summary>
    /// <param name="packageNames">Collection of full names of project dependencies</param>
    public async Task<Dictionary<string, List<string>>> GetDependencyVersions(IEnumerable<string> packageNames)
    {
        var result = new Dictionary<string, List<string>>();
        var packageBaseAddress = await GetServiceIndexUrl();

        if (string.IsNullOrEmpty(packageBaseAddress))
        {
            throw new InvalidOperationException("Package base address is invalid or could not be retrieved.");
        }
        
        foreach (var packageName in packageNames)
        {
            var url = $"{packageBaseAddress}{packageName.ToLower()}{ServiceIndexUrlSuffix}";
            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            
            var jsonResponse = JsonDocument.Parse(response);
            var rootElement = jsonResponse.RootElement;

            if (!rootElement.TryGetProperty("versions", out JsonElement versions))
            {
                throw new InvalidOperationException("The 'versions' array is missing in the NuGet API response.");
            }
            
            result.Add(packageName, versions.EnumerateArray().Select(version => version.ToString()).ToList());
        }

        return result;
    }
}