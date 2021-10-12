using System;

namespace YoHome
{
    public class OperationResultStringMaker
    {
        public void StringMaker(string purpose, bool result, string prompt = null)
        {
            if (result)
            {
                Print($" {purpose} 執行成功！");
            }
            else
            {
                Print($" {purpose} 執行失敗，由於 {prompt}");
            }
        }

        public void Print(string result)
        {
            Console.WriteLine(result);
        }
    }
}