using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Interfaces
{
    interface IReaderRepository
    {
        IEnumerable<Readers> GetAll();
        Readers Get(int id);
        Readers Get(string FIO);
        Readers Add(Readers item);
        void Remove(int id);
        bool Update(Readers item);
    }
}