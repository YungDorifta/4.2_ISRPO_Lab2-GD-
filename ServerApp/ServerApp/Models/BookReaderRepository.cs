using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class BookReaderRepository : IBookReaderRepository
    {
        private List<BookReader> BookReaders = new List<BookReader>();
        private int _nextId = 1;

        public BookReaderRepository()
        {
            Add(new BookReader() {  });
            Add(new BookReader() {  });
            Add(new BookReader() {  });
        }

        public BookReader Add(BookReader item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.Id = _nextId++;
            BookReaders.Add(item);
            return item;
        }

        public BookReader Get(int id)
        {
            return BookReaders.Find(p => p.Id == id);
        }

        public IEnumerable<BookReader> GetAll()
        {
            return BookReaders;
        }

        public void Remove(int id)
        {
            BookReaders.RemoveAll(p => p.Id == id);
        }

        public bool Update(BookReader item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = BookReaders.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;
            BookReaders.RemoveAt(index);
            BookReaders.Add(item);
            return true;
        }

    }
}