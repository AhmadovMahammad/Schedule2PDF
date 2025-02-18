using System.Text.Json.Serialization;

namespace Schedule2PDF_Console.Models;

public class Lesson
{
    // nth row in schedule, either first, second or maybe third lesson.
    // there may be 2 same rank for one day, because they will be different by their week type.
    public int Rank { get; set; }
    public string LessonName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WeekType WeekType { get; set; } = WeekType.Single;
}
