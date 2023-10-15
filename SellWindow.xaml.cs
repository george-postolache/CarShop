using CarShop.Contexts;
using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        public List<Car> CarsList { get; set; }
        public List<Client> ClientsList { get; set; }
        public SellWindow()
        {
            InitializeComponent();

            using (CarShopContext context = new CarShopContext())
            {
                grdCars.ItemsSource = CarsList = context.Cars.Where(c => c.ClientId.HasValue == false).ToList();
                grdClients.ItemsSource = ClientsList = context.Clients.ToList();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            if (grdClients.SelectedItem == null)
            {
                MessageBox.Show(Messages.ClientIsNotSelected, Messages.WarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(grdCars.SelectedItem == null)
            {
                MessageBox.Show(Messages.CarIsNotSelected, Messages.WarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    using (CarShopContext context = new CarShopContext())
                    {
                        var car = grdCars.SelectedItem as Car;
                        var client = grdClients.SelectedItem as Client;
                        car.ClientId = client.ClientId;
                        car.Client = client;
                        //if (client.Cars == null)
                        //{
                        //    client.Cars = new List<Car>() { car };
                        //}
                        //else
                        //{
                        //    client.Cars.Add(car);
                        //}

                        context.Entry(car).State = EntityState.Modified;
                        context.Entry(client).State = EntityState.Modified;
                        context.SaveChanges();

                        grdCars.ItemsSource = context.Cars.Where(c => c.ClientId.HasValue == false).ToList();
                        grdClients.ItemsSource = context.Clients.ToList();
                        MessageBox.Show(Messages.SaleCompleted, Messages.InformationTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
