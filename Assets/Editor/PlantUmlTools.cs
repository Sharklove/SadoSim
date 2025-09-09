#if UNITY_EDITOR
using System.Diagnostics;
using System.IO;
using UnityEditor;

public static class PlantUmlTools
{
    const string InputDir  = "Assets/Script";
    const string OutputDir = "Docs/uml";
    const string PlantUmlJar = @"C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar";

    [MenuItem("Tools/PlantUML/Generate Class Diagram")]
    public static void GenerateClassDiagram()
    {
        Directory.CreateDirectory(OutputDir);
        Run("puml-gen", $"{InputDir} {OutputDir} -dir -ignore bin,obj,Properties -createAssociation -allInOne");
        Run("java", $"-jar \"{PlantUmlJar}\" -tsvg \"{OutputDir}\"");
        EditorUtility.RevealInFinder(OutputDir);
    }

    static void Run(string file, string args)
    {
        var p = new Process {
            StartInfo = new ProcessStartInfo {
                FileName = file,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };
        p.Start();
        UnityEngine.Debug.Log(p.StandardOutput.ReadToEnd());
        var err = p.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(err)) UnityEngine.Debug.LogWarning(err);
        p.WaitForExit();
    }
}
#endif
