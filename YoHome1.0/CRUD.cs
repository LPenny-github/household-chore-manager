using System;
using System.Collections.Generic;
using System.IO;

namespace HouseholdChoreManager
{
    public class CRUD
    {
        public List<string> recommendHousework(DateTime today, string tablePath, string detailPath)
        {
            List<int> houseworkSerialNumbers = new List<int>();
            
            string[] houseworkDetailText = File.ReadAllLines(detailPath);
            DateTime lastDate = DateTime.Today;
            foreach (var item in houseworkDetailText)
            {
                string[] houseworkItem = item.Split(",");
                if (DateTime.TryParse(houseworkItem[3], out DateTime date))
                {
                    lastDate = date;
                }
                
                 
                if (DateTime.Compare(lastDate, DateTime.Now) <= 0)
                {
                    houseworkSerialNumbers.Add(Convert.ToInt32(houseworkItem[0]));
                }
            }

            List<string> houseworkToDoList = new List<string>();
            string[] houseworkKeyTableText = File.ReadAllLines(tablePath);
            Dictionary<int, string> houseworkKeyTable = new Dictionary<int, string>();
            foreach (var item in houseworkKeyTableText)
            {
                string[] houseworkKeyTableItem = item.Split(",");
                houseworkKeyTable[Convert.ToInt32(houseworkKeyTableItem[0])] = houseworkKeyTableItem[1];
            }
            foreach (var item in houseworkSerialNumbers)
            {
                string houseworkName = houseworkKeyTable[item];
                houseworkToDoList.Add(houseworkName);
            }
            return houseworkToDoList;
        }
    }
}