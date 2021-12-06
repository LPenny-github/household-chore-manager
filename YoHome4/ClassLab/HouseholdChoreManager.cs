using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Linq;

namespace ClassLab
{

    public class HouseholdChoreManager
    {
        int householdChoreSerialNumber;
        // int recordSerialNumber = 0;
        const string fileName = "../Data/HouseholdChoreInformation.json";

        List<HouseholdChoreInformation> householdChoreInformation = new();
        OperationResultStringMaker operationResultStringMaker = new();

        List<HouseholdChoreInformation> JsonToList()
        {
            if (File.Exists(fileName))
            {
                var householdChoreInformationJson = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(householdChoreInformationJson))
                {
                    householdChoreInformation = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>(householdChoreInformationJson);
                    householdChoreSerialNumber = householdChoreInformation.Last().HouseholdChoreSerialNumber;
                }
            }
            return householdChoreInformation;
        }

        public List<HouseholdChoreInformation> SearchSimilarHouseholdChore(string keyword)
        {
            // 執行搜尋前，先更新 householdChoreInformation 資料
            JsonToList();

            List<HouseholdChoreInformation> similarHouseholdChore = new();

            if (householdChoreSerialNumber < 1) // 家事序號 < 1，代表沒有任何家事資料
            {
                return similarHouseholdChore;
            }


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

        public (bool valid, string errorMessage, string jsonString) CreateHouseholdChore(string name, int frequency)
        {
            if (!AreCandidatesMatchingSearchPattern(0, SearchSimilarHouseholdChore(name)))
            {
                return (false, "此家事已被建立", null);
            }
            else
            {
                HouseholdChoreInformation householdChoreData = new HouseholdChoreInformation
                {
                    HouseholdChoreSerialNumber = householdChoreSerialNumber + 1,
                    HouseholdChoreName = name,
                    IdealFrequency = frequency,
                    LastImplementedDate = new DateTime()
                };
                householdChoreInformation.Add(householdChoreData);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                string SerializedString = JsonSerializer.Serialize<List<HouseholdChoreInformation>>(householdChoreInformation, options);
                
                return (true, null, SerializedString);
            }

        }
    }
}