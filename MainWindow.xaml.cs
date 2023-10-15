using CarShop.Contexts;
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

namespace CarShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCarsWindow(object sender, RoutedEventArgs e)
        {
            Hide();
            new CarsWindow().ShowDialog();
            ShowDialog();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new ClientsWindow().ShowDialog();
            ShowDialog();
        }

        private void btnSellCar_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new SellWindow().ShowDialog();
            ShowDialog();
        }
    }
}
