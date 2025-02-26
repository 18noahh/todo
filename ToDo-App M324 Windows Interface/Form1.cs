using ToDo_App_M324;
using Task = ToDo_App_M324.Task;

namespace TODO_App;

public partial class Form1 : Form
{
    private const string filePath = "todo_list.csv";


    private static readonly TodoList tasks = new TodoList(filePath);

    public Form1()
    {
        InitializeComponent();

        cmbPriority.Items.AddRange(Enum.GetNames<Priority>());
        cmbPriority.SelectedIndex = 0;

        lblStatus.Text = "";

        tasks.LoadTasks();
        UpdateTaskList();
    }


    private void UpdateTaskList()
    {
        lstTasks.Items.Clear();
        foreach (var task in tasks.GetTasks())
        {
            string status = task.IsDone ? "[Erledigt]" : "[Offen]";
            lstTasks.Items.Add($"{task.Name} {status} (Priorität: {task.Priority})");
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string taskName = txtTask.Text;
        if (!string.IsNullOrWhiteSpace(taskName))
        {
            var priority = Enum.Parse<Priority>(cmbPriority.SelectedItem!.ToString()!);
            tasks.AddTask(new Task(taskName, false, priority));
            tasks.SaveTasks();
            UpdateTaskList();
            txtTask.Clear();
            lblStatus.Text = "Aufgabe hinzugefügt!";
        }
        else
        {
            lblStatus.Text = "Bitte eine gültige Aufgabe eingeben!";
        }
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        var index = lstTasks.SelectedIndex;
        if (index >= 0)
        {
            var taskToRemove = tasks.GetTasks()[index];
            tasks.RemoveTask(taskToRemove);
            tasks.SaveTasks();
            UpdateTaskList();
            lblStatus.Text = "Aufgabe entfernt!";
        }
        else
        {
            lblStatus.Text = "Bitte eine Aufgabe auswählen!";
        }
    }

    private void btnMarkDone_Click(object sender, EventArgs e)
    {
        if (lstTasks.SelectedIndex >= 0)
        {
            tasks.GetTasks()[lstTasks.SelectedIndex].IsDone = true;
            tasks.SaveTasks();
            UpdateTaskList();
            lblStatus.Text = "Aufgabe als erledigt markiert!";
        }
        else
        {
            lblStatus.Text = "Bitte eine Aufgabe auswählen!";
        }
    }

}

