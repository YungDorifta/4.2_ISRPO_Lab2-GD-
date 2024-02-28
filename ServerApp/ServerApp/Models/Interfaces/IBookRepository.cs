using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Interfaces
{
    interface IBookRepository
    {
        IEnumerable<Books> GetAll();
        Books Get(int id);
        Books Add(Books item);
        void Remove(int id);
        bool Update(Books item);
    }
}