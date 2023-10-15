using CarShop.Contexts;
using CarShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        public List<Client> ClientsList { get; set; }
        private CarShopContext context = new CarShopContext();
        public ClientsWindow()
        {
            InitializeComponent();

            ClientsList = new List<Client>();
            //string connetionString = "Data Source=MSSQLLocalDB;Initial Catalog=CarShop.Contexts.CarShopContext;User ID =jumpe; Password =student;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;Application Name=CarShop;Network Library=dbmssocn;Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT ClientId, FirstName, LastName, PIN, PhoneNumber, Address FROM Clients ";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var client = new Client()
                        {
                            ClientId = int.Parse(reader["ClientId"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PIN = reader["PIN"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString()
                        };

                        ClientsList.Add(client);
                    }

                }
            }

            grdClients.ItemsSource = ClientsList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int? clientId = (grdClients.SelectedItem as Client)?.ClientId;
            if (clientId != null)
            {
                var client = context.Clients.Where(a => a.ClientId == clientId).SingleOrDefault();
                context.Clients.Remove(client);
                context.SaveChanges();

                grdClients.ItemsSource = context.Clients.ToList();
            }
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new AddClient().ShowDialog();
            ShowDialog();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CarShopContext context = new CarShopContext();
            grdClients.ItemsSource = ClientsList = context.Clients.ToList();
            grdClients.Items.Refresh();
        }

        private void grdClients_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Client newClient = grdClients.SelectedItem as Client;
            if (newClient != null)
            {
                Client client = context.Clients.Find(newClient.ClientId);
                if (MessageBox.Show(Messages.UpdateMessage, Messages.UpdateTitle, System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    context.Entry(client).State = EntityState.Modified;
                    context.SaveChanges();
                }
                grdClients.ItemsSource = context.Clients.ToList();
            }
        }
    }
}

