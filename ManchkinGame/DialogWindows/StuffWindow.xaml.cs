using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class StuffWindow : Window
{
    public StuffWindow()
    {
        InitializeComponent();
        SetInfo();
        OkButton.Click += OkButtonClick;
    }

    private void SetInfo()
    {
        var stuff = App.Current.Resources["STUFF"] as IStuff;
        StuffLabel.Text = App.Current.Resources["STUFF_TYPE"].ToString();
        NameLabel.Text = stuff.TextRepresentation;
        DamageLabel.Text = stuff.Damage.ToString();
        PriceLabel.Text = stuff.Price.ToString();
        WeightLabel.Text = stuff.Weight == Bulkiness.HUGE ? "большая" : "маленькая";
        if (stuff.Fullness == Arms.NO) FullnessLabel.Text = "без рук";
        else FullnessLabel.Text = stuff.Fullness == Arms.BOTH ? " в обе" : "в одну";
        CheatLabel.Text = stuff.Cheat ? "активен" : "неактивен";
        FlushingLabel.Text = stuff.FlushingBonus.ToString();

        //TODO: дописать парсиг относитльно классов рас и гендеров
    }

    private void OkButtonClick(object sender, RoutedEventArgs e) => Close();
}