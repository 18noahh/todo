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
        var newTask = new Task("Testaufgabe", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(1, tasks.Count);
        Assert.AreEqual("Testaufgabe", tasks[0].Name);
    }

    [TestMethod]
    public void RemoveTask_ShouldRemoveTaskFromList()
    {
        var newTask = new Task("Testaufgabe", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        _todoList.RemoveTask(newTask);
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(0, tasks.Count);
    }


    [TestMethod]
    public void SaveTasks_ShouldWriteToFile()
    {
        var newTask = new Task("Testaufgabe", false, Priority.Mittel);
        _todoList.AddTask(newTask);
        _todoList.SaveTasks();

        Assert.IsTrue(File.Exists(_testFilePath));
        string[] lines = File.ReadAllLines(_testFilePath);
        Assert.AreEqual(2, lines.Length);
        Assert.AreEqual(true, lines[1].Contains("Testaufgabe"));
    }

    [TestMethod]
    public void LoadTasks_ShouldReadFromFile()
    {
        using (var stream = new StreamWriter(_testFilePath))
        using (var writer = new CsvWriter(stream, CultureInfo.InvariantCulture))
        {
            var newTask1 = new Task("Testaufgabe 1", false, Priority.Mittel);
            var newTask2 = new Task("Testaufgabe 2", false, Priority.Mittel);

            writer.WriteRecords([newTask1, newTask2]);
        }

        _todoList.LoadTasks();
        var tasks = _todoList.GetTasks();

        Assert.AreEqual(2, tasks.Count);
        Assert.AreEqual("Testaufgabe 1", tasks[0].Name);
        Assert.AreEqual("Testaufgabe 2", tasks[1].Name);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }
}
