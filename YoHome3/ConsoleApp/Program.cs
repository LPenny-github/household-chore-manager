using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] arguments)
        {
            OperationResultStringMaker operationResultStringMaker = new();
            string GetArgument(int index, string name)
            {
                if (arguments.Length < 1)
                {
                    operationResultStringMaker.StringMaker("執行指令", false, "未輸入任何指令");
                }
                return arguments[index];
            }
            string command = GetArgument(0, nameof(command)).ToLowerInvariant();
            HouseholdChoreManager householdChoreManager = new();
            switch (command)
            {
                case "new": // user input: "new 洗衣服 14"
                    {
                        int frequency = -1;
                        bool isNumber = Int32.TryParse(arguments[2], out frequency);
                        if (!isNumber || frequency < 0)
                        {
                            operationResultStringMaker.StringMaker("建立新家事", false, $"家事頻率({frequency})不合理");
                        }
                        else
                        {
                            householdChoreManager.CreateHouseholdChore(arguments[1], frequency);
                        }
                    }
                    break;
                default:
                    operationResultStringMaker.StringMaker("執行指令", false, $"找不到該指令({command})相應的動作");
                    break;
            }
        }
    }
}
