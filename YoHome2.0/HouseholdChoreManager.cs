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

            return similarHouseholdChore;
        }


        bool HasCandidateExisted(string candidateName, int searchPattern)
        {
            Dictionary<int, string> SimilarHouseholdChore = new Dictionary<int, string>();
            SimilarHouseholdChore = SearchSimilarHouseholdChore(candidateName);
            return SimilarHouseholdChore.Count == searchPattern;
        }

        void CreateHouseholdChore(string name, int frequency)
        {
            if (!HasCandidateExisted(name, 0)) { } ;
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
            string SerializedString = JsonSerializer.Serialize<HouseholdChoreInformation>(householdChoreData,options);

            try
            {
            File.AppendAllText("HouseholdChoreInformation.json", SerializedString.ToString());
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}