using dotnet_dependency_version_checker.model;
using HandlebarsDotNet;

namespace dotnet_dependency_version_checker.service.ReportGenerator;

public class WebReportGenerator : IReportGenerator
{
    
    public void GenerateReport(List<DependencyInformation> dependencyInformation)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, 
            "src", "Content", "Template", "WebReportTemplate.html");
        
        // Register custom helper in Handlebars
        Handlebars.RegisterHelper("conditionalStyle", (writer, context, parameters) =>
        {
            var isUpToDate = (bool)parameters[0];  // The first parameter must be IsUpToDate (boolean)
            writer.WriteSafeString(isUpToDate ? "font-weight: bold; color: green;" : "font-weight: bold; color: red;");
        });
        
        var templateContent = File.ReadAllText(templatePath);
        
        var template = Handlebars.Compile(templateContent);
        var resultHtml = template(dependencyInformation);
        
        var outputPath = Path.Combine(AppContext.BaseDirectory, "report.html");
        File.WriteAllText(outputPath, resultHtml);
        Console.WriteLine("\nHTML report generated at: " + outputPath);

    }
}