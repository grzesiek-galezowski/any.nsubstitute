using AtmaFileSystem;
using AtmaFileSystem.IO;
using static Bullseye.Targets;
using static DotnetExeCommandLineBuilder.DotnetExeCommands;
using static SimpleExec.Command;

var configuration = "Release";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(3).Value();
var srcDir = root.AddDirectoryName("src");
var nugetPath = root.AddDirectoryName("nuget");
var version="8.0.0";

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
    $"pack" +
    $" -c {configuration}" +
    $" --include-symbols" +
    $" --no-build" +
    $" -p:SymbolPackageFormat=snupkg" +
    $" -p:VersionPrefix={version}" +
    $" -o {outputPath}",
    workingDirectory: rootSourceDir.AddDirectoryName(projectName).ToString());
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean", () =>
{
  nugetPath.Delete(true);
  Run($"dotnet",
    "clean " +
    $"-c {configuration} ",
    workingDirectory: srcDir.ToString());
});

Target("Build", () =>
{
  Run($"dotnet",
    "build " +
    $"-c {configuration} " +
    //$"-o {buildDir} " +
    $"-p:VersionPrefix={version}",
    workingDirectory: srcDir.ToString());
});

Target("Test", DependsOn("Build"), () =>
{
  Run("dotnet",
    Test().NoBuild().Configuration(configuration).WithArg("-p:VersionPrefix", version),
    workingDirectory: srcDir.ToString());
});

Target("Pack", DependsOn("Test", (string) "NScan"), () =>
{
  Pack(nugetPath, srcDir, "XFluentAssert");
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

