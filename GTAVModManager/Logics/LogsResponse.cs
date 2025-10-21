namespace GTAVModManager.Logics
{
    public class LogsResponse
    {
        public required List<LogEntry> Logs { get; set; }
        public int Count { get; set; }
    }
}
