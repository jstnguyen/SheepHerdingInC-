# SheepHerdingInCSharp
A sheep herding game in C#.

## Instructions
1. Ensure you have an editor necessary to run a C# project. Visual Studio Code can be downloaded [here](https://code.visualstudio.com/download).
2. Ensure you have the extensions necessary to run a C# project. [An article from VS Code:](https://code.visualstudio.com/Docs/languages/csharp) "C# language support is provided with the [C# Dev Kit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit). You can install it from within VS Code by searching for 'C# Dev Kit' in the Extensions view (Ctrl+Shift+X) or if you already have a project with C# files, VS Code will prompt you to install the extension as soon as you open a C# file."
3. You may need to define a package reference within the project file. Make sure the following snippet is included in sheepHerding.csproj
```bash
  <ItemGroup>
    // other package references
    <PackageReference Include="NAudio" Version="2.2.1" />
  </ItemGroup>
```
4. Add your own file path on line 12 of sheepHerding.cs
```bash
string soundFilePath = @"YourFilePath/betterOffAlone.mp3";
```
