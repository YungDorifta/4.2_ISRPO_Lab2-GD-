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
    public class BookReadersController : ApiController
    {
        static readonly IBookReaderRepository repository = new BookReaderRepository();
        
        public IEnumerable<BookReaders> GetAllBookReaders()
        {
            return repository.GetAll();
        }

        public BookReaders GetBookReader(int id)
        {
            BookReaders item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostBookReader(BookReaders item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<BookReaders>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// Достать список с указанным id читателя
        /// </summary>
        /// <param name="ReaderID"></param>
        /// <returns></returns>
        public IEnumerable<BookReaders> GetBookReadersByReaderId(int ReaderID)
        {
            return repository.GetAll().Where(p => int.Equals(p.ReaderID, ReaderID));
        }

        /// <summary>
        /// Достать список с указанным id книги
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public IEnumerable<BookReaders> GetBookReadersByBookId(int BookID)
        {
            return repository.GetAll().Where(p => int.Equals(p.BookID, BookID));
        }

        /// <summary>
        /// Достать список с указанным временем начала
        /// </summary>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        public IEnumerable<BookReaders> GetBookReadersByStartTime(DateTime StartDate)
        {
            return repository.GetAll().Where(p => DateTime.Equals(p.StartDate, StartDate));
        }

        /// <summary>
        /// Достать список с указанным временем окончания
        /// </summary>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IEnumerable<BookReaders> GetBookReadersByEndTime(DateTime EndDate)
        {
            return repository.GetAll().Where(p => DateTime.Equals(p.EndDate, EndDate));
        }

        public void PutBookReaders(int id, BookReaders bookreader)
        {
            bookreader.ID = id;
            if (!repository.Update(bookreader))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteBookReader(int id)
        {
            repository.Remove(id);
        }
    }
}