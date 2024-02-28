using ServerApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private List<Readers> readersRepository = new List<Readers>();
        private int _nextId = 1;

        /// <summary>
        /// Создание репозитория читателей
        /// </summary>
        public ReaderRepository()
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
                readersRepository = db.Readers.SqlQuery("SELECT * FROM [db_Belashev_ISRPO].[dbo].[Readers]").ToList();
                _nextId = readersRepository[readersRepository.Count - 1].ID + 1;
            }
        }

        /// <summary>
        /// Добавление читателя в БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Readers Add(Readers item)
        {
            // Проверка на ввод нулевого
            if (item == null) throw new ArgumentNullException();

            // Добавление читателя в БД
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Readers.SqlQuery("INSERT INTO [db_Belashev_ISRPO].[dbo].[Readers] ([FIO]) VALUES (" + item.FIO + ")");
            }
            // Перезагрузка репозитория тут
            Reload();
            // Возвращение добавленного объекта
            return item;
        }

        /// <summary>
        /// Найти читателя по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Readers Get(int id)
        {
            return readersRepository.Find(p => p.ID == id);
        }

        /// <summary>
        /// Найти читателя по ФИО
        /// </summary>
        /// <param name="FIO"></param>
        /// <returns></returns>
        public Readers Get(string FIO)
        {
            return readersRepository.Find(p => p.FIO == FIO);
        }

        /// <summary>
        /// Найти всех читателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Readers> GetAll()
        {
            return readersRepository;
        }

        /// <summary>
        /// Удалить читателя по id
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Readers.SqlQuery("DELETE * FROM [db_Belashev_ISRPO].[dbo].[Readers] WHERE (ID = " + id + ")");
            }
            Reload();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(Readers item)
        {
            // Проверка на пустой объект
            if (item == null) throw new ArgumentNullException();

            // Найти индекс объекта в репозитории 
            int index = readersRepository.FindIndex(p => p.ID == item.ID);
            if (index == -1) return false;

            // Изменить читателя
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Readers.SqlQuery("UPDATE [db_Belashev_ISRPO].[dbo].[Readers] SET [FIO] = " + item.FIO + " WHERE (ID = " + item.ID + ")");
            }

            // Обновить репозиторий
            Reload();
            return true;
        }
    }
}