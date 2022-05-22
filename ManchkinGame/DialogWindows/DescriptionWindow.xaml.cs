using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ManchkinGame;

public partial class DescriptionWindow : Window
{
    private List<string> _descriptions;
    public DescriptionWindow(List<string> desc)
    {
        InitializeComponent();
        _descriptions = desc;
        
        DescriptionScrollView.Loaded += DescriptionScrollViewLoaded;
        OkButton.Click += OkButtonClick;
    }

    private void DescriptionScrollViewLoaded(object sender, RoutedEventArgs e)
    {
        var sp = new StackPanel();
        foreach (var description in _descriptions)
        {
            var textBlock = new TextBlock();
            textBlock.Text = description;
            sp.Children.Add(textBlock);
        }

        DescriptionScrollView.Content = sp;
    }

    private void OkButtonClick(object sender, RoutedEventArgs e)
        => Close();
}