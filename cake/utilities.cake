string MakeAbsolutePath(string path)
{
    return MakeAbsolutePath((FilePath)path);
}

string MakeAbsolutePath(FilePath path)
{
    return path.MakeAbsolute(Context.Environment).FullPath;
}