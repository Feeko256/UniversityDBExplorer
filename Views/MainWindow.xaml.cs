using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ModernWpf.Controls;
using SourceChord.FluentWPF;
using UniversityDBExplorer.ViewModels;

namespace UniversityDBExplorer.Views
{
    public partial class MainWindow : AcrylicWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}