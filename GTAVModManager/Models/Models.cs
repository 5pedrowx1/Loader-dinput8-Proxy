using System.Text.Json.Serialization;

namespace GTAVModManager.Models
{
    public class ModInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("path")]
        public string Path { get; set; } = "";

        [JsonPropertyName("loaded")]
        public bool Loaded { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("call_count")]
        public long CallCount { get; set; }

        [JsonPropertyName("avg_execution_us")]
        public long AvgExecutionUs { get; set; }

        [JsonPropertyName("load_time")]
        public string LoadTime { get; set; } = "";

        public string SizeFormatted => FormatBytes(Size);

        private static string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }

    public class ModsResponse
    {
        [JsonPropertyName("mods")]
        public List<ModInfo> Mods { get; set; } = new();

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class StatusInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "";

        [JsonPropertyName("uptime_seconds")]
        public long UptimeSeconds { get; set; }

        [JsonPropertyName("mods_loaded")]
        public int ModsLoaded { get; set; }

        [JsonPropertyName("server_running")]
        public bool ServerRunning { get; set; }

        [JsonPropertyName("performance_monitor_active")]
        public bool PerformanceMonitorActive { get; set; }

        [JsonPropertyName("game_detected")]
        public bool GameDetected { get; set; }

        public string UptimeFormatted
        {
            get
            {
                var ts = TimeSpan.FromSeconds(UptimeSeconds);
                return $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
            }
        }
    }

    public class LogEntry
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = "";

        [JsonPropertyName("level")]
        public string Level { get; set; } = "";

        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }

    public class LogsResponse
    {
        [JsonPropertyName("logs")]
        public List<LogEntry> Logs { get; set; } = new();

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class PerformanceInfo
    {
        [JsonPropertyName("memory_mb")]
        public long MemoryMb { get; set; }

        [JsonPropertyName("peak_memory_mb")]
        public long PeakMemoryMb { get; set; }

        [JsonPropertyName("cpu_percent")]
        public double CpuPercent { get; set; }

        [JsonPropertyName("thread_count")]
        public long ThreadCount { get; set; }

        [JsonPropertyName("handle_count")]
        public long HandleCount { get; set; }

        [JsonPropertyName("total_mods")]
        public long TotalMods { get; set; }

        [JsonPropertyName("uptime_seconds")]
        public long UptimeSeconds { get; set; }

        [JsonPropertyName("slowest_mod")]
        public string SlowestMod { get; set; } = "";

        [JsonPropertyName("slowest_mod_time_us")]
        public long SlowestModTimeUs { get; set; }
    }
}