using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ToDo_App_M324;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private TodoList _todoList;
        private string _testFilePath = "test_todo_list.csv";

        [TestInitialize]
        public void Setup()
        {
            _todoList = new TodoList();
            typeof(TodoList).GetField("_filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_todoList, _testFilePath);

            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void AddTask_ShouldAddTaskToList()
        {
            _todoList.AddTask("Testaufgabe");
            var tasks = _todoList.GetTasks(); 

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("Testaufgabe", tasks[0]);
        }

        [TestMethod]
        public void RemoveTask_ShouldRemoveTaskFromList()
        {
            _todoList.AddTask("Testaufgabe");
            _todoList.RemoveTask("1");
            var tasks = _todoList.GetTasks();

            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public void ShowTasks_ShouldReturnCorrectTaskList()
        {
            _todoList.AddTask("Task1");
            _todoList.AddTask("Task2");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _todoList.ShowTasks();
                string output = sw.ToString();

                Assert.IsTrue(output.Contains("1. Task1"));
                Assert.IsTrue(output.Contains("2. Task2"));
            }
        }

        [TestMethod]
        public void SaveTasks_ShouldWriteToFile()
        {
            _todoList.AddTask("Task1");
            _todoList.SaveTasks();

            Assert.IsTrue(File.Exists(_testFilePath));
            string[] lines = File.ReadAllLines(_testFilePath);
            Assert.AreEqual(1, lines.Length);
            Assert.AreEqual("Task1", lines[0]);
        }

        [TestMethod]
        public void LoadTasks_ShouldReadFromFile()
        {
            File.WriteAllLines(_testFilePath, new[] { "Task1", "Task2" });

            _todoList.LoadTasks();
            var tasks = _todoList.GetTasks();

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual("Task1", tasks[0]);
            Assert.AreEqual("Task2", tasks[1]);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }
    }
}
