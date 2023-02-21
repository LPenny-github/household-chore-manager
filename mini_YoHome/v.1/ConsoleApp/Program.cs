using ConsoleApp.Manager;
using ConsoleApp.Model;
using ConsoleApp.Controller;

List<ChoresInfo> choresInfos = Read.ChoreInfoFile();
List<ChoresRecord> choresRecords = Read.ChoreRecordFile();

// Console.WriteLine("請輸入指令：");
string[] inputs = args;
Command command = new();
command.Executor(inputs, choresInfos, choresRecords);
