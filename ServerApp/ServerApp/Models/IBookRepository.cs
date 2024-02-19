using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Product Get(int id);
        Product Add(Book item);
        void Remove(int id);
        bool Update(Book item);
    }
}