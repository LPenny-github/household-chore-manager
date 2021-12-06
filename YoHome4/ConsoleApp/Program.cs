using System;
using ClassLab;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] arguments)
        {
            string GetArgument(int index, string name)
            {
                if (arguments.Length < 1)
                {
                    return "";
                }
                return arguments[index];
            }
            string command = GetArgument(0, nameof(command)).ToLowerInvariant();
            OperationResultStringMaker operationResultStringMaker = new();
            string message;
            HouseholdChoreManager householdChoreManager = new();
            switch (command)
            {
                case "": // user input: ""
                    {
                        message = operationResultStringMaker.StringMaker("執行指令", false, "未輸入任何指令");
                        Console.WriteLine(message);
                    }
                    break;
                case "new":
                    {
                        const string commandPurpose = "建立新家事";

                        // user input: "new"
                        if (arguments.Length == 1)
                        {
                            message = operationResultStringMaker.StringMaker(commandPurpose, false, "未輸入家事名稱與頻率");
                            Console.WriteLine(message);
                        }

                        // user input: "new 洗衣服"
                        else if (arguments.Length == 2)
                        {
                            message = operationResultStringMaker.StringMaker(commandPurpose, false, "未輸入家事頻率");
                            Console.WriteLine(message);
                        }

                        else if (arguments.Length == 3)
                        {
                            string name = arguments[1];
                            int frequency = -1;
                            bool isNumber = Int32.TryParse(arguments[2], out frequency);

                            // user input: "new 洗衣服 !" or "new 洗衣服 0"
                            if (!isNumber || frequency < 1)
                            {
                                string userInputFrequency = arguments[2];
                                message = operationResultStringMaker.StringMaker(commandPurpose, false, $"家事頻率({userInputFrequency})不合理");
                                Console.WriteLine(message);
                            }

                            // user input: "new 洗衣服 14"
                            else
                            {
                                var result = householdChoreManager.CreateHouseholdChore(arguments[1], frequency);
                                if (result.valid)
                                {
                                    message = new DataWriter().BuildNewChoreItem(commandPurpose, result.jsonString);
                                    Console.WriteLine(message);
                                }
                                else
                                {
                                    message = operationResultStringMaker.StringMaker(commandPurpose, result.valid, result.errorMessage);
                                    Console.WriteLine(message);
                                }

                            }
                        }

                        // user input: "new 刷牙 10 好棒棒"
                        else if (arguments.Length > 3)
                        {
                            message = operationResultStringMaker.StringMaker(commandPurpose, false, "輸入過多參數或詞彙、數組間有多餘空白");
                            Console.WriteLine(message);
                        }
                    }
                    break;
                default:
                    message = operationResultStringMaker.StringMaker("執行指令", false, $"找不到該指令({command})相應的動作");
                    Console.WriteLine(message);
                    break;
            }
        }
    }
}
