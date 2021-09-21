using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

namespace YoHome
{
    public class HouseholdChoreManager
    {
        int householdChoreSerialNumber = 0;
        int recordSerialNumber = 0;
        Dictionary<int, string> SearchSimilarHouseholdChore(string keyword)
        {
            Dictionary<int, string> similarHouseholdChore = new Dictionary<int, string>();
            // 不精確搜尋
            return similarHouseholdChore;
        }


        bool HasCandidateExisted(string candidateName, int searchPattern)
        {
            Dictionary<int, string> SimilarHouseholdChore = SearchSimilarHouseholdChore(candidateName);
            // 精確搜尋
            return SimilarHouseholdChore.Count == searchPattern;
        }

        public void CreateHouseholdChore(string name, int frequency)
        {
            if (!HasCandidateExisted(name, 0)) { };
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
            string filePath = "HouseholdChoreInformation.json";
            
            try
            {
                File.AppendAllText(filePath, SerializedString.ToString());
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}