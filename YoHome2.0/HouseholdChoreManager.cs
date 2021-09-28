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

        // HouseholdChoreInformation householdChoreInformation = new HouseholdChoreInformation();
        List<HouseholdChoreInformation> householdChoreInformation = new List<HouseholdChoreInformation>();

        List<HouseholdChoreInformation> JsonToList()
        { 
            if (File.Exists(fileName))
            {
                var householdChoreInformationJson = File.ReadAllText(fileName);
                householdChoreInformation = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>(householdChoreInformationJson);             
            }
            return householdChoreInformation;
        }

        List<string> SearchSimilarHouseholdChore(string keyword)
        {
            List<string> similarHouseholdChore = new List<string>();

            if (householdChoreSerialNumber < 1) // 家事序號 < 1，代表沒有任何家事資料
            {
                return similarHouseholdChore;
            }

            // 不精確搜尋 / fuzzy search 
            // similarHouseholdChore = householdChoreInformation.Where(h => h.HouseholdChoreName.Contains(keyword) || h.HouseholdChoreName.Any(h.HouseholdChoreName.Contains(keyword))).Select(n => n.HouseholdChoreName);

            return similarHouseholdChore;
        }


        bool AreCandidatesMatchingSearchPattern(string candidateName, int searchPattern, List<string> similarHouseholdChore)
        {

            // 進階搜尋 / Advanced Search
            string[] accurateHouseholdChore = similarHouseholdChore.Where(h => h.Contains(candidateName)).Select(n => n).ToArray();
            return accurateHouseholdChore.Length == searchPattern;
        }

        public void CreateHouseholdChore(string name, int frequency)
        {
            if (!AreCandidatesMatchingSearchPattern(name, 0, SearchSimilarHouseholdChore(name))) { /* 無法建立家事 */  };
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
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}