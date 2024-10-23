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
    /// Логика взаимодействия для GuestList.xaml
    /// </summary>
    public partial class GuestList : Page
    {
        public GuestList()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            MyList.ItemsSource = Data.TradeEntities.GetContext().Product.ToList();
        }
        private void SortUp_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SortDown_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
