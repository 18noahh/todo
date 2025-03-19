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
        var newTask = new Task(0, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(1, tasks.Count);
        Assert.AreEqual("Testaufgabe", tasks[0].Name);
        Assert.AreEqual("Beschreibung", tasks[0].Description);
        Assert.AreEqual(Priority.Mittel, tasks[0].Priority);
        Assert.IsFalse(tasks[0].IsDone);
    }

    [TestMethod]
    public void RemoveTask_ShouldRemoveTaskFromList()
    {
        var newTask = new Task(0, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        _todoList.RemoveTask(newTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(0, tasks.Count);
    }

    [TestMethod]
    public void SaveTasks_ShouldWriteToFile()
    {
        var newTask = new Task(0, "Testaufgabe", "Beschreibung", false, Priority.Mittel);
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
                new Task(1, "Testaufgabe 1", "Beschreibung 1", false, Priority.Mittel),
                new Task(2, "Testaufgabe 2", "Beschreibung 2", true, Priority.Hoch)
            });
        }

        _todoList.LoadTasks();
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(2, tasks.Count);
        Assert.AreEqual("Testaufgabe 1", tasks[0].Name);
        Assert.AreEqual("Beschreibung 1", tasks[0].Description);
        Assert.IsFalse(tasks[0].IsDone);
        Assert.AreEqual(Priority.Mittel, tasks[0].Priority);

        Assert.AreEqual("Testaufgabe 2", tasks[1].Name);
        Assert.AreEqual("Beschreibung 2", tasks[1].Description);
        Assert.IsTrue(tasks[1].IsDone);
        Assert.AreEqual(Priority.Hoch, tasks[1].Priority);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }
}
