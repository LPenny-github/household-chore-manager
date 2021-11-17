using System.IO;

namespace ClassLab
{
    public class DataWriter 
    {
        const string fileName = "../Data/HouseholdChoreInformation.json";
        OperationResultStringMaker operationResultStringMaker = new();

        public string BuildNewChoreItem(string purpose, string jsonString)
        {
            try
            {
                WriteData(jsonString);
                return operationResultStringMaker.StringMaker(purpose, true);
            }
            catch (IOException) // An I/O error occurred while opening the file.
            {
                return operationResultStringMaker.StringMaker(purpose, false, $"檔案({fileName})使用中");
            }
        }

        public void WriteData(string content)
        {
            File.WriteAllText(fileName, content);
        }
    }
}