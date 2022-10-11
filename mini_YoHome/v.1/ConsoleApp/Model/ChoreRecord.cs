namespace ConsoleApp.Model;

public class ChoresRecord
{
    public int SerialNumber { get; set; } = 0;
    public DateTime BuiltDate { get; set; } = DateTime.Today;
    public int ChoreSerialNumber { get; set; }
    public string? Note { get; set; } = null;
}