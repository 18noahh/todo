namespace ToDo_App_M324;

public enum Priority { Niedrig, Mittel, Hoch }
public class Task
{
    public string Name { get; set; }
    public bool IsDone { get; set; }
    public Priority Priority { get; set; }

    public Task() : this("", false, Priority.Mittel) { }
    public Task(string name, bool isDone, Priority priority)
    {
        Name = name;
        IsDone = isDone;
        Priority = priority;
    }

}
