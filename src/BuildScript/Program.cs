using AtmaFileSystem;
using AtmaFileSystem.IO;
using DotnetExeCommandLineBuilder;
using static Bullseye.Targets;
using static DotnetExeCommandLineBuilder.DotnetExeCommands;
using static SimpleExec.Command;

var configuration = "Release";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(2).Value();
var srcDir = root.AddDirectoryName("src");
var nugetPath = root.AddDirectoryName("nuget");
var version="0.2.0";

if (!nugetPath.Exists())
{
  nugetPath.Create();
}

//////////////////////////////////////////////////////////////////////
// HELPER FUNCTIONS
//////////////////////////////////////////////////////////////////////
void Pack(AbsoluteDirectoryPath outputPath, AbsoluteDirectoryPath rootSourceDir, string projectName)
{
  Run("dotnet",
    DotnetExeCommands.Pack()
      .Configuration(configuration)
      .IncludeSymbols()
      .IncludeSource()
      .NoBuild()
      .WithArg("-p:SymbolPackageFormat=snupkg")
      .WithArg($"-p:VersionPrefix={version}")
      .Output(outputPath),
    workingDirectory: rootSourceDir.ToString());
}

//////////////////////////////////////////////////////////////////////                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean", () =>
{
  nugetPath.Delete(true);
  Run("dotnet", Clean().Configuration("Release"),
    workingDirectory: srcDir.ToString());
  Run("dotnet", Clean().Configuration("Debug"),
    workingDirectory: srcDir.ToString());
});

Target("Build", () =>
{
  Run("dotnet", Build().Configuration(configuration).WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcDir.ToString());
});

Target("Test", DependsOn("Build"), () =>
{
  Run("dotnet",
    Test().NoBuild().Configuration(configuration).WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcDir.ToString());
});

Target("Pack", DependsOn("Test", (string) "Build"), () =>
{
  Pack(nugetPath, srcDir, "Any.NSubstitite");
});

Target("Push", DependsOn("Clean", "Pack"), () =>
{
    foreach (var nupkgPath in nugetPath.GetFiles("*.nupkg"))
    {
        Run("dotnet", NugetPush(nupkgPath).Source("https://api.nuget.org/v3/index.json"));
    }
});

Target("default", DependsOn("Pack"));

await RunTargetsAndExitAsync(args);

