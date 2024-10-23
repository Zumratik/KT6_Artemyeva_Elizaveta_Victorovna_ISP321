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
using System.Windows.Threading;

namespace PetShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public static int failCount = 0;
        public DispatcherTimer timer;
        public Auth()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
           timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer;
        }
        public void Timer(object sender, EventArgs e)
        {
            timer.Stop();
            LogButton.IsEnabled = true;
            MessageBox.Show("Попробуйте ввести данные повторно","Информация",MessageBoxButton.OK,MessageBoxImage.Information) ;
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder erors = new StringBuilder();
                if (string.IsNullOrEmpty(LoginTextBox.Text))
                {
                    erors.AppendLine("Введите логин");
                }
                if (string.IsNullOrEmpty(PasswordTextBox.Password))
                {
                    erors.AppendLine("Введите пароль");
                }
               
                if (erors.Length>0)
                {
                    failCount++;
                    MessageBox.Show(erors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (failCount > 0)
                {
                    CapchaInput.Visibility = Visibility.Visible;
                    CapchaText.Visibility = Visibility.Visible;
                    CapchaLabel.Visibility = Visibility.Visible;

                    if (string.IsNullOrEmpty(CapchaInput.Text))
                    {
                        erors.AppendLine("Введите капчу");
                        CapchaBrain();
                    }
                    if (!CapchaInput.Text.Equals(CapchaText.Text , StringComparison.Ordinal))
                    {

                        MessageBox.Show("Данные не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                        CapchaInput.Text = "";
                        LogButton.IsEnabled = false;
                        CapchaBrain();
                        timer.Start();
                        return;
                    }
                   
                }
                if (Data.TradeEntities.GetContext().User.Any(d => d.UserPassword == PasswordTextBox.Password && d.UserLogin == LoginTextBox.Text))
                {
                    var user = Data.TradeEntities.GetContext().User.Where(d => d.UserPassword == PasswordTextBox.Password && d.UserLogin == LoginTextBox.Text).FirstOrDefault();
                    Classes.Manager.CurrentUser = user;


                    switch (user.UserRole1.Name)
                    {
                        case "Администратор":
                            Classes.Manager.MainFrame.Navigate(new Pages.AdminPage());
                            break;
                        case "Менеджер":
                            Classes.Manager.MainFrame.Navigate(new Pages.GuestList());
                            break;
                        case "Клиент":
                            Classes.Manager.MainFrame.Navigate(new Pages.GuestList());
                            break;
                    }

                    CapchaInput.Visibility = Visibility.Hidden;
                    CapchaText.Visibility = Visibility.Hidden;
                    CapchaLabel.Visibility = Visibility.Hidden;
                    CapchaText.Text = "";
                    failCount = 0;
                    CapchaBrain();
                    MessageBox.Show("Успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Учетная запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    failCount++;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public void CapchaBrain()
        {
            string capcha = "";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            Random rand = new Random();
            for (int i=0; i<5; i++)
            {
                capcha += chars[rand.Next(chars.Length)];
            }
            CapchaText.Text = capcha;
        }
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.GuestList());
        }
    }
}
