using Newtonsoft.Json;

namespace GTAVModManager.Logics
{
    public class StatusResponse
    {
        public string Version { get; set; }
        [JsonProperty("uptime_seconds")]
        public long UptimeSeconds { get; set; }
        [JsonProperty("mods_loaded")]
        public int ModsLoaded { get; set; }
        [JsonProperty("server_running")]
        public bool ServerRunning { get; set; }
        [JsonProperty("game_detected")]
        public bool GameDetected { get; set; }
    }
}
