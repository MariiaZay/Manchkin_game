using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class SellWindow : Window
{
    private IManchkin _manchkin;
    private List<IStuff> _variants;
    private int _price;
    private List<IStuff> _stackPanelContent;
    public SellWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        _price = 0;
        
        _stackPanelContent = new List<IStuff>();
        
        _variants = new List<IStuff>();
        _variants.AddRange(_manchkin.SmallStuffs);
        _variants.AddRange(_manchkin.HugeStuffs);
        
        StuffComboBox.Loaded += StuffComboBoxLoaded;
        StuffComboBox.DropDownClosed += StuffComboBoxDropDownClosed;
        
        ShowStuffButton.Click += ShowStuffButtonClick;
        ToSellButton.Click += ToSellButtonClick;

        DeleteButton.Click += DeleteButtonClick;
        
        SellAllButton.Click += SellAllButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if(StuffStackPanel.Children.Count == 0)
            UserMessage.CreateCantDoItNowMessage("нет шмоток на продажу");
        else
            RemoveStuffFromSell();
    }


    private void ToSellButtonClick(object sender, RoutedEventArgs e)
    {
        if(_variants.Count == 0)
            UserMessage.CreateEndStuffForSellingMessage();
        else if(StuffComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку для продажи");
        else
        {
            var stuff = _variants.FirstOrDefault(vari => vari.TextRepresentation == StuffComboBox.Text);
            AddStuffToSell(stuff);
            
            _variants.Remove(stuff);
            RefreshComboBox();
            RefreshButtons();
        }
    }

    private void SellAllButtonClick(object sender, RoutedEventArgs e)
    {
        if (_manchkin.DoublePrice && UserMessage.CreateSellByDoublePriceMessage())
        {
            App.Current.Resources["MANCHKIN"] = _manchkin;
            DialogWindow.Show(new DoblePriceSell(), this);
            _price += (int)App.Current.Resources["PRICE"];
        }

        var stuffs = _manchkin.SmallStuffs;
        stuffs.AddRange(_manchkin.HugeStuffs);
        _price += _manchkin.SellStuffs(stuffs);
        _manchkin.GetLevel(_price / 1000);
        Close();
    }

    private void ShowStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if(StuffComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку для просмотра");
        else
        {
            var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == StuffComboBox.Text);
            ShowStuff(variant);
        }
    }

    private void StuffComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants)
            StuffComboBox.Items.Add(variant.TextRepresentation);
    }
    
    private void StuffComboBoxDropDownClosed(object? sender, EventArgs e)
        => RefreshButtons();
    
    
    private void RefreshComboBox()
    {
        StuffComboBox.Items.Clear();
        foreach (var variant in _variants)
            StuffComboBox.Items.Add(variant.TextRepresentation);
        RefreshButtons();
    }

    private void RefreshStackPanel()
    {
        StuffStackPanel.Children.Clear();
        foreach (var content in _stackPanelContent)
            AddStuffToSell(content);
        RefreshButtons();
    }

    private void AddStuffToSell(IStuff stuff)
    {
        var grid = new Grid();
        _stackPanelContent.Add(stuff);
        grid.Margin = StuffStackPanel.Children.Count == 0
            ? new Thickness(0, 0, 0, 4.4)
            : new Thickness(0, 4.4, 0, 4.4);
        
        var colDef0 =new ColumnDefinition();
        colDef0.Width = new GridLength(0.05, GridUnitType.Star);
        var colDef1 =new ColumnDefinition();
        
        grid.ColumnDefinitions.Add(colDef0);
        grid.ColumnDefinitions.Add(colDef1);
        
        var ellipce = new Ellipse
        {
            Style = (Style) FindResource("EllipseStyle")
        };
        Grid.SetColumn(ellipce,0);
        
        var text = new TextBlock
        {
            Style = (Style) FindResource("TextSellStyle"),
            Text = stuff.TextRepresentation
        };
        Grid.SetColumn(text,1);
        grid.Children.Add(ellipce);
        grid.Children.Add(text);
        StuffStackPanel.Children.Add(grid);
        
        StuffScrollView.Content = StuffStackPanel;
        DeleteButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        SellButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
    }

    private void RemoveStuffFromSell()
    {
        App.Current.Resources["STUFFS"] = _stackPanelContent;
        
        DialogWindow.Show(new ReturnStuffWindow(), this);
        var choosen = App.Current.Resources["CHOOSEN"] as IStuff;
        
        if (choosen == null) return;
        
        _stackPanelContent.Remove(choosen);
        _variants.Add(choosen);
        
        RefreshComboBox();
        RefreshStackPanel();
    }

    private void ShowStuff(IStuff stuff)
    {
        if(stuff == null)
            UserMessage.CreateEmptyStuffMessage();
        else
        {
            Application.Current.Resources["STUFF"] = stuff;
            Application.Current.Resources["STUFF_TYPE"] = stuff switch
            {
                Armor => "броник",
                Shoes => "обувка",
                Weapon => "оружие",
                Hat => "головняк",
                _ => "просто шмотка"
            };
            DialogWindow.Show(new StuffWindow(), this);
        }
    }
    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();

    private void RefreshButtons()
    {
        ToSellButton.Style = ShowStuffButton.Style = 
            _variants.Count == 0 || StuffComboBox.Text == ""
            ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
            : (Style) FindResource("RoundedGreenButtonStyle");
        
        SellDoublePriceButton.Style = _manchkin.Race is Halfling && _manchkin.DoublePrice
            ? (Style) FindResource("RoundedGreenButtonStyle")
            : (Style) FindResource("RoundedNotActiveGreenButtonStyle");
        
        SellButton.Style = StuffStackPanel.Children.Count == 0
            ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
            : (Style) FindResource("RoundedGreenButtonStyle");
        
        DeleteButton.Style = StuffStackPanel.Children.Count == 0
            ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
            : (Style) FindResource("RoundedRedButtonStyle");

    }
}