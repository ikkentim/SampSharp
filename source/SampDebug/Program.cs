using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SampDebug
{
    /// <summary>
    /// Visual Studio is stupid enough not to accept .bat files as a startup process, 
    /// thus cannot run environment/samp-debug.bat. This application does just the same.
    /// </summary>
    class Program
    {
        private static void ConvertDirectory(string path)
        {
            Console.WriteLine("Converting folder \"{0}\"...", path);

            if (Directory.Exists(path))
                foreach (
                    var filename in
                        Directory.GetFiles(path, "*.mdb")
                            .Select(Path.GetFileNameWithoutExtension)
                            .Where(f => File.Exists(path + "/" + f)))
                    ConvertFile(path + "/" + filename);

            Console.WriteLine();
        }

        private static void ConvertFile(string path)
        {
            Console.WriteLine("Converting \"{0}\"...", path);

            Process.Start(new ProcessStartInfo(@"Mono\lib\mono\4.5\pdb2mdb", path)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
            }).WaitForExit();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Converting all pdb symbols to mdb symbols...");
            Console.WriteLine();

            ConvertDirectory("plugins");
            ConvertDirectory("gamemodes");

            Console.WriteLine("Done!");
        }
    }
}
