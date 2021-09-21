using System.Collections.Generic;

namespace YoHome
{
    public class HouseholdChoreManager
    {
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
    }
}