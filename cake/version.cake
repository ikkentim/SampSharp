using System.Text.RegularExpressions;

FilePath GetBackupPathForAssemblyInfo(FilePath path)
{
    var str = path.FullPath;

    return File(str.Substring(0, str.Length - 3) + ".backup.cs");
}

void VersionAssemblyInfo(FilePath path, string version)
{
    VersionAssemblyInfo(path, GetBackupPathForAssemblyInfo(path), version);
}

void VersionAssemblyInfo(FilePath path, FilePath backupPath, string version)
{
    CopyFile(path, backupPath);

    var abs = MakeAbsolute(path).FullPath;
    var content = System.IO.File.ReadAllText(abs);
    var regex = new Regex("\\[assembly: AssemblyVersion\\(\"(.*?)\"\\)\\]", RegexOptions.Multiline);

    content = regex.Replace(content, string.Format("[assembly: AssemblyVersion(\"{0}\")]", version));

    System.IO.File.WriteAllText(abs, content);
}

void RestoreAssemblyInfoBackup(FilePath path)
{
    RestoreAssemblyInfoBackup(path, GetBackupPathForAssemblyInfo(path));
}

void RestoreAssemblyInfoBackup(FilePath path, FilePath backupPath)
{
    if(!FileExists(backupPath))
        return;

    DeleteFile(path);
    MoveFile(backupPath, path);
}
