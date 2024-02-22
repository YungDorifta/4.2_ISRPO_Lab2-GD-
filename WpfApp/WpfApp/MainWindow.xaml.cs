using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using ServerApp;
using ServerApp.Models;

namespace WpfApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void GetAllProducts()
        {
            ServerApp.WPFfunctions.GetAllProducts();
        }

        public MainWindow()
        {
            InitializeComponent();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50237/api/");

                //HTTP GET
                var responseTask = client.GetAsync("products");
                responseTask.Wait();

                var GetResult = responseTask.Result;
                if (GetResult.IsSuccessStatusCode)
                {

                    var readTask = GetResult.Content.ReadAsAsync<Product[]>();
                    readTask.Wait();

                    var products = readTask.Result;

                    foreach (var product in products)
                    {
                        Console.WriteLine("{0} {1} {2} {3}", product.Id, product.Name,
                        product.Category, product.Price);
                    }
                }
            }

        }
    }
}
