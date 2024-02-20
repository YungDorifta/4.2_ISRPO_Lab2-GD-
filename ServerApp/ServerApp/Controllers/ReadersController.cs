using ServerApp.Models;
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

        public IEnumerable<Reader> GetAllReaders()
        {
            return readerRepository.GetAll();
        }

        public Reader GetReader(int id)
        {
            Reader item = readerRepository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public Reader GetReader(string FIO)
        {
            Reader item = readerRepository.Get(FIO);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostReader(Reader item)
        {
            item = readerRepository.Add(item);
            var response = Request.CreateResponse<Reader>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutReader(int id, Reader product)
        {
            product.Id = id;
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