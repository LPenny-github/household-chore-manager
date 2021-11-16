using System;

namespace ClassLab
{
    public class OperationResultStringMaker: IDataWriter
    {
        public void StringMaker(string purpose, bool result, string prompt = null)
        {
            if (result)
            {
                WriteData($" {purpose} 執行成功！");
            }
            else
            {
                WriteData($" {purpose} 執行失敗，由於 {prompt}");
            }
        }

        public void WriteData(string result)
        {
            Console.WriteLine(result);
        }
    }
}