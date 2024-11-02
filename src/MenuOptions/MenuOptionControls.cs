using dotnet_dependency_version_checker.content;
using dotnet_dependency_version_checker.service;

namespace dotnet_dependency_version_checker.MenuOptions;

public class MenuOptionControls
{
     public string? DisplayProjFilePathPrompt()
    {
        Console.WriteLine(TextContent.CsprojFilePrompt);
        return  Console.ReadLine();
    }

    public static void HandleInvalidProjPathPrompt(Action<bool> showMenu)
    {
        Console.WriteLine(TextContent.InvalidProjPathWarning);
        DisplayInvalidProjPathMenuControls();
        
        var displayMenu = true;
        do
        {

            var menuChoice = Console.ReadLine();
        
            if (string.IsNullOrWhiteSpace(menuChoice))
            {
                Console.WriteLine($"Invalid option. Please select '{TextContent.BackOption}' or '{TextContent.ExitOption}'.\n");
                return;
            }
        
            switch (menuChoice.Trim().ToLower())
            {
                case TextContent.ExitOption:
                    displayMenu = false;
                    showMenu(false);
                    break;
                case TextContent.BackOption:
                    //Control is passed back to the main loop in Program.cs
                    displayMenu = false;
                    break;
                default:
                    Console.WriteLine($"Invalid menu option. Option '{menuChoice}' does not exist.\n");
                    DisplayInvalidProjPathMenuControls();
                    displayMenu = true;
                    break;
            }
        } while (displayMenu);

    }
    
    private static void DisplayInvalidProjPathMenuControls()
    {
        Console.WriteLine($"Back: {TextContent.BackOption}");
        Console.WriteLine($"Exit application: {TextContent.ExitOption}\n");
    }

    public string HandleDisplayDependencyVersionCheck(string path)
    {
        // Step 1: call ProjFile service that will parse provided XML file and extract dependency versions
        var projFileService = new ProjFileService();
        var dependencyList = projFileService.ParseProjFile(path);
        // Step 2: call NugetAPI to get latest or stable versions of dependencies extracted in step 1
        // Step 3: compare those versions and display all dependencies that are outdated
        return "";
    }


}