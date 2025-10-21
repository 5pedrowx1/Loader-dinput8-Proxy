using Newtonsoft.Json;

namespace GTAVModManager.Logics
{
    public class Mod
    {
        public required string ID { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public bool Loaded { get; set; }
        public required string Type { get; set; }
        public long Size { get; set; }
        [JsonProperty("load_time")]
        public required string LoadTime { get; set; }
    }
}
