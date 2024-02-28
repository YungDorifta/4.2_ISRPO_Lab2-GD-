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

namespace ServerApp.Controllers
{
    public class ReadersController : ApiController
    {
        static readonly IReaderRepository readerRepository = new ReaderRepository();

        public IEnumerable<Readers> GetAllReaders()
        {
            return readerRepository.GetAll();
        }

        public Readers GetReader(int id)
        {
            Readers item = readerRepository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public Readers GetReader(string FIO)
        {
            Readers item = readerRepository.Get(FIO);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostReader(Readers item)
        {
            item = readerRepository.Add(item);
            var response = Request.CreateResponse<Readers>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutReader(int id, Readers product)
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