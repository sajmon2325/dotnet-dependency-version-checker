using dotnet_dependency_version_checker.content;
using dotnet_dependency_version_checker.MenuOptions;
using dotnet_dependency_version_checker.ProjectFileVerification;

Console.WriteLine(TextContent.MainTitle);

var menuOptionControls = new MenuOptionControls();
var displayMainMenu = true;

do
{
    var csProjFilePath = menuOptionControls.DisplayProjFilePathPrompt();

    if (ProjectFileVerifier.IsValidCsprojPath(csProjFilePath))
    {
        menuOptionControls.HandleDisplayDependencyVersionCheck(csProjFilePath);
    }
    else
    {
        MenuOptionControls.HandleInvalidProjPathPrompt(ShowMainMenu);
    }
} while (displayMainMenu);
Environment.Exit(0);


void ShowMainMenu(bool showMainMenu)
{
    displayMainMenu = showMainMenu;
}



