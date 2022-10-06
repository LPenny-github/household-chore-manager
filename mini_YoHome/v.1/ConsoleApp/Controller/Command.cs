using ConsoleApp.Model;
using ConsoleApp.Manager;

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
                }
                break;
            default:
                {

                }
                break;
        }
        // call to print the result
    }
}