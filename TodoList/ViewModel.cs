using System.ComponentModel;
using System.Windows.Input;
using TodoList.TaskItem;
using Microsoft.Maui.Graphics.Text;
using System.Collections.ObjectModel;


namespace TodoList.ViewModel;

public class ViewModel : INotifyPropertyChanged
{
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    
    public ICommand ChangeColorCommand { get; set; }
    
    public ICommand ChooseColorCommand { get; set; }
    
    public ObservableCollection<TaskItem.TaskItem> ListTaskItems { get; set; } =
        new ();

    public TaskItem.TaskItem task;
    public Color colorok;

    public TaskItem.TaskItem SelectTaskItem
    {
        get => task;
        set
        {
            if (task != value)
            {
                task = value;
                if (task != null)
                    EditTaskItem();
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectTaskItem)));
        }
    }

    private async void EditTaskItem()
    {
        if (Application.Current == null || Application.Current.MainPage == null) return;
        var text = await Application.Current.MainPage.DisplayPromptAsync(title: "Edit task",
            message: "Select Task", 
            initialValue: SelectTaskItem.ItemName);
        SelectTaskItem.ItemName = text;
        SelectTaskItem = null;
    }

    private async void ChooseColor1(TaskItem.TaskItem task)
    {
        if (Application.Current == null) return;
        if (Application.Current.MainPage == null) return;
        var text = await Application.Current.MainPage.DisplayActionSheet("Choose color", "Выйти", null,
            "Red", "Yellow", "Blue");
        task.Coloring = text switch
        {
            "Red" => Colors.Red,
            "Yellow" => Colors.Yellow,
            "Blue" => Colors.Blue,
            _ => task.Coloring
        };
    }

    public ViewModel()
    {
        task = new TaskItem.TaskItem();
        AddTaskCommand = new Command<string>(AddTask);
        DeleteTaskCommand = new Command<TaskItem.TaskItem>(Delete);
        ChangeColorCommand = new Command<string>(ChangeColor);
        ChooseColorCommand = new Command<TaskItem.TaskItem>(ChooseColor1);
        Colorok = Colors.Gray;
    }
    

    public Color Colorok
    {
        get => colorok;
        set
        { 
            if (Equals(value, colorok))
                return;
            colorok = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Colorok)));
        }
    }
        
    private void ChangeColor(string text)
    {
        Colorok = text switch
        {
            "Red" => Colors.Red,
            "Blue" => Colors.Blue,
            "Yellow" => Colors.Yellow,
            _ => Colorok
        };
    }

    private void AddTask(string entry)
    {
        ListTaskItems.Add(new TaskItem.TaskItem()
            {
                ItemName = entry,
                ItemCompleted = false,
                Coloring = colorok
            });
        Colorok = Colors.Gray;
    }

    private void Delete(TaskItem.TaskItem task)
    {
        ListTaskItems.Remove(task);
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
}
