using ConsoleApp.Model;

namespace ConsoleApp.View;

public class Print
{
    public void Result(bool isSuccessful, string resultString)
    {
        Console.WriteLine($"執行是否成功： {isSuccessful}");
        Console.WriteLine($"結果為： {resultString}");
    }

    public bool FormatData(List<ChoresInfo> customData)
    {
        try
        {
            foreach (var item in customData)
            {
                Console.WriteLine($"編號: {item.SerialNumber},名稱: {item.Name}, 理想頻率: {item.IdealFrequency}, 上次執行日: {item.LastImplementedDate}, 提醒: {item.Alert}");
            }
        }
        catch (System.Exception)
        {
            return false;
        }
        return true;
    }
}