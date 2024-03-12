using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DTOs;

namespace WpfApp
{
    public class WPFfunctions
    {
        private static string stringBaseAddress = "http://localhost:50237/api/";
        
        // Для книг (ГОТОВО для string) 
        /// <summary>
        /// Получение всех записей в таблице
        /// </summary>
        public static List<BooksDTO> GetAllBooks()
        {
            List<BooksDTO> LB = new List<BooksDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);
                
                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("Books");
                responseTask.Wait();

                var GetResult = responseTask.Result;

                string ResultBooksMessage = "";
                if (GetResult.IsSuccessStatusCode)
                {

                    var readTask = GetResult.Content.ReadAsAsync<BooksDTO[]>();
                    readTask.Wait();

                    var books = readTask.Result;
                    foreach (var book in books)
                    {
                        //Console.WriteLine("{0} {1} {2} {3}", book.ID, book.BookName, book.Author, book.Pages);
                        //ResultBooksMessage += book.ID + "\t" + book.BookName.TrimEnd(' ') + "\t" + book.Author.TrimEnd(' ') + "\t" + book.Pages + "\n";
                        LB.Add(book);
                    }
                }
                //Console.ReadLine();
                return LB;
            }
        }
        
        /// <summary>
        /// Получить информацию о продукте с ID = 2
        /// </summary>
        public static string GetInfoBook(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("Books/" + id);
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;

                string ResultBooksMessage = "";
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<BooksDTO> readTask = GetResult.Content.ReadAsAsync<BooksDTO>();
                    readTask.Wait();

                    BooksDTO book = readTask.Result;
                    ResultBooksMessage = book.ID + "\t" + book.BookName.TrimEnd(' ') + "\t" + book.Author.TrimEnd(' ') + "\t" + book.Pages + "\n";
                    //Console.WriteLine("{0} {1} {2} {3}", product.Id, product.Name, product.Category, product.Price);
                }
                return ResultBooksMessage;
            }
        }
        
        /// <summary>
        /// Добавление одной записи в таблицу
        /// </summary>
        public static string AddBook(string BookName, string Author, int Pages)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP POST --------------------------------------
                var book = new BooksDTO()
                {
                    BookName = BookName,
                    Author = Author,
                    Pages = Pages
                };

                var postTask = client.PostAsJsonAsync<BooksDTO>("Books", book);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {

                    var readTask = PostResult.Content.ReadAsAsync<BooksDTO>();
                    readTask.Wait();

                    var insertedBook = readTask.Result;

                    //Console.WriteLine("Book {0} inserted with id: {1}", insertedBook.BookName, insertedBook.ID);
                    return "Book " + insertedBook.BookName + " inserted with id: " + insertedBook.ID;
                }
                else
                {
                    //Console.WriteLine(PostResult.StatusCode);
                    return PostResult.StatusCode.ToString();
                }
            }
        }
        
        /// <summary>
        /// Удаление книги по id
        /// </summary>
        public static string DeleteBook(int id)
        {
            using (var client = new HttpClient())
            {
                // не забудьте поменять порт 
                client.BaseAddress = new Uri(stringBaseAddress);

                // операция удаления
                var deleteTask = client.DeleteAsync($"Books/" + id);
                deleteTask.Wait();

                // получение StatusCode необязательно
                HttpResponseMessage deleteResult = deleteTask.Result;
                //Console.WriteLine(deleteResult.StatusCode.ToString());
                return deleteResult.StatusCode.ToString();
            }
        }
        
        /// <summary>
        /// Получение книг по автору
        /// </summary>
        public static string GetInfoBooksWithAuthor(string authorParam)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("Books?authorParam=" + authorParam);
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;

                string result = "";
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<BooksDTO[]> readTask = GetResult.Content.ReadAsAsync<BooksDTO[]>();
                    readTask.Wait();

                    BooksDTO[] books = readTask.Result;
                    
                    foreach (var book in books)
                    {
                        //Console.WriteLine("{0} {1} {2} {3}", book.ID, book.BookName, book.Author, book.Pages);
                        result += book.ID + "\t" + book.BookName.TrimEnd(' ') + "\t" + book.Author.TrimEnd(' ') + "\t" + book.Pages + "\n";
                    }
                }
                return result;
            }

        }
        
        /// <summary>
        /// Обновление информации о продукте с ID = 2
        /// </summary>
        public static void UpdateInfoBook(BooksDTO bookDTO)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);
                
                Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync("Books/" + bookDTO.ID, bookDTO);
                responseTask.Wait();

                
                HttpResponseMessage resultTask = responseTask.Result;

                /*
                if (resultTask.IsSuccessStatusCode)
                {
                    Task<BooksDTO> readResult = resultTask.Content.ReadAsAsync<BooksDTO>();
                    readResult.Wait();

                    BooksDTO result = readResult.Result;
                    //Console.WriteLine("{0} {1} {2} {3}", result.Id, result.Name, result.Category, product.Price);

                }
                */
            }

        }

        //для читателей

        // Для смешанной таблицы


        //для продуктов
        /*
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

                    var readTask = GetResult.Content.ReadAsAsync<ProductDTO[]>();
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
                var student = new ProductDTO()
                {
                    Name = "Сarrot",
                    Category = "DDD",
                    Price = 123
                };

                var postTask = client.PostAsJsonAsync<ProductDTO>("products", student);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {

                    var readTask = PostResult.Content.ReadAsAsync<ProductDTO>();
                    readTask.Wait();

                    var insertedProduct = readTask.Result;

                    Console.WriteLine("ProductDTO {0} inserted with id: {1}",
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

                    Task<ProductDTO> readTask = GetResult.Content.ReadAsAsync<ProductDTO>();
                    readTask.Wait();

                    ProductDTO product = readTask.Result;

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

                    Task<ProductDTO[]> readTask = GetResult.Content.ReadAsAsync<ProductDTO[]>();
                    readTask.Wait();

                    ProductDTO[] products = readTask.Result;

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

                ProductDTO product = new ProductDTO()
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
                    Task<ProductDTO> readResult = resultTask.Content.ReadAsAsync<ProductDTO>();
                    readResult.Wait();

                    ProductDTO result = readResult.Result;
                    Console.WriteLine("{0} {1} {2} {3}",
                                        result.Id, result.Name, result.Category, product.Price);

                }
            }

        }
        */
    }
}
