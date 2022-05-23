using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class WearingWindow : Window
{
    private string _typeOfVariants;
    private List<IDescriptable> _variants;
    private string _current;
    private IManchkin _manchkin;

    public WearingWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _current = App.Current.Resources["CURRENT"].ToString();
        _manchkin = App.Current.Resources["MANCHKIN"] as Manchkin;

        _variants = _typeOfVariants switch
        {
            "броник" => _variants = CardsBase.Armors,
            "обувка" => _variants = CardsBase.Shoeses,
            "головняк" => _variants = CardsBase.Hats,
            "мелкие шмотки" => _variants = CardsBase.SmallStuffs
        };
        //TODO: сделать для оружия отдельную формочку
        VariantsComboBox.Loaded += VariantsComboBoxLoaded;
        WearingButton.Click += WearingButtonClick;
        CancelButton.Click += CancelButtonClick;
        CheatButton.Click += CheatButtonClick;
    }

    private void CheatButtonClick(object sender, RoutedEventArgs e)
    {
        CheatButton.Content = ReferenceEquals(CheatButton.Content, "НЕ ЧИТ!") ? "ЧИТ" : "НЕ ЧИТ!";

        if ((string) CheatButton.Content == "НЕ ЧИТ!")
            CheatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        else
            CheatButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
    }

    private void WearingButtonClick(object sender, RoutedEventArgs e)
    {
        if(VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("новую шмотку");
        else
        {
            var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == VariantsComboBox.Text);

            if (ReferenceEquals(CheatButton.Content, "НЕ ЧИТ!"))
                _manchkin.UseCheat(variant as IStuff);
            
            var v = variant as IStuff;
            if (!_manchkin.CanTakeStuff(v))
                UserMessage.CreateImpossibleTakingStuffMessage();
            else
            {
                App.Current.Resources["NEW"] = variant;
                Close();
            }
        }
    }

    private void VariantsComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants.Where(variant => _current != variant.TextRepresentation))
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["NEW"] = null;
        Close();
    }
}