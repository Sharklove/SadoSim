using NUnit.Framework;
using System.IO;
using System.Text;

public class PumlSequenceTests
{
    [Test]
    public void CanWriteSequenceDiagram()
    {
        var path = "Docs/test_seq.puml";
        Directory.CreateDirectory("Docs");

        var sb = new StringBuilder();
        sb.AppendLine("@startuml");
        sb.AppendLine("Alice -> Bob: Hello");
        sb.AppendLine("@enduml");

        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

        var text = File.ReadAllText(path);
        StringAssert.Contains("Alice -> Bob", text);
    }
}
