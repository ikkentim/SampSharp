
#load "./build/version.cake"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////////

// Project
configuration = configuration.ToLower() == "release"
    ? "Release"
    : "Debug";
var buildDir = configuration == "Debug"
    ? "./env/gamemode"
    : "./bin";
var solution = "./SampSharp.sln";

// Assembly info files
var assemblyInfos = new []{
    File("./src/SampSharp.GameMode/Properties/AssemblyInfo.cs"),
    File("./src/SampSharp.UnitTests/Properties/AssemblyInfo.cs"),
    File("./src/TestMode/Properties/AssemblyInfo.cs")
};

ReleaseNotes releaseNotes;
string version;
//////////////////////////////////////////////////////////////////////
// PRIVATE TASKS
//////////////////////////////////////////////////////////////////////

Task("__Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("__RestoreNuGetPackages")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("__ParseReleaseNotes")
    .Does(() =>
{
    releaseNotes = ParseReleaseNotes("./CHANGES.md");
});

Task("__ComputeVersion")
    .IsDependentOn("__ParseReleaseNotes")
    .Does(() =>
{
    var build = (int)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalDays;
    var revision = ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) / 2;

    version = string.Format("{0}.{1}.{2}.{3}", releaseNotes.Version.Major, releaseNotes.Version.Minor, build, revision);
        Information("Current assembly version: {0}", version);
});

Task("__UpdateAssemblyInfo")
    .IsDependentOn("__ComputeVersion")
    .Does(() =>
{
    foreach(var file in assemblyInfos)
        VersionAssemblyInfo(file, version);
});

Task("__RestoreAssemblyInfo")
    .IsDependentOn("__ParseReleaseNotes")
    .Does(() =>
{
    foreach(var file in assemblyInfos)
        RestoreAssemblyInfoBackup(file);
});

Task("__BuildSolution")
    .IsDependentOn("__UpdateAssemblyInfo")
    .IsDependentOn("__RestoreNuGetPackages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild(solution, settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild(solution, settings =>
        settings.SetConfiguration(configuration));
    }
}).Finally(() =>
{
    foreach(var file in assemblyInfos)
        RestoreAssemblyInfoBackup(file);
});

Task("__RunUnitTests")
    .IsDependentOn("__BuildSolution")
    .Does(() =>
{
    MSTest(buildDir + "/*.UnitTests.dll");
});

Task("__CreateNuGetPackages")
    .WithCriteria(() => configuration == "Release")
    .IsDependentOn("__ParseReleaseNotes")
    .IsDependentOn("__ComputeVersion")
    .IsDependentOn("__BuildSolution")
    .Does(() =>
{
    // Create package.
    // TODO: spec should not be hardcoded
    NuGetPack(File("./nuspec/SampSharp.GameMode.nuspec"), new NuGetPackSettings {
        Version = version,
        ReleaseNotes = releaseNotes.Notes.ToArray(),
        BasePath = Directory(buildDir),
        OutputDirectory = Directory(buildDir)
    });
});

Task("__PublishNuGetPackages")
    .Does(() =>
{
    // Does nothing yet...
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("__Clean")
    .IsDependentOn("__RestoreNuGetPackages")
    .IsDependentOn("__ParseReleaseNotes")
    .IsDependentOn("__ComputeVersion")
    .IsDependentOn("__UpdateAssemblyInfo")
    .IsDependentOn("__BuildSolution")
    .IsDependentOn("__RestoreAssemblyInfo")
    /*.IsDependentOn("__RunUnitTests")*/
    ;

Task("PublishToNuGet")
    .IsDependentOn("__CreateNuGetPackages")
    .IsDependentOn("__PublishNuGetPackages")
    ;

///////////////////////////////////////////////////////////////////////////////
// PRIMARY TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    ;

Task("Publish")
    .IsDependentOn("Build")
    .IsDependentOn("PublishToNuGet")
    ;

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
