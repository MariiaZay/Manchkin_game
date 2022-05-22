using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ManchkinGame.Windows;

namespace ManchkinGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var button = StartButton;
            button.Click += StartButtonClick;
        }

        private void CreateMessageForUser(string mess, string caption,
            MessageBoxButton button, MessageBoxImage icon)
            => MessageBox.Show(mess, caption, button, icon, MessageBoxResult.Yes);

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            var userName = NameBox.Text;
            switch (userName.Length)
            {
                case 0:
                    UserMessage.CreateNotChosenItemMessage("имя");
                    break;
                case > 59:
                    CreateMessageForUser(String.Format("Ваше имя слишком длинное!\n" +
                                                       "Пожалуйста, введите имя на {0} символа короче",
                            userName.Length - 59), "Некорректный ввод",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:
                {
                    if (MaleButton.IsChecked == false && FemaleBottun.IsChecked == false)
                    {
                        UserMessage.CreateNotChosenItemMessage("пол");
                    }
                    else
                    {
                        var sex = MaleButton.IsChecked == true ? "мужcкой" : "женский";

                        App.Current.Resources["USER_NAME"] = userName;
                        App.Current.Resources["SEX"] = sex;

                        var PlayWin = new PlayerWindow();
                        PlayWin.Show();
                        Close();
                    }
                    break;
                }
            }
        }
    }
}