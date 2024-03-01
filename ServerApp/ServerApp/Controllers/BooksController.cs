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


// Переделать на вывод DTO!!!

namespace ServerApp.Controllers
{
    public class BooksController : ApiController
    {
        static readonly IBookRepository repository = new BookRepository();

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
        /// Вернуть книгу с заданным id
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

            /*
            BooksDTO item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
            */
        }





        //
        public HttpResponseMessage PostBook(Books item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Books>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        //
        public IEnumerable<Books> GetBooksByAuthor(string author)
        {
            return repository.GetAll().Where(p => string.Equals(p.Author, author, StringComparison.OrdinalIgnoreCase));
        }
        //
        public void PutBook(int id, Books book)
        {
            book.ID = id;
            if (!repository.Update(book))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        //
        public void DeleteBook(int id)
        {
            repository.Remove(id);
        }
    }
}