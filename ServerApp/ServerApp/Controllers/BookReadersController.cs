using ServerApp.Models;
using ServerApp.Models.DTOs;
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

//!!! переделать под DTO
//как реализовать передачу совмещенных данных?

namespace ServerApp.Controllers
{
    public class BookReadersController : ApiController
    {
        static readonly IBookReaderRepository repository = new BookReaderRepository();
        
        //!!! не работает, как должно
        /// <summary>
        /// Передать DTO всех совмещенных объектов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookReadersDTO> GetAllBookReaders()
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var bookReaders = db.BookReaders.Join(db.Books,
                    bookReader => bookReader.BookID,
                    book => book.ID,
                    (bookReader, book) => new
                    {
                        bookname = book.Bookname,
                        author = book.Author,
                        bookReaderID = bookReader.ID,
                        readerID = bookReader.ReaderID
                    }).Join(db.Readers, bookReader => bookReader.bookReaderID, reader => reader.ID,
                    (bookReader, reader) => new BookReadersDTO() {
                        //!!! Здесь заместо нынешнего ДТО вставить объект со всеми необходимыми данными
                    }).ToList();
                
                return bookReaders;
            }
        }

        //Достать совмещенную запись по ID
        public BookReaders GetBookReader(int id)
        {
            BookReaders item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //Добавить совмещенную запись
        public HttpResponseMessage PostBookReader(BookReaders item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<BookReaders>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        
        //Достать список с указанным id читателя
        public IEnumerable<BookReaders> GetBookReadersByReaderId(int ReaderID)
        {
            return repository.GetAll().Where(p => int.Equals(p.ReaderID, ReaderID));
        }

        //Достать список с указанным id книги
        public IEnumerable<BookReaders> GetBookReadersByBookId(int BookID)
        {
            return repository.GetAll().Where(p => int.Equals(p.BookID, BookID));
        }

        //Достать список с указанным временем начала
        public IEnumerable<BookReaders> GetBookReadersByStartTime(DateTime StartDate)
        {
            return repository.GetAll().Where(p => DateTime.Equals(p.StartDate, StartDate));
        }

        //Достать список с указанным временем окончания
        public IEnumerable<BookReaders> GetBookReadersByEndTime(DateTime EndDate)
        {
            return repository.GetAll().Where(p => DateTime.Equals(p.EndDate, EndDate));
        }

        //Обновить запись 
        public void PutBookReaders(int id, BookReaders bookreader)
        {
            bookreader.ID = id;
            if (!repository.Update(bookreader))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //Удалить совмещенную запись
        public void DeleteBookReader(int id)
        {
            repository.Remove(id);
        }
    }
}