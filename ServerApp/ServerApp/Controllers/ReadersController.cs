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
        /// <returns></returns>
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
        public IEnumerable<ReadersDTO> GetReader(string FIO)
        {
            using (db_Belashev_ISRPOEntitiesActual db = new db_Belashev_ISRPOEntitiesActual())
            {
                List<Readers> readers = db.Readers.Where(b => b.FIO.Contains(FIO)).ToList();

                List<ReadersDTO> readersDTO = new List<ReadersDTO>();
                foreach (var reader in readers) readersDTO.Add(new ReadersDTO { ID = reader.ID, FIO = reader.FIO });

                return readersDTO;
            }
        }

        public HttpResponseMessage PostReader(ReadersDTO item)
        {
            item = readerRepository.Add(item);
            var response = Request.CreateResponse<Readers>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutReader(int id, ReadersDTO product)
        {
            product.ID = id;
            if (!readerRepository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteReader(int id)
        {
            readerRepository.Remove(id);
        }
    }
}