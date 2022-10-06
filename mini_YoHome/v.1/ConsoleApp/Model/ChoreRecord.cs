namespace ConsoleApp.Model;

public class ChoresRecord
{
    public int SerialNumber { get; set; } = 0;
    public string BuiledDate { get; set; } = DateTime.Today.ToString("d");
    public int ChoreSerialNumber { get; set; }
    public int RealFrequency { get; set; }
    public string? Note { get; set; } = null;
}