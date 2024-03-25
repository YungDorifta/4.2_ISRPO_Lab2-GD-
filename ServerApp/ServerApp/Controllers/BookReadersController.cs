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

namespace ServerApp.Controllers
{
    public class BookReadersController : ApiController
    {
        static readonly IBookReaderRepository repository = new BookReaderRepository();
        
        /// <summary>
        /// (ГОТОВО) Передать DTO всех совмещенных объектов
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
                        bookReaderID = bookReader.ID,
                        bookID = book.ID,
                        bookname = book.Bookname,
                        author = book.Author,
                        pages = book.Pages,
                        readerID = bookReader.ReaderID,
                        startdate = bookReader.StartDate,
                        enddate = bookReader.EndDate
                    }).Join(db.Readers, bookReader => bookReader.bookReaderID, reader => reader.ID,
                    (bookReader, reader) => new BookReadersDTO() {
                        ID = bookReader.bookReaderID,
                        readerID = reader.ID,
                        FIO = reader.FIO,
                        bookID = bookReader.bookID,
                        BookName = bookReader.bookname,
                        Author = bookReader.author,
                        Pages = bookReader.pages,
                        StartDate = bookReader.startdate,
                        EndDate = bookReader.enddate
                    }).ToList();
                
                return bookReaders;
            }
        }

        /// <summary>
        /// Достать совмещенную запись по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(BookReadersDTO))]
        public async Task<IHttpActionResult> GetBookReader(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var bookreader = db.BookReaders.Join(db.Books,
                    bookReader => bookReader.BookID,
                    book => book.ID,
                    (bookReader, book) => new
                    {
                        bookReaderID = bookReader.ID,
                        bookID = book.ID,
                        bookname = book.Bookname,
                        author = book.Author,
                        pages = book.Pages,
                        readerID = bookReader.ReaderID,
                        startdate = bookReader.StartDate,
                        enddate = bookReader.EndDate
                    }).Join(db.Readers, bookReader => bookReader.bookReaderID, reader => reader.ID,
                    (bookReader, reader) => new BookReadersDTO()
                    {
                        ID = bookReader.bookReaderID,
                        readerID = reader.ID,
                        FIO = reader.FIO,
                        bookID = bookReader.bookID,
                        BookName = bookReader.bookname,
                        Author = bookReader.author,
                        Pages = bookReader.pages,
                        StartDate = bookReader.startdate,
                        EndDate = bookReader.enddate
                    }).SingleOrDefaultAsync(b => b.ID == id);

                if (bookreader == null)
                {
                    return NotFound();
                }

                return Ok(bookreader);
            }
        }

        /// <summary>
        /// Добавить совмещенную запись
        /// </summary>
        /// <param name="bookReaderArg"></param>
        /// <returns></returns>
        public HttpResponseMessage PostBookReader(BookReadersDTO bookReaderArg)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                Books book = db.Books.Where(b => b.Bookname == bookReaderArg.BookName).FirstOrDefault();
                Readers reader = db.Readers.Where(b => b.FIO == bookReaderArg.FIO).FirstOrDefault();
                if (book == null) {
                    //добавить книгу при отсутствии
                    book = new Books { Bookname = bookReaderArg.BookName, Author = bookReaderArg.Author, Pages = bookReaderArg.Pages};
                    db.Books.Add(book);
                    //db.SaveChanges();
                }
                if (reader == null)
                {
                    //добавить читателя при отсутствии
                    reader = new Readers { FIO = bookReaderArg.FIO };
                    db.Readers.Add(reader);
                    //db.SaveChanges();
                }
                db.SaveChanges();

                book = db.Books.Where(b => b.Bookname == bookReaderArg.BookName).FirstOrDefault();
                reader = db.Readers.Where(b => b.FIO == bookReaderArg.FIO).FirstOrDefault();
                BookReaders bookreader = new BookReaders { BookID = book.ID, ReaderID = reader.ID, StartDate = bookReaderArg.StartDate, EndDate = bookReaderArg.EndDate };
                db.BookReaders.Add(bookreader);
                db.SaveChanges();

                var response = Request.CreateResponse<BookReaders>(HttpStatusCode.Created, bookreader);
                string uri = Url.Link("DefaultApi", new { id = bookReaderArg.ID });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }

        /// <summary>
        /// Удалить совмещенную запись
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBookReader(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var bookreader = db.BookReaders.Where(b => b.ID == id).FirstOrDefault();
                db.BookReaders.Remove(bookreader);
                db.SaveChanges();
            };
        }



        /*не используется

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
        */
    }
}