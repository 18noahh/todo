using System;
using System.Collections.Generic;
using System.IO;

namespace ToDo_App_M324;

public class TodoList
{
    private string _filePath = "todo_list.csv";
    private List<string> _tasks = new List<string>();
    
    public void LoadTasks()
    {
        if (File.Exists(_filePath))
        {
            _tasks = new List<string>(File.ReadAllLines(_filePath));
        }
    }
    
    public List<string> GetTasks()
    {
        return new List<string>(_tasks);
    }

    public void SaveTasks()
    {
        File.WriteAllLines(_filePath, _tasks);
    }

    public void AddTask(string task)
    {
        if (!string.IsNullOrWhiteSpace(task))
        {
            _tasks.Add(task);
            Console.WriteLine("Aufgabe hinzugefügt!");
        }
    }

    public void RemoveTask( string taskNumber)
    {
        ShowTasks();
        Console.Write("Nummer der zu löschenden Aufgabe: ");
        if (int.TryParse(taskNumber, out int index) && index > 0 && index <= _tasks.Count)
        {
            _tasks.RemoveAt(index - 1);
            Console.WriteLine("Aufgabe entfernt!");
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe!");
        }
    }

    public void ShowTasks()
    {
        Console.WriteLine("\nAktuelle Aufgaben:");
        if (_tasks.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben vorhanden.");
        }
        else
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_tasks[i]}");
            }
        }
    }
}