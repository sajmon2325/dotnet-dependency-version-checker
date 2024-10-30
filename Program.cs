using dotnet_dependency_version_checker.content;
using dotnet_dependency_version_checker.MenuOptions;
using dotnet_dependency_version_checker.ProjectFileVerification;

Console.WriteLine(TextContent.MainTitle);

// Console.WriteLine(TextContent.CsprojFilePrompt);
//
// var csProjFilePath = Console.ReadLine();
var menuOptionControls = new MenuOptionControls();
var displayMainMenu = true;

var csProjFilePath = menuOptionControls.DisplayProjFilePathPrompt();

do
{
    if (ProjectFileVerifier.IsValidCsprojPath(csProjFilePath))
    {
        
    }
    else
    {
        menuOptionControls.HandleInvalidProjPathPrompt(csProjFilePath, ShowMainMenu);
    }
} while (displayMainMenu);
Environment.Exit(0);


void ShowMainMenu(bool showMainMenu)
{
    displayMainMenu = showMainMenu;
}



