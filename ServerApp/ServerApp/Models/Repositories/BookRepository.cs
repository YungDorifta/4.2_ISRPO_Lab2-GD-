using ServerApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Books> bookRepository = new List<Books>();
        private int _nextId = 1;
        /*
         * Подключение к БД
        private static string connectionString = "Server=ADCLG1;Database=db_Belashev_ISRPO;Trusted_Connection=True";
        private SqlConnection db = new SqlConnection(connectionString);
        */

        /// <summary>
        /// Создание репозитория книг
        /// </summary>
        public BookRepository()
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
                bookRepository = db.Books.SqlQuery("SELECT * FROM [db_Belashev_ISRPO].[dbo].[Books]").ToList();
                _nextId = bookRepository[bookRepository.Count - 1].ID + 1;
            }
        }
        
        /// <summary>
        /// Добавление книги в БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Books Add(Books item)
        {
            // Проверка на ввод нулевого
            if (item == null) throw new ArgumentNullException();
            
            // Добавление книги в БД
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Books.SqlQuery("INSERT INTO [db_Belashev_ISRPO].[dbo].[Books] ([Bookname],[Author],[Pages]) VALUES (" + item.Bookname + ", " + item.Author + ", " + item.Pages + ")");
            }
            // Перезагрузка репозитория тут
            Reload();
            // Возвращение добавленного объекта
            return item;
        }

        /// <summary>
        /// Найти книгу по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books Get(int id)
        {
            return bookRepository.Find(p => p.ID == id);
        }

        /// <summary>
        /// Найти все книги
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Books> GetAll()
        {
            return bookRepository;
        }

        /// <summary>
        /// Удалить книгу из БД
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                Books b = db.Books.Where((p) => p.ID == id).FirstOrDefault();
                db.Books.Remove(b);
                db.SaveChanges();
                //db.Books.SqlQuery("DELETE * FROM [db_Belashev_ISRPO].[dbo].[Books] WHERE (ID = " + id + ")");
            }
            Reload();
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(Books item)
        {
            // Проверка на пустой объект
            if (item == null) throw new ArgumentNullException();

            // Найти индекс объекта в репозитории 
            int index = bookRepository.FindIndex(p => p.ID == item.ID);
            if (index == -1) return false;

            // Изменить книгу
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Books.SqlQuery("UPDATE [db_Belashev_ISRPO].[dbo].[Books] SET [Bookname] = " + item.Bookname + " ,[Author] = " + item.Author + " ,[Pages] = " + item.Pages + " WHERE (ID = " + item.ID + ")");
            }

            // Обновить репозиторий
            Reload();
            return true;
        }
    }
}