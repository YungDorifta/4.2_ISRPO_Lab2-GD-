using ServerApp.Models;
using ServerApp.Models.Interfaces;
using ServerApp.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ServerApp.Models.DTOs;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http.Description;

// ГОТОВО!

namespace ServerApp.Controllers
{
    public class ReadersController : ApiController
    {
        
        //static readonly IReaderRepository readerRepository = new ReaderRepository();

        /// <summary>
        /// Получить DTO всех читателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReadersDTO> GetAllReaders()
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var readers = db.Readers.Select(b => new ReadersDTO()
                {
                    ID = b.ID,
                    FIO = b.FIO
                }).ToList();
                return readers;
            }
        }

        /// <summary>
        /// Вернуть читателя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>]
        [ResponseType(typeof(ReadersDTO))]
        public async Task<IHttpActionResult> GetReader(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var reader = await db.Readers.Select(b => new ReadersDTO()
                {
                    ID = b.ID,
                    FIO = b.FIO
                }).SingleOrDefaultAsync(b => b.ID == id);

                if (reader == null)
                {
                    return NotFound();
                }

                return Ok(reader);
            }
        }

        /// <summary>
        /// Найти читателей по ФИО
        /// </summary>
        /// <param name="FIO"></param>
        /// <returns></returns>
        public ReadersDTO GetReader(string FIO)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                List<Readers> readers = db.Readers.Where(b => b.FIO.Contains(FIO)).ToList();
                List<ReadersDTO> readersDTO = new List<ReadersDTO>();
                if (readers.Count > 0)
                {
                    foreach (var reader in readers) readersDTO.Add(new ReadersDTO { ID = reader.ID, FIO = reader.FIO });
                    return readersDTO[0];
                }
                else throw new Exception("Читателей с введенным ФИО не найдено!");
            }
        }

        /// <summary>
        /// Добавить читателя
        /// </summary>
        /// <param name="readerDTO"></param>
        /// <returns></returns>
        public HttpResponseMessage PostReader(ReadersDTO readerDTO)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                Readers reader = new Readers { FIO = readerDTO.FIO };
                db.Readers.Add(reader);
                db.SaveChanges();

                var response = Request.CreateResponse<Readers>(HttpStatusCode.Created, reader);
                string uri = Url.Link("DefaultApi", new { id = readerDTO.ID });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }

        /// <summary>
        /// Изменить читателя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readerDTO"></param>
        public void PutReader(int id, ReadersDTO readerDTO)
        {

            readerDTO.ID = id;
            Readers reader = new Readers
            {
                ID = readerDTO.ID,
                FIO = readerDTO.FIO
            };

            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                db.Readers.Remove(db.Readers.Where(b => b.ID == id).FirstOrDefault());
                db.Readers.Add(reader);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Удалить читателя
        /// </summary>
        /// <param name="id"></param>
        public void DeleteReader(int id)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                var reader = db.Readers.Where(b => b.ID == id).FirstOrDefault();
                db.Readers.Remove(reader);
                db.SaveChanges();
            };
        }
    }
}