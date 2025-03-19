namespace ToDo_App_M324;

public enum Priority { Niedrig, Mittel, Hoch }

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; } 
    public bool IsDone { get; set; }
    public Priority Priority { get; set; }

    public Task() : this(0, "", "", false, Priority.Mittel) { }

    public Task(int id, string name, string? description, bool isDone, Priority priority)
    {
        Id = id;
        Name = name;
        Description = description;
        IsDone = isDone;
        Priority = priority;
    }
}