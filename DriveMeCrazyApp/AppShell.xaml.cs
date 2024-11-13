﻿using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
