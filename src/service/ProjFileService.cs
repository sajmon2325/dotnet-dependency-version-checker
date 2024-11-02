using System.Xml;
using System.Xml.Linq;

namespace dotnet_dependency_version_checker.service;

public class ProjFileService
{
    public Dictionary<string, string> ParseProjFile(string projPath)
    {
        var document = XDocument.Load(projPath);
        var itemGroups = document.Elements("Project").Elements("ItemGroup");

        var dependencyList = new Dictionary<string, string>();

        foreach (var itemGroup in itemGroups)
        {
            foreach (var element in itemGroup.Elements("PackageReference"))
            {
                var packageName = element.Attribute("Include")?.Value;
                var version = element.Attribute("Version")?.Value;

                if (!string.IsNullOrEmpty(packageName) && !string.IsNullOrEmpty(version))
                {
                    dependencyList[packageName] = version;
                }
            }
        }
        
        return dependencyList;
    }
}