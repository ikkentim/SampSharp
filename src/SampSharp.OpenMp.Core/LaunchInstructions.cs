using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SampSharp.OpenMp.Core;

internal static partial class LaunchInstructions
{
    private static readonly Regex _slnProjectRegex = ProjectInSolutionRegex();

    public static void Write()
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("SampSharp");
        Console.WriteLine("-------------------------------------");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("ERROR: This SampSharp gamemode must be run using an open.mp server.");
        Console.ResetColor();
        Console.WriteLine("See <<TODO: Documentation URL>> for more information.");
        Console.WriteLine();

        if (!IsRunningInVisualStudio())
        {
            return;
        }

        var dir = GetProjectDir();
        if (dir != null)
        {
            Console.WriteLine("It appears you are running this application in Visual Studio. Would you like SampSharp to update your launchSettings.json with the following configuration?");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("""
                              -------------------------------------
                              {
                                "profiles": {
                                  "open.mp": {
                                    "commandName": "Executable",
                                    "executablePath": "C:\path\to\server\omp-server.exe",
                                    "workingDirectory": "C:\path\to\server\",
                                    "commandLineArgs": "-c sampsharp.directory=$(TargetDir) -c sampsharp.assembly=\"$(TargetName)\""
                                  }
                                }
                              }
                              -------------------------------------
                              """);
            Console.WriteLine();
            Console.WriteLine($"Detected project path: {dir}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WARNING: This will replace any existing launch profiles for your project.");
            Console.ResetColor();

            if (!PromptYesNo())
            {
                return;
            }

            var props = dir.CreateSubdirectory("Properties");

            var serverDir = PromptServerDirectory();

            var launchSettingsPath = Path.Combine(props.FullName, "launchSettings.json");

            serverDir = serverDir.Replace(@"\", @"\\");

            File.WriteAllText(launchSettingsPath,
                $$"""
                  {
                    "profiles": {
                      "open.mp": {
                        "commandName": "Executable",
                        "executablePath": "{{serverDir}}omp-server.exe",
                        "workingDirectory": "{{serverDir}}",
                        "commandLineArgs": "-c sampsharp.directory=$(TargetDir) -c sampsharp.assembly=\"$(TargetName)\""
                      }
                    }
                  }
                  """);

            Console.WriteLine($"File written to {launchSettingsPath}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You will find the 'open.mp' launch option in the dropdown next to the 'Start' button in Visual Studio.");
            Console.ResetColor();
        }
    }

    private static bool PromptYesNo()
    {
        Console.Write("Press (y)es / (n)o: ");
        while (true)
        {
            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.N:
                    Console.WriteLine();
                    return false;
                case ConsoleKey.Y:
                    Console.WriteLine();
                    return true;
            }
        }

    }

    private static string PromptServerDirectory()
    {
        while (true)
        {
            Console.Write("Enter the path to your open.mp server directory: ");

            var dir = Console.ReadLine();
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Directory not found.");
                continue;
            }

            var exe = Path.Combine(dir, "omp-server.exe");

            if (!File.Exists(exe))
            {
                Console.WriteLine("Invalid directory.");
                continue;
            }

            if (!dir.EndsWith('/') && !dir.EndsWith('\\'))
            {
                dir += Path.DirectorySeparatorChar;
            }
            return dir;
        }
    }

    private static bool IsRunningInVisualStudio()
    {
        return Debugger.IsAttached && Environment.GetEnvironmentVariable("VisualStudioVersion") != null;
    }

    private static DirectoryInfo? GetProjectDir()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        for (var i = 0; i < 10; i++)
        {
            // does this dir contain a .csproj file? assume this dir is the project root
            var csproj = dir.GetFiles("*.csproj");
            if (csproj.Length != 0)
            {
                return dir;
            }

            // does this dir contain a .sln file? read it and find the project root
            var result = dir.GetFiles("*.sln")
                .Select(GetProjectDirFromSolution)
                .FirstOrDefault(x => x != null);

            if (result != null)
            {
                return result;
            }

            // does this dir contain a .slnx file? read it and find the project root
            result = dir.GetFiles("*.slnx")
                .Select(GetProjectDirFromSolutionEx)
                .FirstOrDefault(x => x != null);
            if (result != null)
            {
                return result;
            }

            // move up a directory and try again
            dir = dir.Parent;
            if (dir == null)
            {
                break;
            }
        }

        return null;
    }

    private static DirectoryInfo? GetProjectDirFromSolution(FileInfo sln)
    {
        var asmName = Assembly.GetEntryAssembly()?.GetName().Name;

        if (asmName == null)
        {
            return null;
        }

        var matches = new List<string>();
        var lines = File.ReadAllLines(sln.FullName);

        foreach (var line in lines)
        {
            var match = _slnProjectRegex.Match(line);
            if (match.Success)
            {
                var path = match.Groups["path"].Value;
                var name = match.Groups["name"].Value;

                if (name.Contains(asmName))
                {
                    matches.Add(path);
                }
            }
        }

        if (matches.Count > 0)
        {
            var best = matches.OrderBy(x => x.Length).First();
            var file = new DirectoryInfo(Path.GetDirectoryName(Path.Combine(sln.Directory!.FullName, best))!);
            return file;
        }

        return null;
    }

    private static DirectoryInfo? GetProjectDirFromSolutionEx(FileInfo slnx)
    {
        var asmName = Assembly.GetEntryAssembly()?.GetName().Name;

        if (asmName == null)
        {
            return null;
        }

        var doc = XDocument.Load(slnx.FullName);

        var matches = doc.Descendants("Project")
            .Where(e => (string?)e.Attribute("Path") is { } path && path.Contains(asmName))
            .Select(e => (string)e.Attribute("Path")!)
            .ToList();

        if (matches.Count > 0)
        {
            var best = matches.OrderBy(x => x.Length).First();
            var file = new DirectoryInfo(Path.GetDirectoryName(Path.Combine(slnx.Directory!.FullName, best))!);
            return file;
        }

        return null;
    }

    [GeneratedRegex("""
                    Project\("{[0-9A-Fa-f\-]+}"\)\s*=\s*"(?<name>[^"]+)"\s*,\s*"(?<path>[^"]+)"
                    """)]
    private static partial Regex ProjectInSolutionRegex();
}