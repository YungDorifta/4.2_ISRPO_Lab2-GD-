using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class ReaderRepository : IReaderRepository
    {
        private List<Reader> Readers = new List<Reader>();
        private int _nextId = 1;

        public ReaderRepository()
        {
            Add(new Reader() { FIO = "Белашев Арсений Дмитриевич" });
            Add(new Reader() { FIO = "Белашев Арсений Дмитриевич" });
            Add(new Reader() { FIO = "Белашев Арсений Дмитриевич" });
        }

        public Reader Add(Reader item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.Id = _nextId++;
            Readers.Add(item);
            return item;
        }

        public Reader Get(int id)
        {
            return Readers.Find(p => p.Id == id);
        }

        public IEnumerable<Reader> GetAll()
        {
            return Readers;
        }

        public void Remove(int id)
        {
            Readers.RemoveAll(p => p.Id == id);
        }

        public bool Update(Reader item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = Readers.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;
            Readers.RemoveAt(index);
            Readers.Add(item);
            return true;
        }
    }
}