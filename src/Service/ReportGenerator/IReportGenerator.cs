using dotnet_dependency_version_checker.model;

namespace dotnet_dependency_version_checker.service.ReportGenerator;

public interface IReportGenerator
{
    void GenerateReport(List<DependencyInformation> dependencyInformation);
}