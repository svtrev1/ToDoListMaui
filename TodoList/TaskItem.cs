using System.ComponentModel;
using System.Text;
using System;

namespace TodoList.TaskItem;

public class TaskItem : INotifyPropertyChanged
{
    private string itemName;
    private bool itemCompleted;
    private Color coloring;
    public string ItemName
    {
        get => itemName;
        set { 
            itemName = value; PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(nameof(ItemName))); 
        }
    }

    public bool ItemCompleted
    {
        get => itemCompleted;
        set
        {
            itemCompleted = value; PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(ItemCompleted)));
        }
    }
    public Color Coloring
    {
        get => coloring;
        set
        {
            coloring = value; PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Coloring)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

