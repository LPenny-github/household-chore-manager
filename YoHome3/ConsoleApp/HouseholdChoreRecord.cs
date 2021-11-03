using System;
namespace ConsoleApp
{
    class HouseholdChoreRecord
    {
        public int RecordSerialNumber { get; set; }
        public DateTime ExpectedDate { get; set; }
        public int HouseholdChoreSerialNumber { get; set; }
        public bool HadThisHouseholdChoreDone { get; set; }
        public int RealFrequency { get; set; }
        public string Note { get; set; }
    }
}