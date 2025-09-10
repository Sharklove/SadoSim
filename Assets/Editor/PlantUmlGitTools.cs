using System;
using System.Diagnostics;
using System.IO;

public static class PlantUmlGitTools
{
    const string InputDir  = "Assets/Script/Sample";   // 必要なら Scripts に変更
    const string OutputDir = "Docs/uml";
    const string PlantUmlJar = "plantuml.jar";  // Actions で curl で取得した jar を使う
    static readonly string toolPath = Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".dotnet", "tools", "puml-gen");
    public static void Main(string[] args)
    {
        Directory.CreateDirectory(OutputDir);

        Run(toolPath, $"{InputDir} {OutputDir} -dir -ignore bin,obj,Properties -createAssociation -allInOne");
        Run("java", $"-jar \"{PlantUmlJar}\" -tsvg \"{OutputDir}\"");
    }

    static void Run(string file, string arguments)
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = file,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };
        p.Start();
        Console.WriteLine(p.StandardOutput.ReadToEnd());
        var err = p.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(err))
            Console.WriteLine("[WARN] " + err);
        p.WaitForExit();
    }
}
