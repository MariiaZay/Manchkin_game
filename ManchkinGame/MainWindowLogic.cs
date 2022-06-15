using System;
using System.Windows;
using ManchkinGame.Windows;

namespace ManchkinGame;

public class MainWindowLogic
{
    private MainWindow _window;

    public MainWindowLogic(MainWindow window)
    {
        _window = window;
        
        BindEventHandlers();
    }

    private void BindEventHandlers()
    {
        _window.StartButton.Click += StartButtonClick;
    }

    private void CreateMessageForUser(string mess, string caption,
        MessageBoxButton button, MessageBoxImage icon)
    {
        MessageBox.Show(mess, caption, button, icon, MessageBoxResult.Yes);
    }

    public void StartButtonClick(object sender, RoutedEventArgs e)
    {
        var userName = _window.NameBox.Text;
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
                if (_window.MaleButton.IsChecked == false && _window.FemaleBottun.IsChecked == false)
                {
                    UserMessage.CreateNotChosenItemMessage("пол");
                }
                else
                {
                    var sex = _window.MaleButton.IsChecked == true ? "мужcкой" : "женский";

                    App.Current.Resources["USER_NAME"] = userName;
                    App.Current.Resources["SEX"] = sex;

                    var PlayWin = new PlayerWindow();
                    PlayWin.Show();
                    _window.Close();
                }
                break;
            }
        }
    }
}