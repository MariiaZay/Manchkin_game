using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ManchkinGame;

public partial class DescriptionWindow : Window
{
    private List<string> _descriptions;
    public DescriptionWindow(List<string> desc)
    {
        InitializeComponent();
        _descriptions = desc;
        Title.Text = App.Current.Resources["TITLE"].ToString();
        DescriptionScrollView.Loaded += DescriptionScrollViewLoaded;
        OkButton.Click += OkButtonClick;
    }

    private void DescriptionScrollViewLoaded(object sender, RoutedEventArgs e)
    {
        var desc = _descriptions.ToArray();
        for (var i = 0; i < desc.Length; i++)
        {
            var grid = new Grid();
            
            if (i == 0)
                grid.Margin = new Thickness(0, 0, 0, 4.4);
            else
            {
                grid.Margin = i == desc.Length - 1 ? 
                    new Thickness(0, 4.4, 0, 0) 
                    : new Thickness(0, 4.4, 0, 4.4);
            }
            var colDef0 =new ColumnDefinition();
            colDef0.Width = new GridLength(0.05, GridUnitType.Star);
            var colDef1 =new ColumnDefinition();
            grid.ColumnDefinitions.Add(colDef0);
            grid.ColumnDefinitions.Add(colDef1);
            
            var ellipce = new Ellipse();
            ellipce.Style = (Style) FindResource("EllipseStyle");
            Grid.SetColumn(ellipce,0);

            var text = new TextBlock();
            text.Style = (Style) FindResource("TextStyle");
            text.Text = desc[i];
            Grid.SetColumn(text,1);

            grid.Children.Add(ellipce);
            grid.Children.Add(text);
            DescriptionStackPanel.Children.Add(grid);
        }
        DescriptionScrollView.Content = DescriptionStackPanel;
    }

    private void OkButtonClick(object sender, RoutedEventArgs e)
        => Close();
}