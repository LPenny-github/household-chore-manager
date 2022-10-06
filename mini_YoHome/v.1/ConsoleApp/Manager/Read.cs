using System.IO;
using System.Text.Json;
using ConsoleApp.Model;

namespace ConsoleApp.Manager;

public class Read
{
    public static List<ChoresInfo> ChoreInfoFile()
    {
        string fileName = "../ConsoleApp/Data/ChoreInfos.json";
        List<ChoresInfo>? choreInfos = new();

        if (File.Exists(fileName))
        {
            string? tempText = File.ReadAllText(fileName);
            if (!string.IsNullOrEmpty(tempText))
            {
                choreInfos = JsonSerializer.Deserialize<List<ChoresInfo>>(tempText);
            }
        }
        return choreInfos;
    }

    public static List<ChoresRecord> ChoreRecordFile()
    {
        string fileName = "../ConsoleApp/Data/ChoreRecords.json";
        List<ChoresRecord>? choreRecords = new();

        if (File.Exists(fileName))
        {
            string? tempText = File.ReadAllText(fileName);
            if (!string.IsNullOrEmpty(tempText))
            {
                choreRecords = JsonSerializer.Deserialize<List<ChoresRecord>>(tempText);
            }
        }
        return choreRecords;
    }
}