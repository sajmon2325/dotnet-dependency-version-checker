using dotnet_dependency_version_checker.content;
using dotnet_dependency_version_checker.MenuOptions;
using dotnet_dependency_version_checker.ProjectFileVerification;
using dotnet_dependency_version_checker.service.ReportGenerator;

Console.WriteLine(TextContent.MainTitle);

var menuOptionControls = new MenuOptionControls();
var displayMainMenu = true;

do
{
    var csProjFilePath = menuOptionControls.DisplayProjFilePathPrompt();

    if (ProjectFileVerifier.IsValidCsprojPath(csProjFilePath))
    {
        var dependencyInformation = await menuOptionControls.HandleDisplayDependencyVersionCheck(csProjFilePath);
        Console.WriteLine("\n");

        // Generate console report for the result of dependency analysis
        var consoleReportGenerator = new ConsoleReportGenerator();
        consoleReportGenerator.GenerateReport(dependencyInformation);
        
        // Display menu option to generate web report for dependency analysis
        ShowMainMenu(false);
    }
    else
    {
        MenuOptionControls.HandleInvalidProjPathPrompt(ShowMainMenu);
    }
} while (displayMainMenu);

Environment.Exit(0);
return;


void ShowMainMenu(bool showMainMenu)
{
    displayMainMenu = showMainMenu;
}



