using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using Microsoft.Maui.HotReload;
using TodoList.ViewModel;

namespace TodoList;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ViewModel.ViewModel();
    }
    
}
