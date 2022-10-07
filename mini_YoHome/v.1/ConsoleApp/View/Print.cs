using ConsoleApp.Model;

namespace ConsoleApp.View;

public class Print
{
    public void Result(bool isSuccessful, string resultString)
    {
        Console.WriteLine($"執行是否成功： {isSuccessful}");
        Console.WriteLine($"結果為： {resultString}");
    }

    public bool FormatInfo(List<ChoresInfo> customData)
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
    public bool FormatRecord(List<ChoresInfo> infos ,List<ChoresRecord> customData)
    {
        try
        {
            foreach (var item in customData)
            {
                string choresName = infos.Where(a => a.SerialNumber == item.SerialNumber).Select(b => b.Name).ToString(); 
                Console.WriteLine($"編號: {item.ChoreSerialNumber}, 建立日: {item.BuiltDate}, 家事名稱: {choresName}, 真實頻率: {item.RealFrequency}, 備註: {item.Note}");
            }
        }
        catch (System.Exception)
        {
            return false;
        }
        return true;
    }
}