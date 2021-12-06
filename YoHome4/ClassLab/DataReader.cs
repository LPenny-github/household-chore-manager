using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClassLab
{
    public class DataReader
    {
        const string fileName = "../Data/HouseholdChoreInformation.json";
        List<HouseholdChoreInformation> householdChoreInformation = new();
        public List<HouseholdChoreInformation> JsonToList()
        {
            if (File.Exists(fileName))
            {
                var householdChoreInformationJson = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(householdChoreInformationJson))
                {
                    householdChoreInformation = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>
                                                                            (householdChoreInformationJson);
                }
            }
            return householdChoreInformation;
        }
    }
}