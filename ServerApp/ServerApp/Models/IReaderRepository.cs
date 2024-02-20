using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    interface IReaderRepository
    {
        IEnumerable<Reader> GetAll();
        Reader Get(int id);
        Reader Get(string FIO);
        Reader Add(Reader item);
        void Remove(int id);
        bool Update(Reader item);
    }
}