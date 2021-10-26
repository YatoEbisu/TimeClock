using System;
using System.Text.Json.Serialization;
using TimeClock.API.Entities;

namespace TimeClock.API.DTOs
{
    public class ResponseTimeClockDTO
    {
        public Guid Id { get; set; }

        [JsonPropertyName("EntryTime")]
        public string _entryTime { get; set; }
        [JsonPropertyName("ExitTime")]
        public string _exitTime { get; set; }
        [JsonPropertyName("PointDate")]
        public string _pointDate { get; set; }


        [JsonIgnore]
        public TimeSpan? EntryTime { get; set; }
        [JsonIgnore]
        public TimeSpan? ExitTime { get; set; }
        [JsonIgnore]
        public DateTime PointDate { get; set; }

    }
}
