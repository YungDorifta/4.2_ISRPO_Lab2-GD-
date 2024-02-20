using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServerApp.Controllers
{
    public class BooksController : ApiController
    {
        static readonly IBookRepository repository = new BookRepository();

        public IEnumerable<Book> GetAllProducts()
        {
            return repository.GetAll();
        }

        public Book GetBook(int id)
        {
            Book item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostBook(Book item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Book>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return repository.GetAll().Where(p => string.Equals(p.Author, author, StringComparison.OrdinalIgnoreCase));
        }

        public void PutBook(int id, Book book)
        {
            book.Id = id;
            if (!repository.Update(book))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteBook(int id)
        {
            repository.Remove(id);
        }
    }
}