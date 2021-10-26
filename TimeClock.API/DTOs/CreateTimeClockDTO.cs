using System;
using System.Text.Json.Serialization;
using TimeClock.API.Entities;

namespace TimeClock.API.DTOs
{
    public class CreateTimeClockDTO
    {
        [JsonPropertyName("EntryTime")]
        public string _entryTime { get; set; }
        [JsonPropertyName("ExitTime")]
        public string _exitTime { get; set; }

        [JsonIgnore]
        public TimeSpan? EntryTime { get; set; }
        [JsonIgnore]
        public TimeSpan? ExitTime { get; set; }
        public DateTime PointDate { get; set; }
    }
}
