using CarShop.Contexts;
using CarShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for CarsWindow.xaml
    /// </summary>
    public partial class CarsWindow : Window
    {
        public List<Car> CarsList { get; set; }
        private CarShopContext context = new CarShopContext();
        public CarsWindow()
        {
            InitializeComponent();
         
            grdCars.ItemsSource = CarsList = context.Cars.ToList();
        }

        private void grdCars_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int? carId = (grdCars.SelectedItem as Car)?.CarId;
            if (carId != null)
            {
                CarShopContext context = new CarShopContext();
                var car = context.Cars.Where(a => a.CarId == carId).SingleOrDefault();
                context.Cars.Remove(car);
                context.SaveChanges();

                grdCars.ItemsSource = context.Cars.ToList();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        private void grdCars_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            Car newCar = grdCars.SelectedItem as Car;
            if (newCar != null)
            {
                Car car = context.Cars.Find(newCar.CarId);
                
                if (MessageBox.Show(Messages.UpdateMessage, Messages.UpdateTitle, System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    context.Entry(car).State = EntityState.Modified;
                    context.SaveChanges();

                }
                grdCars.ItemsSource = context.Cars.ToList();
            }
        }

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new AddCar().ShowDialog();
            ShowDialog();
        }

        private void _window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CarShopContext context = new CarShopContext();
            grdCars.ItemsSource = CarsList = context.Cars.ToList();
            grdCars.Items.Refresh();
        }
    }
}
