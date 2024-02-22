using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class WPFfunctions
    {
        private static string stringBaseAddress = "http://localhost:50237/api/";

        //для смешанной таблицы

        //для книг

        //для читателей



        //для продуктов
        /// <summary>
        /// Получение всех записей в таблице
        /// </summary>
        public static void GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("products");
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
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Добавление одной записи в таблицу
        /// </summary>
        public static void AddProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP POST --------------------------------------
                var student = new Product()
                {
                    Name = "Сarrot",
                    Category = "DDD",
                    Price = 123
                };

                var postTask = client.PostAsJsonAsync<Product>("products", student);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {

                    var readTask = PostResult.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    var insertedProduct = readTask.Result;

                    Console.WriteLine("Product {0} inserted with id: {1}",
                                                           insertedProduct.Name, insertedProduct.Id);
                }
                else
                {
                    Console.WriteLine(PostResult.StatusCode);
                }
            }
        }

        /// <summary>
        /// Удаление продукта с ID = 2
        /// </summary>
        public static void DeleteProduct()
        {
            using (var client = new HttpClient())
            {
                // не забудьте поменять порт 
                client.BaseAddress = new Uri(stringBaseAddress);

                // операция удаления
                var deleteTask = client.DeleteAsync($"products/2");
                deleteTask.Wait();

                // получение StatusCode необязательно
                HttpResponseMessage deleteResult = deleteTask.Result;
                Console.WriteLine(deleteResult.StatusCode.ToString());
            }
        }

        /// <summary>
        /// Получить информацию о продукте с ID = 2
        /// </summary>
        public static void GetInfoProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("products/2");
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<Product> readTask = GetResult.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    Product product = readTask.Result;

                    Console.WriteLine("{0} {1} {2} {3}", product.Id, product.Name,
                    product.Category, product.Price);
                }
            }
        }

        /// <summary>
        /// Получение информации о продуктах с заданной категорией
        /// </summary>
        public static void GetInfoProductsWithCategory()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("products?category=AAA");
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<Product[]> readTask = GetResult.Content.ReadAsAsync<Product[]>();
                    readTask.Wait();

                    Product[] products = readTask.Result;

                    foreach (var product in products)
                    {
                        Console.WriteLine("{0} {1} {2} {3}", product.Id, product.Name,
                        product.Category, product.Price);
                    }
                }
            }

        }

        /// <summary>
        /// Обновление информации о продукте с ID = 2
        /// </summary>
        public static void UpdateInfoProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                Product product = new Product()
                {
                    Name = "Сarrot",
                    Category = "DDD",
                    Price = 123
                };

                Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync("products/2", product);
                responseTask.Wait();

                HttpResponseMessage resultTask = responseTask.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    Task<Product> readResult = resultTask.Content.ReadAsAsync<Product>();
                    readResult.Wait();

                    Product result = readResult.Result;
                    Console.WriteLine("{0} {1} {2} {3}",
                                        result.Id, result.Name, result.Category, product.Price);

                }
            }

        }
    }
}
