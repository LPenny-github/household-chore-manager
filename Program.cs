using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HouseholdChoreManager
{
    class Program
    {
        public class houseworkData
        {
            public int serialNumber;
            public int frequency;
            public DateTime? lastDate;
            public DateTime? nextDate;
            public houseworkData(int serialNumber, int frequency = 0, DateTime? lastDate = null, DateTime? nextDate = null)
            {
                this.serialNumber = serialNumber;
                this.frequency = frequency;
                this.lastDate = lastDate;
                this.nextDate = nextDate;
            }
        }
        static void Main(string[] arguments)
        {
            string path = "housework-data.csv";
            string[] text = File.ReadAllLines(path);
            Dictionary<int, string> houseworkKeyTable = new Dictionary<int, string>();
            int number = 1;
            foreach (var item in text)
            {
                string[] houseworkItem = item.Split(",");
                for (int i = 1; i < houseworkItem.Length; ++i)
                {
                    if (!houseworkKeyTable.ContainsValue(houseworkItem[i]) && houseworkItem[i] != "")
                    {
                        houseworkKeyTable.Add(number, houseworkItem[i]);
                        ++number;
                    }
                }
            }
            houseworkData[] houseworkDetail = new houseworkData[houseworkKeyTable.Count];
            foreach (var item in houseworkKeyTable)
            {
                houseworkData data = new houseworkData(item.Key);
                DateTime lastExcuteDay = default;
                string houseworkName = houseworkKeyTable[data.serialNumber];
                int frequency = 0;
                CultureInfo provider = CultureInfo.InvariantCulture;
                foreach (var a in text)
                {
                    string[] houseworkItem = a.Split(",");
                    for (int i = 1; i < houseworkItem.Length; ++i)
                    {
                        if (houseworkItem[i] == houseworkName)
                        {
                            DateTime day = DateTime.Parse(houseworkItem[0]);
                            
                            TimeSpan interval = new TimeSpan(day.Ticks - lastExcuteDay.Ticks);
                            frequency = interval.Days > 90 || interval.Days <= 1 ? 90 : interval.Days;
                            lastExcuteDay = day;
                        }
                    }
                }
                data.frequency = frequency;
                data.lastDate = lastExcuteDay;
                if (frequency > 0)
                {
                    DateTime defaultValue = default;
                    if (lastExcuteDay != defaultValue)
                    {
                        data.nextDate = lastExcuteDay.AddDays(frequency);
                    }
                    else
                    {
                        data.nextDate = DateTime.Now.AddDays(1);
                    }
                }
                houseworkDetail[data.serialNumber - 1] = data;
            }
            
            string houseworkKeyTableText = String.Join(Environment.NewLine, houseworkKeyTable.Select(d => $"{d.Key},{d.Value}"));
            string houseworkKeyTablePath = "housework-key-table.csv";
            
            System.IO.File.WriteAllText(houseworkKeyTablePath, houseworkKeyTableText);
            
            string houseworkDetailText = String.Join(Environment.NewLine, houseworkDetail.Select(d => $"{d.serialNumber},{d.frequency},{d.lastDate:yyyy-MM-dd},{d.nextDate:yyyy-MM-dd}"));
            string houseworkDetailPath = "housework-detail.csv";
            
            System.IO.File.WriteAllText(houseworkDetailPath, houseworkDetailText);
            
            string GetArgument(int index, string name)
            {
                if (arguments.Length < 1)
                {
                    throw new ArgumentNullException(nameof(name), $"Argument `{name}` is empty.");
                }
                return arguments[index];
            }
            string command = GetArgument(0, nameof(command)).ToLowerInvariant();
            CRUD crud = new CRUD();
            switch (command)
            {
                case "todo":
                    {
                        List<string> recommendHouseworkList = crud.recommendHousework(DateTime.Today, houseworkKeyTablePath, houseworkDetailPath);
                        if (recommendHouseworkList.Count > 0)
                        {
                            foreach (var item in recommendHouseworkList)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("今日無代辦家事");
                        }
                    }
                    break;
                default:
                    Console.WriteLine($"System don't recognize this command: {command}");
                    break;
            }
        }
    }
}
