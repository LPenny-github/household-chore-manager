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
            case "new": // 使用者輸入範例: new 整理雜物 
                {
                    if (!String.IsNullOrEmpty(userInput[1]))
                    {
                        ChoresInfo data = new()
                        {
                            SerialNumber = choresInfos.Count() + 1,
                            Name = userInput[1],

                            // 有時間再增加此功能
                            // IdealFrequency = Convert.ToInt16(userInput[2])
                        };
                        choresInfos.Add(data);
                        isSuccessful = write.ChoreInfoFile(choresInfos);
                    }

                    resultString = isSuccessful ? "新增家事基本資料成功" : "新增家事基本資料失敗";
                }
                break;
            case "editalert": // 使用者輸入範例: editalert 11 false
                {
                    choresInfos.Where(item => item.SerialNumber == Convert.ToInt16(userInput[1])).ToList()
                                            .ForEach(i => i.Alert = Convert.ToBoolean(userInput[2]));
                   
                    isSuccessful = write.ChoreInfoFile(choresInfos);
                    resultString = isSuccessful ? "更改家事基本資料成功" : "更改家事基本資料失敗";
                }
                break;
            case "editname": // 使用者輸入範例: editname 11 清潔地板
                {
                    choresInfos.Where(item => item.SerialNumber == Convert.ToInt16(userInput[1])).ToList()
                                            .ForEach(i => i.Name = userInput[2]);

                    isSuccessful = write.ChoreInfoFile(choresInfos);
                    resultString = isSuccessful ? "更改家事基本資料成功" : "更改家事基本資料失敗";
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
            case "add": // 使用者輸入範例: add 1 2022/7/4
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


                    ChoresInfo info = (from i in choresInfos
                                       where i.SerialNumber == choresNum
                                       select i).Single();
                    info.LastImplementedDate = builtDate;
                    info.IdealFrequency = newFrequency == 0 ? oldFrequency : newFrequency;

                    isSuccessful = write.ChoreInfoFile(choresInfos);

                    resultString = isSuccessful ? "資料儲存成功" : "資料儲存失敗";
                }
                break;
            case "lastdate":
                {
                    DateTime searchResultDate = new();
                    searchResultDate = select.LastRecordDate(choresInfos);
                    int searchResulCount = select.NumberOfLastRecordDate(choresInfos, searchResultDate);
                    isSuccessful = print.DataInfo(searchResultDate, searchResulCount);
                    resultString = isSuccessful ? "輸出資料成功" : "輸出資料失敗";
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