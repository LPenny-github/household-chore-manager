namespace ConsoleApp.View;

public class Print
{
    public void result(bool isSuccessful ,string resultString)
    {
        Console.WriteLine($"執行是否成功： {isSuccessful}");
        Console.WriteLine($"結果為： {resultString}");
    }
}