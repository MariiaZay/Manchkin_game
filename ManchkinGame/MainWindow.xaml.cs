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
            if(userName.Length == 0)
            {
                CreateMessageForUser("Пожалуйста введите имя", "Недостаточно данных",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (MaleButton.IsChecked == false && FemaleBottun.IsChecked == false)
                {
                    CreateMessageForUser("Пожалуйста укажите пол", "Недостаточно данных",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var sex = MaleButton.IsChecked == true ? "мужcкой" : "женский";
                    
                    App.Current.Resources["USER_NAME"] = userName;
                    App.Current.Resources["SEX"] = sex;
                    
                    var PlayWin= new PlayerWindow();
                    PlayWin.Show();
                    Close();
                }
            }
        }
    }
}