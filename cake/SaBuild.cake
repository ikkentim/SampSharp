#tool "nuget:?package=gitreleasemanager"

public class SaBuild
{
    protected string configuration;
    protected string buildDir;
    protected string solution;
    protected FilePath[] assemblyInfos;
    protected FilePath[] nuGetPackages;
    protected string githubUsername;
    protected string githubRepo;
    protected string nuGetKey;
    protected string nuGetSource;
    protected string githubReleaseUsername;
    protected string githubReleasePassword;
    
    public ICakeContext Context { get; private set; }
    public SaBuild(ICakeContext context, string configuration, string debugBuildDir, string releaseBuildDir, string solution, string[] assemblyInfos, string[] nuGetPackages, string githubUsername, string githubRepo, string nuGetKey, string nuGetSource, string githubReleaseUsername, string githubReleasePassword)
    {
        this.configuration = configuration.ToLower() == "release"
            ? "Release"
            : "Debug";
    
        this.Context = context;
        this.buildDir = this.configuration == "Release" ? releaseBuildDir : debugBuildDir;
        this.solution = solution;
        this.assemblyInfos = assemblyInfos.Select(x => (FilePath)x).ToArray();
        this.nuGetPackages = nuGetPackages.Select(x => (FilePath)x).ToArray();
        this.githubUsername = githubUsername;
        this.githubRepo = githubRepo;
        this.nuGetKey = nuGetKey;
        this.nuGetSource = nuGetSource;
        this.githubReleaseUsername = githubReleaseUsername;
        this.githubReleasePassword = githubReleasePassword;
    }
    
    #region Properties
    
    public virtual bool IsRelease
    {
        get
        {
            return configuration == "Release";
        }
    }
    
    public virtual bool IsRunningOnAppVeyor
    {
        get
        {
            return IsRelease && Context.BuildSystem().AppVeyor.IsRunningOnAppVeyor;
        }
    }
    
    public virtual bool IsAppVeyorTag
    {
        get
        {
            return IsRunningOnAppVeyor && Context.BuildSystem().AppVeyor.Environment.Repository.Tag.IsTag;
        }
    }
    
    public virtual string AppVeyorTagName
    {
        get
        {
            return Context.BuildSystem().AppVeyor.Environment.Repository.Tag.Name;
        }
    }
    
    ReleaseNotes _releaseNotes;
    public virtual ReleaseNotes ReleaseNotes
    {
        get
        {
            return _releaseNotes ?? (_releaseNotes = Context.ParseReleaseNotes("./CHANGES.md"));
        }
    }
    
    private string _version;
    public virtual string Version
    {
        get
        {
            if(_version != null)
            {
                return _version;
            }
            
            if(IsAppVeyorTag)
            {
                _version = AppVeyorTagName;
            }
            else
            {
                var build = (int)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalDays;// Days since 2000
                var revision = ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) / 2;// Minutes since midnight
                _version = string.Format("{0}.{1}.{2}.{3}", ReleaseNotes.Version.Major, ReleaseNotes.Version.Minor, build, revision);
            }
            
            return _version;
        }
    }
    
    private string _semanticVersion;
    public virtual string SemanticVersion
    {
        get
        {
            if(_semanticVersion != null)
            {
                return _semanticVersion;
            }
            
            var idx = Version.IndexOf('-');

            if(idx < 0)
            {
                idx = Version.IndexOf(' ');
            }

            return _semanticVersion = idx <= 0 ? Version : Version.Substring(0, idx);
        }
    }
    
    private string _latestReleaseNotesPath;
    public virtual string LatestReleaseNotesPath
    {
        get
        {
            if(_latestReleaseNotesPath != null)
            {
                return _latestReleaseNotesPath;
            }
            
            _latestReleaseNotesPath = buildDir + "/releasenotes-" + Version + ".txt";
            string notes = string.Join(System.Environment.NewLine, ReleaseNotes.Notes);
            System.IO.File.WriteAllText(_latestReleaseNotesPath, notes);
            
            return _latestReleaseNotesPath;
        }
    }
    
    #endregion
    
    public virtual void Clean()
    {
        Context.CleanDirectory(buildDir);
    }
    
    public virtual void RestoreNuGetPackages()
    {
        Context.NuGetRestore(solution);
    }
    
    private FilePath GetBackupPathForAssemblyInfo(FilePath path)
    {
        var str = path.FullPath;
        return Context.File(str.Substring(0, str.Length - 3) + ".backup.cs");
    }

    public virtual void VersionAssemblyInfo()
    {
        foreach(var path in assemblyInfos)
        {
            Context.Information("Updating assembly information for file {0}...", path);

            FilePath backupPath = GetBackupPathForAssemblyInfo(path);
        
            Context.CopyFile(path, backupPath);

            var abs = Context.MakeAbsolute(path).FullPath;
            var content = System.IO.File.ReadAllText(abs);
            var regex = new System.Text.RegularExpressions.Regex("\\[assembly: AssemblyVersion\\(\"(.*?)\"\\)\\]", System.Text.RegularExpressions.RegexOptions.Multiline);

            content = regex.Replace(content, string.Format("[assembly: AssemblyVersion(\"{0}\")]", SemanticVersion));

            System.IO.File.WriteAllText(abs, content);
        }
    }
    
    public virtual void RestoreAssemblyInfo()
    {
        foreach(var path in assemblyInfos)
        {
            Context.Information("Restoring assembly information for file {0}...", path);

            FilePath backupPath = GetBackupPathForAssemblyInfo(path);
        
            if(!Context.FileExists(backupPath))
            {
                continue;
            }
            
            Context.DeleteFile(path);
            Context.MoveFile(backupPath, path);
        }
    }
    
    public virtual void Build()
    {
        
        try
        {
            VersionAssemblyInfo();
            
            // Build the solution
            if(Context.IsRunningOnWindows())
            {
                // Use MSBuild
                Context.MSBuild(solution, settings =>
                    settings.SetConfiguration(configuration));
            }
            else
            {
                // Use XBuild
                Context.XBuild(solution, settings =>
                    settings.SetConfiguration(configuration));
            }
        }
        finally
        {
            RestoreAssemblyInfo();
        }
    }
    
    public virtual void Test()
    {
        Context.MSTest(buildDir + "/*.UnitTests.dll");
    }
    
    public virtual void CreateNuGetPackages()
    {
        foreach(var pkgName in nuGetPackages)
        {
            var nuspec = Context.File("./nuspec/" + pkgName + ".nuspec");

            Context.NuGetPack(nuspec, new NuGetPackSettings {
                Version = Version,
                ReleaseNotes = ReleaseNotes.Notes.ToArray(),
                BasePath = Context.Directory(buildDir),
                OutputDirectory = Context.Directory(buildDir)
            });
        }
    }
    
    public virtual void PublisNuGetPackages()
    {
        foreach(var pkgName in nuGetPackages)
        {
            foreach(var package in Context.GetFiles(buildDir + "/" + pkgName + ".*.nupkg"))
            {
                Context.NuGetPush(package, new NuGetPushSettings {
                    Source = nuGetSource,
                    ApiKey = nuGetKey
                });
            }
        }
    }
    
    protected virtual string GitHubReleaseAssets
    {
        get
        {
            return null;
        }
    }
    
    public virtual void PublishGitHubRelease()
    {
        Context.GitReleaseManagerCreate(githubReleaseUsername, githubReleasePassword, githubUsername, githubRepo, new GitReleaseManagerCreateSettings {
            InputFilePath     = Context.MakeAbsolute((FilePath)LatestReleaseNotesPath).FullPath,
            Name              = Version,
            Prerelease        = Version != SemanticVersion,
            TargetCommitish   = "master",
            Assets            = GitHubReleaseAssets
        });
    
        Context.GitReleaseManagerPublish(githubReleaseUsername, githubReleasePassword, githubUsername, githubRepo, Version);
    }
}




























