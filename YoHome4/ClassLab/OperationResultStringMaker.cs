using System;

namespace ClassLab
{
    public class OperationResultStringMaker
    {
        public string StringMaker(string purpose, bool result, string prompt = null)
        {
            if (result)
            {
                return $" {purpose} 執行成功！";
            }
            else
            {
                return $" {purpose} 執行失敗，由於 {prompt}";
            }
        }
    }
}