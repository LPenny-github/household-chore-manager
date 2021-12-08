using System;
namespace ClassLab
{
    public class CheckCommandNew
    {
        bool IsValid = false;
        string errorMessage = null;

        int frequency = -1;
        public (bool IsValid, string errorMessage, int frequency) ReturnCommandNewResult(string[] arguments)
        {
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
                    IsValid = true;
                }
            }

            // user input: "new 刷牙 10 好棒棒"
            else if (arguments.Length > 3)
            {
                errorMessage = "輸入過多參數或詞彙、數組間有多餘空白";
            }

            return (IsValid, errorMessage, frequency);
        }
    }
}