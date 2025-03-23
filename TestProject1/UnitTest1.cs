using CsvHelper;
using System.Globalization;
using ToDo_App_M324;
using Task = ToDo_App_M324.Task;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    private TodoList _todoList = null!;
    private string _testFilePath = "test_todo_list.csv";

    [TestInitialize]
    public void Setup()
    {
        _todoList = new TodoList(_testFilePath);
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [TestMethod]
    public void AddTask_ShouldAddTaskToList()
    {
        var newTask = new Task(1, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(1, tasks.Count);
        Assert.AreEqual(newTask.Id, tasks[0].Id);
        Assert.AreEqual(newTask.Name, tasks[0].Name);
        Assert.AreEqual(newTask.Description, tasks[0].Description);
        Assert.AreEqual(newTask.Priority, tasks[0].Priority);
        Assert.AreEqual(newTask.IsDone, tasks[0].IsDone);
    }

    [TestMethod]
    public void AddTask_ShouldNotAddInvalidTask()
    {
        var invalidTask = new Task(2, "", "Beschreibung", false, Priority.Hoch);
        _todoList.AddTask(invalidTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(0, tasks.Count); // Task sollte nicht hinzugefügt werden
    }

    [TestMethod]
    public void RemoveTask_ShouldRemoveTaskFromList()
    {
        var task = new Task(3, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(task);
        _todoList.RemoveTask(task);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(0, tasks.Count);
    }

    [TestMethod]
    public void RemoveTask_ShouldNotFailOnNonExistentTask()
    {
        var task = new Task(4, "Nicht existierende Aufgabe", "Beschreibung", false, Priority.Niedrig);
        _todoList.RemoveTask(task); // Sollte keinen Fehler werfen
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(0, tasks.Count);
    }

    [TestMethod]
    public void SaveTasks_ShouldWriteToFile()
    {
        var newTask = new Task(5, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        _todoList.SaveTasks();

        Assert.IsTrue(File.Exists(_testFilePath));
        string[] lines = File.ReadAllLines(_testFilePath);

        Assert.AreEqual(2, lines.Length); // Header + 1 Task
        Assert.IsTrue(lines[1].Contains("Testaufgabe"));
        Assert.IsTrue(lines[1].Contains("Beschreibung"));
    }

    [TestMethod]
    public void LoadTasks_ShouldReadFromFile()
    {
        using (var stream = new StreamWriter(_testFilePath))
        using (var writer = new CsvWriter(stream, CultureInfo.InvariantCulture))
        {
            writer.WriteRecords(new[]
            {
                new Task(6, "Testaufgabe 1", "Beschreibung 1", false, Priority.Mittel),
                new Task(7, "Testaufgabe 2", "Beschreibung 2", true, Priority.Hoch)
            });
        }

        _todoList.LoadTasks();
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(2, tasks.Count);

        // Erste Aufgabe überprüfen
        Assert.AreEqual(6, tasks[0].Id);
        Assert.AreEqual("Testaufgabe 1", tasks[0].Name);
        Assert.AreEqual("Beschreibung 1", tasks[0].Description);
        Assert.IsFalse(tasks[0].IsDone);
        Assert.AreEqual(Priority.Mittel, tasks[0].Priority);

        // Zweite Aufgabe überprüfen
        Assert.AreEqual(7, tasks[1].Id);
        Assert.AreEqual("Testaufgabe 2", tasks[1].Name);
        Assert.AreEqual("Beschreibung 2", tasks[1].Description);
        Assert.IsTrue(tasks[1].IsDone);
        Assert.AreEqual(Priority.Hoch, tasks[1].Priority);
    }

    [TestMethod]
    public void AddTask_ShouldIgnoreDuplicateTasks()
    {
        var task = new Task(8, "Duplikat", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(task);
        _todoList.AddTask(task); // Zweites Mal hinzufügen

        var tasks = _todoList.GetTasks();

        Assert.AreEqual(2, tasks.Count);
        Assert.AreEqual("Duplikat", tasks[0].Name);
        Assert.AreEqual("Duplikat", tasks[1].Name);
    }

    [TestMethod]
    public void SaveTasks_ShouldHandleEmptyList()
    {
        _todoList.SaveTasks();
        string[] lines = File.ReadAllLines(_testFilePath);

        Assert.AreEqual(1, lines.Length); // Nur der Header sollte vorhanden sein
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }
}
