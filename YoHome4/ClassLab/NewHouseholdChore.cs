using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Linq;

namespace ClassLab
{

    public class NewHouseholdChore
    {
        int householdChoreSerialNumber;

        // 優先更新 householdChoreInformation 資料
        List<HouseholdChoreInformation> preData = new DataReader().JsonToList();

        // int recordSerialNumber = 0;

        void GetLastHouseholdChoreSerialNumber()
        {
            if (preData.Count() > 0)
            {
                householdChoreSerialNumber = preData.Last().HouseholdChoreSerialNumber;
            }
        }

        public List<HouseholdChoreInformation> SearchSimilarHouseholdChore(string keyword)
        {
            List<HouseholdChoreInformation> similarHouseholdChore = new();
            GetLastHouseholdChoreSerialNumber();
            if (householdChoreSerialNumber < 1) // 家事序號 < 1，代表沒有任何家事資料
            {
                return similarHouseholdChore;
            }


            // 不精確搜尋 / fuzzy search 
            similarHouseholdChore = preData.Where(h => h.HouseholdChoreName.Contains(keyword)).ToList();

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
                preData.Add(householdChoreData);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                string SerializedString = JsonSerializer.Serialize<List<HouseholdChoreInformation>>(preData, options);

                return (true, null, SerializedString);
            }

        }
    }
}