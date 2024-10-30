using dotnet_dependency_version_checker.content;

namespace dotnet_dependency_version_checker.MenuOptions;

public class MenuOptionControls
{
     public string? DisplayProjFilePathPrompt()
    {
        Console.WriteLine(TextContent.CsprojFilePrompt);
        return  Console.ReadLine();
    }

    public void HandleInvalidProjPathPrompt(string? csProjFilePath, Action<bool> showMenu)
    {
        var menuOptionControls = new MenuOptionControls();
        
        Console.WriteLine($"The provided path '{csProjFilePath}' is not valid or does not point to a .csproj file. \n");
        Console.WriteLine("Back: b");
        Console.WriteLine("Exit application: e \n");
        
        var menuChoice = Console.ReadLine();
        
        if (menuChoice == null || string.Equals(menuChoice.ToLower(), "e"))
        {
            showMenu(false);
        } else if (string.Equals(menuChoice.ToLower(), "b"))
        {
            menuOptionControls.DisplayProjFilePathPrompt();
            //Handle situaltion if the user inputs correct / uncorrect path
        }
        else
        {
            Console.WriteLine($"Invalid option. Option '{menuChoice}' does not exist \n");
            menuOptionControls.DisplayProjFilePathPrompt();
        }
    }
}