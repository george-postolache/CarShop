using CarShop.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CarShopContext context = new CarShopContext();

            if (String.IsNullOrEmpty(txtFirstName.Text) || String.IsNullOrEmpty(txtLastName.Text) || String.IsNullOrEmpty(txtPIN.Text) ||
                String.IsNullOrEmpty(txtPhoneNumber.Text) || String.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show(Messages.CompleteAllFieldsError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtPIN.Text.Length < 13)
            {
                MessageBox.Show(Messages.PINTooShortError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (context.Clients.Any(c => c.PIN == txtPIN.Text))
            {
                MessageBox.Show(Messages.VINAlreadyExistError, Messages.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    string query = "INSERT INTO Clients (PIN, FirstName, Lastname, PhoneNumber, Address) " +
                   "VALUES (@PIN, @FirstName, @Lastname, @PhoneNumber, @Address) ";

                    using (SqlConnection cn = new SqlConnection(context.Database.Connection.ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("@PIN", SqlDbType.NVarChar, 13).Value = txtPIN.Text;
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = txtFirstName.Text;
                        cmd.Parameters.Add("@Lastname", SqlDbType.NVarChar, 150).Value = txtLastName.Text;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 10).Value = txtPhoneNumber.Text;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = txtAddress.Text;

                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();

                        this.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        private void txtPIN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }
    }
}
