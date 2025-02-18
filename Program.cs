using Schedule2PDF_Console.Models;
using Schedule2PDF_Console.Services;
using System.Diagnostics;
using System.Text.Json;

namespace Schedule2PDF_Console;
internal class Program
{
    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        AllowTrailingCommas = true,
    };

    private static void Main(string[] args)
    {
        string jsonFilePath = string.Empty;
        while (!File.Exists(jsonFilePath))
        {
            Console.Write("Enter the path to your schedule.json or 'quit' to exit: ");
            jsonFilePath = Console.ReadLine() ?? string.Empty;

            if (jsonFilePath != null && jsonFilePath.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting program.");
                return;
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("File not found. Please try again.");
            }
        }

        string json = File.ReadAllText(jsonFilePath);
        Schedule? schedule = JsonSerializer.Deserialize<Schedule>(json, _serializerOptions);

        if (schedule != null)
        {
            string dir = Directory.GetCurrentDirectory();
            string backward = string.Join('\\', Enumerable.Repeat("..", 3));
            string htmlTemplate = File.ReadAllText(Path.Combine(dir, backward, "Templates", "template.html"));

            IScheduleService scheduleService = new ScheduleService();
            string tableRows = scheduleService.GenerateTableRows(schedule);
            string finalHtml = htmlTemplate.Replace("{0}", tableRows);

            string filePath = Path.Combine(Path.GetTempPath(), "schedule.html");
            File.WriteAllText(filePath, finalHtml);
            if (!OpenInBrowser(filePath))
            {
                Console.WriteLine("An error occurred while opening file in browser.");
            }
        }
        else
        {
            Console.WriteLine("Deserialization failed.");
            return;
        }
    }

    private static bool OpenInBrowser(string filePath)
    {
        var processStartInfo = new ProcessStartInfo(filePath)
        {
            UseShellExecute = true,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = processStartInfo };
        return process.Start();
    }
}