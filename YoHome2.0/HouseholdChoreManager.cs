using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Linq;

namespace YoHome
{

    public class HouseholdChoreManager
    {
        int householdChoreSerialNumber = 0;
        int recordSerialNumber = 0;
        const string fileName = "HouseholdChoreInformation.json";

        List<HouseholdChoreInformation> householdChoreInformation = new();
        OperationResultStringMaker operationResultStringMaker = new();

        List<HouseholdChoreInformation> JsonToList()
        {
            if (File.Exists(fileName))
            {
                var householdChoreInformationJson = File.ReadAllText(fileName);
                householdChoreInformation = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>(householdChoreInformationJson);
            }
            return householdChoreInformation;
        }

        public List<HouseholdChoreInformation> SearchSimilarHouseholdChore(string keyword)
        {
            List<HouseholdChoreInformation> similarHouseholdChore = new();

            if (householdChoreSerialNumber < 1) // 家事序號 < 1，代表沒有任何家事資料
            {
                return similarHouseholdChore;
            }

            // 執行搜尋前，先更新 householdChoreInformation 資料
            JsonToList();

            // 不精確搜尋 / fuzzy search 
            similarHouseholdChore = householdChoreInformation.Where(
                                                                h => h.HouseholdChoreName.Contains(keyword))
                                                                                         .ToList();

            return similarHouseholdChore;
        }


        bool AreCandidatesMatchingSearchPattern(int searchPattern, List<HouseholdChoreInformation> similarHouseholdChore)
        {
            return similarHouseholdChore.Count() == searchPattern;
        }

        public void CreateHouseholdChore(string name, int frequency)
        {
            string purpose = $"建立新家事({name})";
            if (!AreCandidatesMatchingSearchPattern(0, SearchSimilarHouseholdChore(name)))
            {   
                operationResultStringMaker.StringMaker(purpose, false, "此家事已被建立");
            };
            HouseholdChoreInformation householdChoreData = new HouseholdChoreInformation
            {
                HouseholdChoreSerialNumber = householdChoreSerialNumber + 1,
                HouseholdChoreName = name,
                IdealFrequency = frequency,
                LastImplementedDate = new DateTime()
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            string SerializedString = JsonSerializer.Serialize<HouseholdChoreInformation>(householdChoreData, options);

            try
            {
                File.AppendAllText(fileName, SerializedString.ToString());
                operationResultStringMaker.StringMaker(purpose, true);
            }
            catch (IOException) // An I/O error occurred while opening the file.
            {
                operationResultStringMaker.StringMaker(purpose, false, $"檔案({fileName})使用中");
            }
        }
    }
}