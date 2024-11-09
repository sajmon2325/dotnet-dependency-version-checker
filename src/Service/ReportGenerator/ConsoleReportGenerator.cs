using dotnet_dependency_version_checker.model;

namespace dotnet_dependency_version_checker.service.ReportGenerator;

public class ConsoleReportGenerator : IReportGenerator
{
    public void GenerateReport(List<DependencyInformation> dependencyInformation)
    {
        foreach (var dependency in dependencyInformation)
        {
            Console.Write($"{dependency.DependencyName} version {dependency.CurrentVersion} ");

            if (dependency.IsUpToDate)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("is up to date");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("is outdated");
            }

            // Reset the console color after each message
            Console.ResetColor();
        }
    }
}