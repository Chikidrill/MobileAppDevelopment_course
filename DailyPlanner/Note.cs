namespace DailyPlanner;

public class Note
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
}
