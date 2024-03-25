using ServerApp.Models;
using ServerApp.Models.DTOs;
using ServerApp.Models.Interfaces;
using ServerApp.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

// ГОТОВО!

namespace ServerApp.Controllers
{
    public class BooksController : ApiController
    {
        //static readonly IBookRepository repository = new BookRepository();

        /// <summary>
        /// Передать DTO всех книг
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BooksDTO> GetAllBooks()
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var books = db.Books.Select(b => new BooksDTO()
                {
                    ID = b.ID,
                    BookName = b.Bookname,
                    Author = b.Author,
                    Pages = b.Pages
                }).ToList();
                return books;
            }
        }
        
        /// <summary>
        /// Вернуть книгу по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(BooksDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var book = await db.Books.Select(b => new BooksDTO()
                {
                    ID = b.ID,
                    BookName = b.Bookname,
                    Author = b.Author,
                    Pages = b.Pages
                }).SingleOrDefaultAsync(b => b.ID == id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
        }

        /// <summary>
        /// Добавить книгу
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        public HttpResponseMessage PostBook(BooksDTO bookDTO)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                Books book = new Books { Bookname = bookDTO.BookName, Author = bookDTO.Author, Pages = bookDTO.Pages };
                db.Books.Add(book);
                db.SaveChanges();

                var response = Request.CreateResponse<Books>(HttpStatusCode.Created, book);
                string uri = Url.Link("DefaultApi", new { id = bookDTO.ID });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }

        /// <summary>
        /// Удалить книгу по id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBook(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var book = db.Books.Where(b => b.ID == id).FirstOrDefault();
                db.Books.Remove(book);
                db.SaveChanges();
            };
        }

        /// <summary>
        /// Получить книги по автору
        /// </summary>
        /// <param name="authorParam">Автор</param>
        /// <returns></returns>
        public IEnumerable<BooksDTO> GetBooksByAuthor(string authorParam)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                List<Books> books = db.Books.Where(b => b.Author.Contains(authorParam)).ToList();

                List<BooksDTO> booksDTO = new List<BooksDTO>();
                foreach (var book in books) booksDTO.Add(new BooksDTO { ID = book.ID, BookName = book.Bookname, Author = book.Author, Pages = book.Pages });

                return booksDTO;
            }
            //return repository.GetAll().Where(p => string.Equals(p.Author, author, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Получить книги по названию
        /// </summary>
        /// <param name="authorParam">Автор</param>
        /// <returns></returns>
        public BooksDTO GetBooksByBookname(string booknameParam)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                List<Books> books = db.Books.Where(b => b.Bookname == booknameParam).ToList();
                if (books.Count > 0)
                {
                    Books book = books[0];
                    BooksDTO booksDTO = new BooksDTO { ID = book.ID, BookName = book.Bookname, Author = book.Author, Pages = book.Pages };
                    return booksDTO;
                }
                else return null; //throw new Exception("Книг с указанным названием не найдено");
            }
        }

        /// <summary>
        /// Изменить книгу по указанному id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDTO"></param>
        public void PutBook(int id, BooksDTO bookDTO)
        {
            bookDTO.ID = id;
            Books book = new Books {
                ID = bookDTO.ID,
                Bookname = bookDTO.BookName,
                Author = bookDTO.Author,
                Pages = bookDTO.Pages
            };

            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Books.Remove(db.Books.Where(b => b.ID == id).FirstOrDefault());
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
    }
}