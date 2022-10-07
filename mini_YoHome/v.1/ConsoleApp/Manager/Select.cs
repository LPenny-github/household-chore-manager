using ConsoleApp.Model;

namespace ConsoleApp.Manager;

public class Select
{
    public List<ChoresInfo> Todo(List<ChoresInfo> customData)
    {
        List<ChoresInfo> todoList = new();
        
        foreach (var data in customData)
        {
            DateTime executingDay = Convert.ToDateTime(data.LastImplementedDate).AddDays(data.IdealFrequency);
            if (data.LastImplementedDate == null || executingDay <= DateTime.Today)
            {
                todoList.Add(data);
            }
        }

        return todoList;
    }
}