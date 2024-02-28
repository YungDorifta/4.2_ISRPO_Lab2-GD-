using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Interfaces
{
    interface IBookReaderRepository
    {
        IEnumerable<BookReaders> GetAll();
        BookReaders Get(int id);
        BookReaders Add(BookReaders item);
        void Remove(int id);
        bool Update(BookReaders item);
    }
}