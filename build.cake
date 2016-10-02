#load "./cake/SaBuild.cake"

//////////////////////////////////////////////////////////////////////
// SABUILD
//////////////////////////////////////////////////////////////////////

public class CustomBuild : SaBuild
{
    private string pluginZipPath;
    
    public CustomBuild(ICakeContext context, string configuration, string debugBuildDir, string releaseBuildDir, string solution, string[] assemblyInfos, string[] nuGetPackages, string githubUsername, string githubRepo, string nuGetKey, string nuGetSource, string githubReleaseUsername, string githubReleasePassword, string[] nugetSources) 
        : base(context, configuration, debugBuildDir, releaseBuildDir, solution, assemblyInfos, nuGetPackages, githubUsername, githubRepo, nuGetKey, nuGetSource, githubReleaseUsername, githubReleasePassword, nugetSources)
    {
        
    }
    
    protected override string GitHubReleaseAssets
    {
        get
        {
            return Context.MakeAbsolute((FilePath)pluginZipPath).FullPath;
        }
    }
    
    public override void VersionAssemblyInfo()
    {
        base.VersionAssemblyInfo();
        
        UpdateAssemblyInfo("./src/SampSharp/main.h", "#define PLUGIN_VERSION \"(.*)\"", "#define PLUGIN_VERSION \"{0}\"");
    }
    
    public override void RestoreAssemblyInfo()
    {
        base.RestoreAssemblyInfo();
        
        RestoreAssemblyInfo("./src/SampSharp/main.h");
    }
    
    public override void Build()
    {
        base.Build();
        
        if(!IsRelease)
        {
            return;
        }
        
        var outName = "SampSharp-" + Version;
        var pluginDir = buildDir + "/" + outName + "/";

        Context.CreateDirectory(pluginDir);

        Context.CopyDirectory("./env/codepages", pluginDir + "codepages");
        Context.CopyDirectory("./env/filterscripts", pluginDir + "filterscripts");
        Context.CopyDirectory("./env/gamemodes", pluginDir + "gamemodes");

        Context.CreateDirectory(pluginDir + "plugins");
        Context.CopyFile(buildDir + "/SampSharp.dll", pluginDir + "plugins/SampSharp.dll");
        Context.CopyFile("./env/server.cfg.template", pluginDir + "server.cfg.template");

        pluginZipPath = buildDir + "/" + outName + ".zip";
        Context.Zip(pluginDir, pluginZipPath);

        if(IsRunningOnAppVeyor)
        {
            Context.CopyFile(pluginZipPath, "./bin/plugin.zip");
        }
    }
}

var build = new CustomBuild(
    Context,                                // cake context
    Argument("configuration", "Release"),   // configuration
    "./env/gamemode",                       // debug build directory
    "./bin",                                // release build directory
    "./SampSharp.sln",                      // solution
    new[] {                                 // assembly infos
        "./src/SampSharp.GameMode/Properties/AssemblyInfo.cs", 
        "./src/SampSharp.UnitTests/Properties/AssemblyInfo.cs", 
        "./src/TestMode/Properties/AssemblyInfo.cs",
    },
    new[] {                                 // nuget packages
        "SampSharp.GameMode",
    },
    "ikkentim",                             // github username
    "SampSharp",                            // github repository
    EnvironmentVariable("LAGET_KEY"),       // nuget key
    "http://nuget.timpotze.nl/upload",      // nuget source
    EnvironmentVariable("GITHUB_USERNAME"), // github release username
    EnvironmentVariable("GITHUB_PASSWORD"), // github release password
    new[] {                                 // nuget sources
        "http://nuget.timpotze.nl/api/v2/"
    }
);
   
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("__Clean")
    .Does(() => build.Clean());

Task("__RestoreNuGetPackages")
    .Does(() => build.RestoreNuGetPackages());

Task("__BuildSolution")
    .IsDependentOn("__RestoreNuGetPackages")
    .Does(() => build.Build());

Task("__RunUnitTests")
    .IsDependentOn("__BuildSolution")
    .Does(() => build.Test());
    
Task("__CreateNuGetPackagesIfAppVeyorTag")
    .WithCriteria(() => build.IsAppVeyorTag)
    .IsDependentOn("__BuildSolution")
    .Does(() => build.CreateNuGetPackages());

Task("__PublishNuGetPackagesIfAppVeyorTag")
    .WithCriteria(() => build.IsAppVeyorTag)
    .Does(() => build.PublisNuGetPackages());


Task("__PublishGitHubReleaseIfAppVeyorTag")
    .WithCriteria(() => build.IsAppVeyorTag)
    .Does(() => build.PublishGitHubRelease())
    .OnError(exception =>
    {
        Information("__PublishGitHubReleaseIfAppVeyorTag Task failed, but continuing with next task...");
    });

Task("__DisplayVersion")
    .Does(() =>
{
    if(!build.IsRelease)
    {
        throw new Exception("Build in release before you tag!");
    }
    
    Console.WriteLine("Suggested tag for the next release is: " + build.Version);
});

Task("Build")
    .IsDependentOn("__Clean")
    .IsDependentOn("__RestoreNuGetPackages")
    .IsDependentOn("__BuildSolution")
    .IsDependentOn("__RunUnitTests")
    ;

Task("PublishToNuGetIfAppVeyorTag")
    .WithCriteria(() => build.IsAppVeyorTag)
    .IsDependentOn("__CreateNuGetPackagesIfAppVeyorTag")
    .IsDependentOn("__PublishNuGetPackagesIfAppVeyorTag")
    .IsDependentOn("__PublishGitHubReleaseIfAppVeyorTag")
    ;

///////////////////////////////////////////////////////////////////////////////
// PRIMARY TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    ;

Task("AppVeyor")
    .IsDependentOn("Build")
    .IsDependentOn("PublishToNuGetIfAppVeyorTag")
    ;

Task("GenerateTag")
    .IsDependentOn("Build")
    .IsDependentOn("__DisplayVersion");
 
///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(Argument("target", "Default"));
