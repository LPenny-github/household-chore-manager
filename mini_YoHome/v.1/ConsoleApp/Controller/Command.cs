using ConsoleApp.Model;
using ConsoleApp.Manager;
using ConsoleApp.View;

namespace ConsoleApp.Controller;

public class Command
{
    public void Executor(string[] userInput, List<ChoresInfo> choresInfos, List<ChoresRecord> choresRecords)
    {
        string userCommand = userInput[0].ToLowerInvariant();
        string resultString = "";
        bool isSuccessful = false;

        Write write = new();
        Print print = new();
        Select select = new();

        switch (userCommand)
        {
            case "new":
                {
                    ChoresInfo data = new()
                    {
                        SerialNumber = choresInfos.Count() + 1,
                        Name = userInput[1],
                        IdealFrequency = Convert.ToInt16(userInput[2])
                    };
                    choresInfos.Add(data);
                    isSuccessful = write.ChoreInfoFile(choresInfos);
                    resultString = isSuccessful ? "新增家事基本資料成功" : "新增家事基本資料失敗";
                }
                break;
            case "info":
                {
                    isSuccessful = print.FormatInfo(choresInfos);
                    resultString = isSuccessful ? "列印資料成功" : "列印資料失敗";
                }
                break;
            case "record":
                {
                    isSuccessful = print.FormatRecord(choresInfos, choresRecords);
                    resultString = isSuccessful ? "列印紀錄成功" : "列印紀錄失敗";
                }
                break;
            case "todo":
                {
                    List<ChoresInfo> searchResult = new();
                    searchResult = select.Todo(choresInfos);
                    isSuccessful = print.FormatInfo(searchResult);
                    resultString = isSuccessful ? "輸出資料成功" : "輸出資料失敗";
                }
                break;
            case "add":
                {   
                    int choresNum = Convert.ToInt32(userInput[1]);
                    
                    var infoObj = choresInfos.Where(a => a.SerialNumber == choresNum);
                    DateTime lastDate = infoObj.Select(b => b.LastImplementedDate).SingleOrDefault();
                    
                    DateTime builtDate = DateTime.Today;
                    if (userInput.Length == 3)
                    {
                        builtDate = Convert.ToDateTime(userInput[2]);
                    }
                    
                    int newFrequency = 0;
                    int oldFrequency = Convert.ToInt32(infoObj.Select(a => a.IdealFrequency).Single());
                    string? note = null;
                    
                    
                    if (lastDate != default)
                    {
                        newFrequency = (builtDate - lastDate).Days;
                        note = $"old frequency is {infoObj.Select(a => a.IdealFrequency).Single()}";
                    }
                    
                    ChoresRecord data = new()
                    {
                        SerialNumber = choresRecords.Count() + 1,
                        ChoreSerialNumber = choresNum,
                        Note = note
                    };
                    choresRecords.Add(data);
                    isSuccessful = write.ChoreDecordFile(choresRecords);


                    ChoresInfo info = (from i in choresInfos where i.SerialNumber == choresNum
                                            select i).Single();
                        info.LastImplementedDate = builtDate;
                        info.IdealFrequency = newFrequency == 0? oldFrequency:newFrequency;
                    
                    isSuccessful = write.ChoreInfoFile(choresInfos);
                
                    resultString = isSuccessful ? "資料儲存成功" : "資料儲存失敗";
                }
                break;
            default:
                {
                    resultString = "查無此指令";
                }
                break;
        }

        print.Result(isSuccessful, resultString);
    }
}