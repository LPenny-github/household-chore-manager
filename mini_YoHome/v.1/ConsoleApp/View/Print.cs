using ConsoleApp.Model;

namespace ConsoleApp.View;

public class Print
{
    public void Result(bool isSuccessful, string resultString)
    {
        // 需要知道結果字串，特別當新增完一筆資料時
        // 但可以忽略成功與否(true/false)，因使用者能從結果字串獲得此資訊

        // Console.WriteLine($"執行是否成功： {isSuccessful}");
        Console.WriteLine($"結果為： {resultString}");
    }

    public bool DataInfo(DateTime date, int count)
    {
        string formatDate = date.ToString("yyyy/MM/dd");
        Console.WriteLine("最後紀錄日: " + formatDate + "\n數量: " + count);
        return true;
    }

    public bool FormatInfo(List<ChoresInfo> customData)
    {
        try
        {
            foreach (var item in customData)
            {
                var definedLastDate = item.LastImplementedDate == default? "無": item.LastImplementedDate.ToString("d");
                Console.WriteLine($"編號: {item.SerialNumber},名稱: {item.Name}, 理想頻率: {item.IdealFrequency}, 上次執行日: {definedLastDate}, 提醒: {item.Alert}");
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
                string choresName = infos.Where(a => a.SerialNumber == item.ChoreSerialNumber).Select(b => b.Name).Single(); 
                Console.WriteLine($"編號: {item.SerialNumber}, 建立日: {item.BuiltDate.ToString("d")}, 家事名稱: {choresName}, 備註: {item.Note}");
            }
        }
        catch (System.Exception)
        {
            return false;
        }
        return true;
    }
}