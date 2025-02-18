# Schedule2PDF

## Overview

Schedule2PDF is a simple tool that helps generate a dynamic, easy-to-read schedule from a JSON input. It allows you and your group mates to quickly view your schedule in a browser, making it easy to screenshot and save for reference.

This project was created as a personal tool to manage and visualize timetables, specifically for my group mates and me. It is **not intended for large-scale use by big companies or other users outside of our group**.

## How It Works

1. **Input Data:** Provide a `schedule.json` file in a specified format. This file contains the lessons, teacher names, room numbers, and week types for each day of the week.
2. **Schedule Generation:** The program reads the JSON file, processes the data, and generates an HTML page that organizes the lessons by day and rank.
3. **Output:** The final HTML schedule is automatically opened in your default browser for viewing and saving.

## JSON Format

```json
{
  "days": [
    {
      "day_of_week": "Monday",
      "lessons": [
        {
          "rank": 1,
          "lesson_name": "RME",
          "teacher_name": "Ramaldanova Nuriye",
          "room_number": "429-1",
          "week_type": "Single"
        },
        {
          "rank": 2,
          "lesson_name": "Emel. sist. qur. pr. (M)",
          "teacher_name": "S.Izmir",
          "room_number": "506-6",
          "week_type": "Top"
        },
        {
          "rank": 2,
          "lesson_name": "NoSQL vbis (m)",
          "teacher_name": "Kerimov K.",
          "room_number": "506-6",
          "week_type": "Bottom"
        }
      ]
    },
    {
      "day_of_week": "Tuesday",
      "lessons": [
        {
          "rank": 1,
          "lesson_name": "",
          "teacher_name": "",
          "room_number": "",
          "week_type": ""
        }
      ]
    },
    {
      "day_of_week": "Wednesday",
      "lessons": [
        {
          "rank": 1,
          "lesson_name": "",
          "teacher_name": "",
          "room_number": "",
          "week_type": ""
        }
      ]
    },
    {
      "day_of_week": "Thursday",
      "lessons": [
        {
          "rank": 1,
          "lesson_name": "",
          "teacher_name": "",
          "room_number": "",
          "week_type": ""
        }
      ]
    },
    {
      "day_of_week": "Friday",
      "lessons": [
        {
          "rank": 1,
          "lesson_name": "",
          "teacher_name": "",
          "room_number": "",
          "week_type": ""
        }
      ]
    }
  ]
}
```

## Important Notes

- Only the weekdays Monday to Friday are included. Saturday and Sunday are excluded.
- The `week_type` field defines the lesson’s classification: Single, Top, or Bottom.
- Ensure that the JSON file structure is valid for the tool to work correctly.

## Example Output

The generated HTML file will present a table with each day of the week, listing the lessons scheduled at each rank. Each lesson will display the following information:

- Lesson Name
- Teacher Name
- Room Number

### from
![image](https://github.com/user-attachments/assets/e112e8c9-4954-4970-b3e5-fe2b8deea045)

### to
![image](https://github.com/user-attachments/assets/7ab702cf-4ac9-45bc-96d4-6b99729047da)


Lessons will be color-coded based on their `week_type` (Single, Top, or Bottom) for easier recognition.

## Customization

Feel free to modify the HTML template (`template.html`) to adjust the look and feel of the generated schedule. You can change colors, fonts, or layout as needed.

## Limitations

- This tool is specifically built for managing and displaying schedules for my group.
- It may not be suitable for use by larger organizations or in production environments.
- The output is designed for quick screenshots and is not optimized for printing or sharing as a professional document.
