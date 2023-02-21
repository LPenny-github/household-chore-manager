using ConsoleApp.Model;

namespace ConsoleApp.Manager;

public class Select
{
    public List<ChoresInfo> Todo(List<ChoresInfo> customData)
    {
        List<ChoresInfo> todoList = new();
        
        foreach (var data in customData)
        {
            DateOnly executingDay = default;
            if (data.LastImplementedDate != default)
            {
                executingDay = DateOnly.FromDateTime(data.LastImplementedDate.AddDays(data.IdealFrequency));
            }
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (data.LastImplementedDate == default || executingDay.CompareTo(today) <= 0)
            {
                todoList.Add(data);
            }

            if (!data.Alert)
            {
                todoList.Remove(data);
            }
        }

        return todoList;
    }

    public DateTime LastRecordDate(List<ChoresInfo> customData)
    {
        DateTime lastRecordDate = customData.First().LastImplementedDate;

        foreach (var data in customData)
        {
            if (data.LastImplementedDate > lastRecordDate)
            {
                lastRecordDate = data.LastImplementedDate;
            }
        }
        return lastRecordDate;
    }
}