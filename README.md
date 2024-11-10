# Dependency Version Checker

A console tool for .NET projects that checks dependency versions and helps keep projects up to date, secure, and free from deprecated packages.

## Description

The Dependency Version Checker compares the installed versions of dependencies in your .NET project(s) against the latest available versions on NuGet. This tool is useful for ensuring your project stays up to date and for identifying any deprecated packages that may pose security or compatibility risks.

## Features

- **Dependency Version Comparison**: Shows installed versions versus the latest versions for all dependencies.
- **Deprecated Package Alerts**: Notifies you of any packages that are deprecated or have breaking changes.
- **Summary Report Export**: Exports a summary report of outdated dependencies and deprecated packages for easy review.

## Real-Life Use

This tool helps developers maintain dependencies by checking for available updates and deprecated packages. It is especially useful for managing version drift in large or multi-project solutions.

### Build Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/dotnet-dependency-version-checker.git
   cd dotnet-dependency-version-checker
2. **Restore dependencies: Before building the project, restore the required dependencies:**:
   ```bash
   dotnet restore
3. **Build the project: To build the project in release mode, run:**:
   ```bash
   dotnet build -c Release
4. **Publish the project: If you want to publish the project as a self-contained executable (including .NET runtime), use the following command. Replace osx-arm64 with your platform identifier (win-x64, linux-x64, etc.) as needed:**:
   ```bash
   dotnet publish -c Release -r osx-arm64 --self-contained
5. **Run the executable: For macOS/Linux, run the executable with:**:
   ```bash
   ./dotnet-dependency-version-checker

## Example 

Showcase of console output and web report

1. **Console output**:
![image](https://github.com/user-attachments/assets/c527fd92-eff1-4501-b5ce-e083730e30ab)

2. **Web report**:
![image](https://github.com/user-attachments/assets/7e25bd56-496d-43c5-a712-696a2ebad7d8)
