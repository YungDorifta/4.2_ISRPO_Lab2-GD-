using ServerApp.Models;
using ServerApp.Models.Interfaces;
using ServerApp.Models.Repositories;
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

        public IEnumerable<Books> GetAllProducts()
        {
            return repository.GetAll();
        }

        public Books GetBook(int id)
        {
            Books item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostBook(Books item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Books>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public IEnumerable<Books> GetBooksByAuthor(string author)
        {
            return repository.GetAll().Where(p => string.Equals(p.Author, author, StringComparison.OrdinalIgnoreCase));
        }

        public void PutBook(int id, Books book)
        {
            book.ID = id;
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