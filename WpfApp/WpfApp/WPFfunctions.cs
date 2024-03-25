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

        //общее
        //!!! переделать повторяющийся код для выбора типа объекта
        /// <summary>
        /// Получение всех записей в таблице
        /// </summary>
        public static List<Object> GetAll(Object typeObject)
        {
            List<Object> LB = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = null;
                if (typeObject is BooksDTO) responseTask = client.GetAsync("Books");
                else if (typeObject is ReadersDTO) responseTask = client.GetAsync("Readers");
                else if (typeObject is BookReadersDTO) responseTask = client.GetAsync("BookReaders");

                if (responseTask != null)
                {
                    responseTask.Wait();
                    var GetResult = responseTask.Result;

                    //Изъятие массива данных
                    if (GetResult.IsSuccessStatusCode)
                    {
                        if (typeObject is BooksDTO)
                        {
                            var readTask = GetResult.Content.ReadAsAsync<BooksDTO[]>();
                            readTask.Wait();
                            var objects = readTask.Result;
                            foreach (var obj in objects) LB.Add(obj);
                        }
                        else if (typeObject is ReadersDTO)
                        {
                            var readTask = GetResult.Content.ReadAsAsync<ReadersDTO[]>();
                            readTask.Wait();
                            var objects = readTask.Result;
                            foreach (var obj in objects) LB.Add(obj);
                        }
                        else if (typeObject is BookReadersDTO)
                        {
                            var readTask = GetResult.Content.ReadAsAsync<BookReadersDTO[]>();
                            readTask.Wait();
                            var objects = readTask.Result;
                            //!!! не выводит информацию о книге и читателе
                            foreach (var obj in objects) LB.Add(obj);
                        }
                    }

                    //Возвращение полученного массива
                    return LB;
                }
                else throw new Exception("Указанный тип объекта не соответствует ни одному доступному!");
            }
        }

        /// <summary>
        /// Получить информацию об объекте по ID
        /// </summary>
        public static Object GetInfo(int id, Object typeObject)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = null;
                if (typeObject is BooksDTO) responseTask = client.GetAsync("Books/" + id);
                else if (typeObject is ReadersDTO) responseTask = client.GetAsync("Readers/" + id);
                else if (typeObject is BookReadersDTO) responseTask = client.GetAsync("BookReaders/" + id);

                if (responseTask != null)
                {
                    responseTask.Wait();
                    HttpResponseMessage GetResult = responseTask.Result;
                    if (GetResult.IsSuccessStatusCode)
                    {
                        if (typeObject is BooksDTO)
                        {
                            Task<BooksDTO> readTask = GetResult.Content.ReadAsAsync<BooksDTO>();
                            readTask.Wait();
                            BooksDTO book = readTask.Result;
                            return book;
                        }
                        else if (typeObject is ReadersDTO)
                        {
                            Task<ReadersDTO> readTask = GetResult.Content.ReadAsAsync<ReadersDTO>();
                            readTask.Wait();
                            ReadersDTO reader = readTask.Result;
                            return reader;
                        }
                        else if (typeObject is BookReadersDTO)
                        {
                            Task<BookReadersDTO> readTask = GetResult.Content.ReadAsAsync<BookReadersDTO>();
                            readTask.Wait();
                            BookReadersDTO bookreaders = readTask.Result;
                            return bookreaders;
                        }
                        else throw new Exception("Информация об объекте по id не изъята: тип объекта не входит в допустиые!");
                    }
                    else throw new Exception("Объект с выбранным ID не найден!");
                }
                else throw new Exception("Информация об объекте по id не изъята: тип объекта не входит в допустиые!");
            }
        }

        /// <summary>
        /// Удаление объекта по id
        /// </summary>
        public static string DeleteObj(int id, Object typeObject)
        {
            using (var client = new HttpClient())
            {
                // не забудьте поменять порт 
                client.BaseAddress = new Uri(stringBaseAddress);
                
                string controllerName = "";
                if (typeObject is BooksDTO) controllerName = "Books";
                else if (typeObject is ReadersDTO) controllerName = "Readers";
                else if (typeObject is BookReadersDTO) controllerName = "BookReaders";
                else throw new Exception("Удаление объекта: тип объекта не соответствует ни одному допустимому!");

                // операция удаления
                var deleteTask = client.DeleteAsync($"" + controllerName + "/" + id);
                deleteTask.Wait();

                // получение StatusCode необязательно
                HttpResponseMessage deleteResult = deleteTask.Result;
                return deleteResult.StatusCode.ToString();
            }
        }


        // Для книг (ГОТОВО)
        /// <summary>
        /// Добавление книги в таблицу
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
        /// Получение книги по названию
        /// </summary>
        public static BooksDTO GetInfoBooksWithBookname(string booknameParam)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("Books?booknameParam=" + booknameParam);
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;

                BooksDTO book = null;
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<BooksDTO> readTask = GetResult.Content.ReadAsAsync<BooksDTO>();
                    readTask.Wait();
                    book = readTask.Result;
                }
                return book;
            }
        }

        /// <summary>
        /// Обновление информации о книге
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
        /// <summary>
        /// Добавление читателя в таблицу
        /// </summary>
        public static string AddReader(string FIO)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP POST --------------------------------------
                var reader = new ReadersDTO()
                {
                    FIO = FIO
                };

                var postTask = client.PostAsJsonAsync<ReadersDTO>("Readers", reader);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {
                    var readTask = PostResult.Content.ReadAsAsync<ReadersDTO>();
                    readTask.Wait();
                    var insertedReader = readTask.Result;
                    return "Reader " + insertedReader.FIO + " inserted with id: " + insertedReader.ID;
                }
                else
                {
                    return PostResult.StatusCode.ToString();
                }
            }
        }
        
        /// <summary>
        /// Получение записи по ФИО
        /// </summary>
        /// <param name="FIOParam"></param>
        /// <returns></returns>
        public static ReadersDTO GetInfoReaderWithFIO(string FIOParam)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);

                //HTTP GET
                Task<HttpResponseMessage> responseTask = client.GetAsync("Readers?FIOParam=" + FIOParam);
                responseTask.Wait();

                HttpResponseMessage GetResult = responseTask.Result;

                ReadersDTO reader = null;
                if (GetResult.IsSuccessStatusCode)
                {

                    Task<ReadersDTO> readTask = GetResult.Content.ReadAsAsync<ReadersDTO>();
                    readTask.Wait();
                    reader = readTask.Result;
                }
                return reader;
            }
        }
        //изменение записи



        //для смешанной таблицы
        /// <summary>
        /// Добавление общей записи
        /// </summary>
        /// <param name="FIO"></param>
        /// <param name="BookName"></param>
        /// <param name="Author"></param>
        /// <param name="Pages"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static string AddBookReader(string FIO, string BookName, string Author, int Pages, DateTime StartTime, DateTime EndTime )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(stringBaseAddress);
                
                //HTTP POST --------------------------------------
                var bookReader = new BookReadersDTO()
                {
                    FIO = FIO,
                    BookName = BookName,
                    Author = Author,
                    Pages = Pages,
                    StartDate = StartTime,
                    EndDate = EndTime
                };

                var postTask = client.PostAsJsonAsync<BookReadersDTO>("BookReaders", bookReader);
                postTask.Wait();

                var PostResult = postTask.Result;
                if (PostResult.IsSuccessStatusCode)
                {
                    var readTask = PostResult.Content.ReadAsAsync<BookReadersDTO>();
                    readTask.Wait();
                    var insertedReader = readTask.Result;
                    return "BookReader (reader name " + insertedReader.FIO + ", book " + insertedReader.BookName + " inserted with id: " + insertedReader.ID;
                }
                else
                {
                    return PostResult.StatusCode.ToString();
                }
            }
        }
        //получение записи по <...>
        //изменение
    }
}
