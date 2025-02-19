using System;
using ToDo_App_M324;

class Program
{
    static void Main()
    {
        TodoList todoList = new TodoList();
        todoList.LoadTasks();

        while (true)
        {
            Console.WriteLine("\nToDo-Liste: ");
            Console.WriteLine("1. Aufgabe hinzufügen");
            Console.WriteLine("2. Aufgabe entfernen");
            Console.WriteLine("3. Aufgaben anzeigen");
            Console.WriteLine("4. Aufgaben speichern");
            Console.WriteLine("5. Beenden");
            Console.Write("Auswahl: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    todoList.AddTask(Console.ReadLine());
                    break;
                case "2":
                    todoList.RemoveTask(Console.ReadLine());
                    break;
                case "3":
                    todoList.ShowTasks();
                    break;
                case "4":
                    todoList.SaveTasks();
                    Console.WriteLine("Aufgaben gespeichert!");
                    break;
                case "5":
                    todoList.SaveTasks();
                    return;
                default:
                    Console.WriteLine("Ungültige Auswahl!");
                    break;
            }
        }
    }
}