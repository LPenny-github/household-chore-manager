namespace ConsoleApp.Model;

public class ChoresInfo
{
    public int SerialNumber { get; set; } = 0;
    public string Name { get; set; } = "未命名";
    public int IdealFrequency { get; set; } = 100;
    public DateTime LastImplementedDate { get; set; } = default;
    public bool Alert { get; set; } = true;
}