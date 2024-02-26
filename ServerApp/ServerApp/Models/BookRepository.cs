using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class BookRepository : IBookRepository
    {
        private List<Book> Books = new List<Book>();
        private int _nextId = 1;
        private static string connectionString = "Server=ADCLG1;Database=db_Belashev_ISRPO;Trusted_Connection=True";
        private SqlConnection db = new SqlConnection(connectionString);

        public BookRepository()
        {
            db.Open();
            using (db) {
                Books = db.
            }
            db.Close();
        }

        public Book Add(Book item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.Id = _nextId++;
            Books.Add(item);
            return item;
        }

        public Book Get(int id)
        {
            return Books.Find(p => p.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return Books;
        }

        public void Remove(int id)
        {
            Books.RemoveAll(p => p.Id == id);
        }

        public bool Update(Book item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = Books.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;
            Books.RemoveAt(index);
            Books.Add(item);
            return true;
        }
    }
}