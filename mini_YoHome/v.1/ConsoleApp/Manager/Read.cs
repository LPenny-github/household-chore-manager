using System.IO;
using System.Text.Json;
using ConsoleApp.Model;

namespace ConsoleApp.Manager;

public class Read
{
    static List<ChoresInfo> ChoreInfoFile()
    {
        string fileName = "/Data/ChoreInfos.json";
        List<ChoresInfo>? choreInfos = new();

        if (File.Exists(fileName))
        {
            string? tempText = File.ReadAllText(fileName);
            choreInfos = JsonSerializer.Deserialize<List<ChoresInfo>>(tempText);
        }
        return choreInfos;
    }

    static List<ChoresRecord> ChoreRecordFile()
    {
        string fileName = "/Data/ChoreRecords.json";
        List<ChoresRecord>? choreRecords = new();

        if (File.Exists(fileName))
        {
            string? tempText = File.ReadAllText(fileName);
            choreRecords = JsonSerializer.Deserialize<List<ChoresRecord>>(tempText);
        }
        return choreRecords;
    }
}