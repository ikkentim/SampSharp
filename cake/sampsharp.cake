
#load "./version.cake"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////////

// /--------------------\
// |   Target options   |
// \--------------------/
configuration = configuration.ToLower() == "release"
    ? "Release"
    : "Debug";
var buildDir = configuration == "Debug"
    ? "./env/gamemode"
    : "./bin";

// /--------------------\
// |      Projects      |
// \--------------------/
var solution = "./SampSharp.sln";
var assemblyInfos = new [] {
    File("./src/SampSharp.GameMode/Properties/AssemblyInfo.cs"),
    File("./src/SampSharp.UnitTests/Properties/AssemblyInfo.cs"),
    File("./src/TestMode/Properties/AssemblyInfo.cs")
};

var lagetPackages = new [] {
    File("SampSharp.GameMode")
};


// /--------------------\
// | Computed variables |
// \--------------------/
ReleaseNotes releaseNotes;
string version;
string semanticVersion;
string pluginZipPath;

public class SampSharpBuild
{
    private ReleaseNotes _releaseNotes;
    private string _version;
    private string _semanticVersion;
    private bool _didRestoreNuGetPackages;

    public SampSharpBuild(string solution, string buildDir, string configuration, FilePath[] assemblyInfos)
    {
        Solution = solution;
        BuildDir = buildDir;
        Configuration = configuration;
        AssemblyInfos = assemblyInfos;
    }

    public string Solution { get; private set; }
    public string BuildDir { get; private set; }
    public string Configuration { get; private set; }
    public FilePath[] AssemblyInfos { get; private set; }

    public ReleaseNotes ReleaseNotes
    {
        get
        {
            return _releaseNotes ?? (_releaseNotes = ParseReleaseNotes("./CHANGES.md"));
        }
    }

    public string Version
    {
        get
        {
            EnsureVersionComputed();
            return _version;
        }
    }

    public string SemanticVersion
    {
        get
        {
            EnsureVersionComputed();
            return _semanticVersion;
        }
    }

    public string PluginZipPath { get; private set; }

    public void Clean()
    {
        CleanDirectory(BuildDir);
    }

    public void RestoreNuGetPackages()
    {
        if(_didRestoreNuGetPackages)
        {
            return;
        }

        NuGetRestore(Solution);

        _didRestoreNuGetPackages = true;
    }

    public void UpdateAssemblyInfo()
    {
        foreach(var file in AssemblyInfos)
        {
            Information("Updating assembly information for file {0}...", file);

            VersionAssemblyInfo(file, SemanticVersion);
        }
    }

    public void RestoreAssemblyInfo()
    {
        foreach(var file in AssemblyInfos)
        {
            Information("Restoring assembly information for file {0}...", file);

            RestoreAssemblyInfoBackup(file);
        }
    }

    public void BuildSolution()
    {
        RestoreNuGetPackages();

        try
        {
                // Build the solution
                if(IsRunningOnWindows())
                {
                  // Use MSBuild
                  MSBuild(Solution, settings =>
                    settings.SetConfiguration(Configuration));
                }
                else
                {
                  // Use XBuild
                  XBuild(Solution, settings =>
                    settings.SetConfiguration(Configuration));
                }
        }
        catch
        {
            RestoreAssemblyInfo();
            throw;
        }
    }

    private void EnsureVersionComputed()
    {
        if(BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
        {
            Information("Using repository tag as version name.");

            _version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
        }
        else
        {
            Information("Computing version name from release notes and date.");

            var build = (int)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalDays;// Days since 2000
            var revision = ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) / 2;// Minutes since midnight

            _version = string.Format("{0}.{1}.{2}.{3}", ReleaseNotes.Version.Major, ReleaseNotes.Version.Minor, build, revision);
                Information("Current assembly version: {0}", version);
        }

        var idx = _version.IndexOf('-');

        if(idx < 0)
        {
            idx = _version.IndexOf(' ');
        }

        if(idx <= 0)
        {
            _semanticVersion = _version;
        }
        else
        {
            _semanticVersion = _version.Substring(idx);
        }
    }
}

Task("__RunUnitTests")
    .IsDependentOn("__BuildSolution")
    .Does(() =>
{
    MSTest(buildDir + "/*.UnitTests.dll");
});

Task("__CreatePluginPackage")
    .WithCriteria(() => IsRunningOnWindows() && configuration == "Release")
    .Does(() =>
{
    var outName = "SampSharp-" + version;
    var pluginDir = buildDir + "/" + outName + "/";

    CreateDirectory(pluginDir);

    CopyDirectory("./env/codepages", pluginDir + "codepages");
    CopyDirectory("./env/filterscripts", pluginDir + "filterscripts");
    CopyDirectory("./env/gamemodes", pluginDir + "gamemodes");

    CreateDirectory(pluginDir + "plugins");
    CopyFile(buildDir + "/SampSharp.dll", pluginDir + "plugins/SampSharp.dll");
    CopyFile("./env/server.cfg.template", pluginDir + "server.cfg.template");

    pluginZipPath = buildDir + "/" + outName + ".zip";
    Zip(pluginDir, pluginZipPath);

    if((EnvironmentVariable("APPVEYOR") ?? "False") == "True")
    {
        CopyFile(pluginZipPath, "./bin/plugin.zip");
    }
});

Task("__CreateNuGetPackagesIfAppVeyorTag")
    .WithCriteria(() => configuration == "Release" &&
        (EnvironmentVariable("APPVEYOR") ?? "False") == "True" &&
        BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .IsDependentOn("__ParseReleaseNotes")
    .IsDependentOn("__ComputeVersion")
    .IsDependentOn("__BuildSolution")
    .Does(() =>
{
    // Create package.
    foreach(var pkgName in lagetPackages)
    {
        var nuspec = File("./nuspec/" + pkgName + ".nuspec");

        NuGetPack(nuspec, new NuGetPackSettings {
            Version = version,
            ReleaseNotes = releaseNotes.Notes.ToArray(),
            BasePath = Directory(buildDir),
            OutputDirectory = Directory(buildDir)
        });
    }
});

Task("__PublishNuGetPackagesIfAppVeyorTag")
    .WithCriteria(() => configuration == "Release" &&
        (EnvironmentVariable("APPVEYOR") ?? "False") == "True" &&
        BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .Does(() =>
{
    var lagetKey = EnvironmentVariable("LAGET_KEY");

    foreach(var pkgName in lagetPackages)
    {
        foreach(var package in GetFiles(buildDir + "/" + pkgName + ".*.nupkg"))
        {
            NuGetPush(package, new NuGetPushSettings {
                Source = "http://nuget.timpotze.nl/upload",
                ApiKey = lagetKey
            });
        }
    }
});

Task("__CreateGitHubReleaseIfAppVeyorTag")
.WithCriteria(() => configuration == "Release" &&
    (EnvironmentVariable("APPVEYOR") ?? "False") == "True" &&
    BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
.IsDependentOn("__ComputeVersion")
.Does(() =>
{
    var user = EnvironmentVariable("GITHUB_USERNAME");
    var pass = EnvironmentVariable("GITHUB_PASSWORD");

    GitReleaseManagerCreate(user, pass, "ikkentim", "SampSharp", new GitReleaseManagerCreateSettings {
            Milestone         = version,
            Name              = version,
            Prerelease        = version != semanticVersion,
            TargetCommitish   = "master"
        });
})
.OnError(exception =>
{
    Information("__CreateGitHubReleaseIfAppVeyorTag Task failed, but continuing with next Task...");
});

Task("__PublishGitHubReleaseIfAppVeyorTag")
    .WithCriteria(() => configuration == "Release" &&
        (EnvironmentVariable("APPVEYOR") ?? "False") == "True" &&
        BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .IsDependentOn("__ComputeVersion")
    .IsDependentOn("__CreatePluginPackage")
    .Does(() =>
{
    var user = EnvironmentVariable("GITHUB_USERNAME");
    var pass = EnvironmentVariable("GITHUB_PASSWORD");

    GitReleaseManagerAddAssets(user, pass, "ikkentim", "SampSharp", version, pluginZipPath);
    GitReleaseManagerClose(user, pass, "ikkentim", "SampSharp", version);
})
.OnError(exception =>
{
    Information("__PublishGitHubReleaseIfAppVeyorTag Task failed, but continuing with next Task...");
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
    .IsDependentOn("__CreatePluginPackage")
    .IsDependentOn("__RestoreAssemblyInfo")
    .IsDependentOn("__RunUnitTests")
    ;

Task("PublishToNuGetIfAppVeyorTag")
    .WithCriteria(() => configuration == "Release" &&
        (EnvironmentVariable("APPVEYOR") ?? "False") == "True" &&
        BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag)
    .IsDependentOn("__CreateNuGetPackagesIfAppVeyorTag")
    .IsDependentOn("__PublishNuGetPackagesIfAppVeyorTag")
    .IsDependentOn("__CreateGitHubReleaseIfAppVeyorTag")
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

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
