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

namespace PetShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            MyList.ItemsSource = Data.TradeEntities.GetContext().Product.ToList();
            var manufacturer = Data.TradeEntities.GetContext().Proizvoditel.ToList();
            manufacturer.Insert(0, new Data.Proizvoditel { Name = "Все производители" });
            Manufacturer.ItemsSource = manufacturer;
            Manufacturer.SelectedItem = 0;
            if (Classes.Manager.CurrentUser == null)
            {
                FIO.Visibility = Visibility.Hidden;
            }
            else
            {
                FIO.Content = $"{Classes.Manager.CurrentUser.UserName}" + $"{Classes.Manager.CurrentUser.UserSurname}" + $"{Classes.Manager.CurrentUser.UserPatronymic}";
            }
            CountLabel.Content = $"{Data.TradeEntities.GetContext().Product.Count()}" + "/" + $"{Data.TradeEntities.GetContext().Product.Count()}";
            SortDown.IsChecked = false;
            SortUp.IsChecked = false;

        }
        public List<Data.Product> _currentProduct = Data.TradeEntities.GetContext().Product.ToList();
        public void Update()
        {
            _currentProduct = Data.TradeEntities.GetContext().Product.ToList();
            _currentProduct = (from item in Data.TradeEntities.GetContext().Product.ToList()
                               where item.ProductName.Name.ToLower().Contains(SearchBox.Text) ||
                               item.Description.ToLower().Contains(SearchBox.Text) ||
                               item.Count.ToLower().ToString().Contains(SearchBox.Text) ||
                               item.Price.ToString().Contains(SearchBox.Text) ||
                               item.Proizvoditel.Name.ToLower().ToString().Contains(SearchBox.Text)
                               select item).ToList();
            if (SortUp.IsChecked == true)
            {
                _currentProduct = _currentProduct.OrderBy(d => d.Price).ToList();
            }
            if (SortDown.IsChecked == true)
            {
                _currentProduct = _currentProduct.OrderByDescending(d => d.Price).ToList();
            }
            var select = Manufacturer.SelectedItem as Data.Proizvoditel;

            if (Manufacturer.SelectedItem != "Все производители" && select != null)
            {
                _currentProduct = _currentProduct.Where(d => d.Proizvoditel.Id == select.Id).ToList();
            }
            CountLabel.Content = $"{_currentProduct.Count()}" + "/" + $"{Data.TradeEntities.GetContext().Product.Count()}";
            MyList.ItemsSource = _currentProduct;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Manager.MainFrame.CanGoBack)
            {
                Classes.Manager.MainFrame.GoBack();
            }
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            if (Classes.Manager.MainFrame.CanGoBack)
            {
                Classes.Manager.MainFrame.GoBack();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Data.Product;
            var fordelet = Data.TradeEntities.GetContext().Order.Where(d=>d.IdProduct == select.Id).ToList();
            if (fordelet.Count < 0)
            {
                Data.TradeEntities.GetContext().Product.Remove(select);
                Data.TradeEntities.GetContext().SaveChanges();

            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AddEdit());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AddEdit());
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Manufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void SortUp_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void SortDown_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
