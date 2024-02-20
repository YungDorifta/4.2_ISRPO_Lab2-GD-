using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    interface IBookReaderRepository
    {
        IEnumerable<BookReader> GetAll();
        BookReader Get(int id);
        BookReader Add(BookReader item);
        void Remove(int id);
        bool Update(BookReader item);
    }
}