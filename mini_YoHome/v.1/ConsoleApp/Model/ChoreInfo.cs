namespace ConsoleApp.Model;

public class ChoresInfo
{
    public int SerialNumber { get; set; }
    public string Name { get; set; } = "未命名";
    public int IdealFrequency { get; set; }
    public string? LastImplementedDate { get; set; } = null;
    public bool Alert { get; set; } = true;
}