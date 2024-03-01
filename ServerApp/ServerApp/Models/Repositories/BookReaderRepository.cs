using ServerApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Repositories
{
    public class BookReaderRepository : IBookReaderRepository
    {
        private List<BookReaders> bookReadersRepository = new List<BookReaders>();
        private int _nextId = 1;

        /// <summary>
        /// Создание репозитория совместного
        /// </summary>
        public BookReaderRepository()
        {
            Reload();
        }

        /// <summary>
        /// Перезагрузка репозитория
        /// </summary>
        public void Reload()
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                bookReadersRepository = db.BookReaders.SqlQuery("SELECT * FROM [db_Belashev_ISRPO].[dbo].[BookReaders]").ToList();
                _nextId = bookReadersRepository[bookReadersRepository.Count - 1].ID + 1;
            }
        }

        public BookReaders Add(BookReaders item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.ID = _nextId++;
            bookReadersRepository.Add(item);
            return item;
        }

        public BookReaders Get(int id)
        {
            return bookReadersRepository.Find(p => p.ID == id);
        }

        public IEnumerable<BookReaders> GetAll()
        {
            return bookReadersRepository;
        }

        public void Remove(int id)
        {
            bookReadersRepository.RemoveAll(p => p.ID == id);
        }

        public bool Update(BookReaders item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = bookReadersRepository.FindIndex(p => p.ID == item.ID);
            if (index == -1)
                return false;
            bookReadersRepository.RemoveAt(index);
            bookReadersRepository.Add(item);
            return true;
        }

    }
}