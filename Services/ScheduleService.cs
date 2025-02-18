using Schedule2PDF_Console.Models;
using System.Text;

namespace Schedule2PDF_Console.Services;
public interface IScheduleService
{
    string GenerateTableRows(Schedule schedule);
    Dictionary<int, Dictionary<DayOfWeek, List<Lesson>>> GenerateRankedLessons(Schedule schedule);
    Dictionary<int, string> GenerateTimeTable(int rank);
}

public class ScheduleService : IScheduleService
{
    public string GenerateTableRows(Schedule schedule)
    {
        StringBuilder stringBuilder = new StringBuilder();
        Dictionary<int, Dictionary<DayOfWeek, List<Lesson>>> dict = GenerateRankedLessons(schedule);
        Dictionary<int, string> timeSlots = GenerateTimeTable(dict.Count);

        foreach (var rank in dict.Keys.OrderBy(k => k))
        {
            stringBuilder.AppendLine("<tr>");

            timeSlots.TryGetValue(rank, out string? value);
            stringBuilder.AppendLine($"<td>{value}</td>");

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Where(d => d != DayOfWeek.Sunday && d != DayOfWeek.Saturday))
            {
                if (dict[rank].TryGetValue(day, out var lessons) && lessons.Count > 0)
                {
                    stringBuilder.Append("<td>");

                    foreach (var lesson in lessons)
                    {
                        string cssClass = lesson.WeekType switch
                        {
                            WeekType.Single => "single-lesson",
                            WeekType.Top => "above-week",
                            WeekType.Bottom => "below-week",
                            _ => ""
                        };

                        stringBuilder.Append($@"
                                                <div class='lesson {cssClass}'>
                                                    <strong>{lesson.LessonName}</strong><br />
                                                    Teacher: {lesson.TeacherName}<br />
                                                    Room: {lesson.RoomNumber}
                                                </div>");
                    }

                    stringBuilder.AppendLine("</td>");
                }
                else
                {
                    stringBuilder.AppendLine("<td></td>");
                }
            }

            stringBuilder.AppendLine("</tr>");
        }

        return stringBuilder.ToString();
    }

    public Dictionary<int, Dictionary<DayOfWeek, List<Lesson>>> GenerateRankedLessons(Schedule schedule)
    {
        var rankedLessons = new Dictionary<int, Dictionary<DayOfWeek, List<Lesson>>>();

        int maxRank = schedule.Days
            .SelectMany(day => day.Lessons)
            .Max(lesson => lesson.Rank);

        for (int rank = 1; rank <= maxRank; rank++)
        {
            var lessonsByDay = schedule.Days
                .Select(day => new
                {
                    day.DayOfWeek,
                    Lessons = day.Lessons.Where(lesson => lesson.Rank == rank).ToList()
                }).ToDictionary(x => x.DayOfWeek, x => x.Lessons);

            rankedLessons[rank] = lessonsByDay;
        }

        return rankedLessons;
    }

    public Dictionary<int, string> GenerateTimeTable(int rank)
    {
        Dictionary<int, string> dict = [];
        var startTime = new DateTime(2003, 5, 31, 18, 30, 0);

        for (int i = 1; i <= rank; i++)
        {
            var endTime = startTime.AddMinutes(80);
            dict.Add(i, $"{startTime:HH:mm} - {endTime:HH:mm}");

            if (i < rank)
            {
                startTime = endTime.AddMinutes(10);
            }
        }

        return dict;
    }
}
