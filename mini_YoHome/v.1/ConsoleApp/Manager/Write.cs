using ConsoleApp.Model;
using System.Text.Json;

namespace ConsoleApp.Manager;

public class Write
{
    public bool ChoreInfoFile(List<ChoresInfo> choresInfo)
    {
        string filePath = "../ConsoleApp/Data/ChoreInfos.json";
        string data = JsonSerializer.Serialize<List<ChoresInfo>>(choresInfo);
        try
        {
            File.WriteAllText(filePath, data);
            return true;
        }
        catch (System.Exception)
        {

            throw;
        }
        return false;
    }
}