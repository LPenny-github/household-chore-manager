using ConsoleApp.Model;
using ConsoleApp.Manager;
using ConsoleApp.View;

namespace ConsoleApp.Controller;

public class Command
{
    public void Executor(string[] userInput, List<ChoresInfo> choresInfos, List<ChoresRecord> choresRecords)
    {
        string userCommand = userInput[0].ToLowerInvariant();
        string resultString = null;
        bool isSuccessful = false;
        Write write = new();
        switch (userCommand)
        {
            case "new":
                {
                    ChoresInfo data = new()
                    {
                        SerialNumber = choresInfos.Count() +1, 
                        Name = userInput[1],
                        IdealFrequency = Convert.ToInt16(userInput[2]) 
                    };
                    choresInfos.Add(data);
                    isSuccessful = write.ChoreInfoFile(choresInfos);
                    resultString = isSuccessful? "新增家事基本資料成功":"新增家事基本資料失敗";
                }
                break;
            default:
                {

                }
                break;
        }
        Print print = new();
        print.result(isSuccessful, resultString);
    }
}