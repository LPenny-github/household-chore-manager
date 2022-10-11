using ConsoleApp.Model;
using System.Text.Json;

namespace ConsoleApp.Manager;

public class Write
{
    public bool ChoreInfoFile(List<ChoresInfo> choresInfos)
    {
        string filePath = "../ConsoleApp/Data/ChoreInfos.json";
        string data = JsonSerializer.Serialize<List<ChoresInfo>>(choresInfos);
        try
        {
            File.WriteAllText(filePath, data);
            return true;
        }
        catch (System.Exception)
        {
            // todo
            // throw;
        }
        return false;
    }

    public bool ChoreDecordFile(List<ChoresRecord> choresRecords)
    {
        string filePath = "../ConsoleApp/Data/ChoreRecords.json";
        string data = JsonSerializer.Serialize<List<ChoresRecord>>(choresRecords);
        try
        {
            File.WriteAllText(filePath, data);
            return true;
        }
        catch (System.Exception)
        {
            // todo
            // throw;
        }
        return false;
    }
}