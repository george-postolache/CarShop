using CarShop.Contexts;
using CarShop.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public AddCar()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CarShopContext context = new CarShopContext();

            if (String.IsNullOrEmpty(txtMake.Text) || String.IsNullOrEmpty(txtModel.Text) || String.IsNullOrEmpty(txtVIN.Text) ||
                String.IsNullOrEmpty(txtColor.Text) || String.IsNullOrEmpty(txtYear.Text) || String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show(Messages.CompleteAllFieldsError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtVIN.Text.Length < 17)
            {
                MessageBox.Show(Messages.VINTooShortError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (context.Cars.Any(c => c.VIN == txtVIN.Text))
            {
                MessageBox.Show(Messages.VINAlreadyExistError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Car car = new Car()
                {
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    VIN = txtVIN.Text,
                    Color = txtColor.Text,
                    Year = int.Parse(txtYear.Text),
                    Price = decimal.Parse(txtPrice.Text)

                };

                try
                {
                    using (context)
                    {
                        context.Cars.Add(car);
                        context.SaveChanges();
                        this.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void txtYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
            if (!IsValidDecimalWithDot(newText))
            {
                e.Handled = true;
            }
        }

        private bool IsValidDecimalWithDot(string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && !input.StartsWith(".") &&
                (decimal.TryParse(input, out decimal _) || input == "."))
            {
                return true;
            }

            return false;
        }
    }
}
