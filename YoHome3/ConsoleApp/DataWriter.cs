using System.IO;
using System;

namespace ConsoleApp
{
    public class DataWriter : IDataWriter
    {
        const string fileName = "../Data/HouseholdChoreInformation.json";
        OperationResultStringMaker operationResultStringMaker = new();

        public void BuildNewChoreItem(string purpose, string jsonString)
        {
            try
            {
                WriteData(jsonString);
                operationResultStringMaker.StringMaker(purpose, true);
            }
            catch (IOException) // An I/O error occurred while opening the file.
            {
                operationResultStringMaker.StringMaker(purpose, false, $"檔案({fileName})使用中");
            }
        }

        public void WriteData(string content)
        {
            File.WriteAllText(fileName, content);
        }
    }
}