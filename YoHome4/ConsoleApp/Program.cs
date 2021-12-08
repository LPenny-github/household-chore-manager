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
            string message = null;

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

                        var checkCommandNewResult = new CheckCommandNew().ReturnCommandNewResult(arguments);
                        if (checkCommandNewResult.IsValid)
                        {
                            var result = new NewHouseholdChore().CreateHouseholdChore(arguments[1], checkCommandNewResult.frequency);
                            if (result.valid)
                            {
                                message = new DataWriter().BuildNewChoreItem(commandPurpose, result.jsonString);
                            }
                            else
                            {
                                errorMessage = result.errorMessage;
                            }
                        }
                        else
                        {
                            errorMessage = checkCommandNewResult.errorMessage;
                        }
                    }
                    break;
                default:
                    errorMessage = $"找不到該指令({command})相應的動作";
                    break;
            }

            OperationResultStringMaker operationResultStringMaker = new();
            if (string.IsNullOrEmpty(message))
            {
                message = operationResultStringMaker.StringMaker(commandPurpose, IsSuccessful, errorMessage);
            }
            Console.WriteLine(message);
        }
    }
}
