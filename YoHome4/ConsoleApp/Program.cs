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

            string commandPurpose = "執行指令";
            bool IsSuccessful = false;
            string errorMessage = null;
            string message;
            
            NewHouseholdChore newHouseholdChore = new();
            switch (command)
            {
                case "": // user input: ""
                    {
                        errorMessage = "未輸入任何指令";
                    }
                    break;
                case "new":
                    {
                        commandPurpose = "建立新家事";

                        // user input: "new"
                        if (arguments.Length == 1)
                        {
                            errorMessage = "未輸入家事名稱與頻率";
                        }

                        // user input: "new 洗衣服"
                        else if (arguments.Length == 2)
                        {
                            errorMessage = "未輸入家事頻率";
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
                                errorMessage = $"家事頻率({userInputFrequency})不合理";
                            }

                            // user input: "new 洗衣服 14"
                            else
                            {
                                var result = newHouseholdChore.CreateHouseholdChore(arguments[1], frequency);
                                if (result.valid)
                                {
                                    message = new DataWriter().BuildNewChoreItem(commandPurpose, result.jsonString);
                                }
                                else
                                {
                                    errorMessage = result.errorMessage;
                                }

                            }
                        }

                        // user input: "new 刷牙 10 好棒棒"
                        else if (arguments.Length > 3)
                        {
                            errorMessage = "輸入過多參數或詞彙、數組間有多餘空白";
                        }
                    }
                    break;
                default:
                    errorMessage = $"找不到該指令({command})相應的動作";
                    break;
            }

            OperationResultStringMaker operationResultStringMaker = new();
            message = operationResultStringMaker.StringMaker(commandPurpose, IsSuccessful, errorMessage);
            Console.WriteLine(message);
        }
    }
}
