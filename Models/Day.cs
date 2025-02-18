using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Schedule2PDF_Console.Models;

public class Day
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DayOfWeek DayOfWeek { get; set; }
    public List<Lesson> Lessons { get; set; } = [];
}
