using CsvHelper;
using System.Globalization;

namespace ToDo_App_M324;
public class TodoList(string filePath)
{
    private List<Task> _tasks = new List<Task>();

    public void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            using var stream = new StreamReader(filePath);
            using var reader = new CsvReader(stream, CultureInfo.InvariantCulture);
            _tasks = reader.GetRecords<Task>().ToList();
        }
    }

    public List<Task> GetTasks()
    {
        return _tasks;
    }

    public void SaveTasks()
    {
        using var stream = new StreamWriter(filePath);
        using var writer = new CsvWriter(stream, CultureInfo.InvariantCulture);
        writer.WriteRecords(_tasks);
    }

    public void AddTask(Task task)
    {
        if (!string.IsNullOrWhiteSpace(task.Name))
        {
            _tasks.Add(task);
            Console.WriteLine("Aufgabe hinzugefügt!");
        }
    }

    public void RemoveTask(Task task)
    {
        var index = _tasks.IndexOf(task);
        if (index != -1)
        {
            _tasks.RemoveAt(index);
        }
    }
}