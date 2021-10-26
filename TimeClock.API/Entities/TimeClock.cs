using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeClock.API.Entities
{
    [Table("TimesClock")]
    public class TimeClock
    {
        public Guid Id { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan ExitTime { get; set; }
        public DateTime PointDate { get; set; }
    }
}
