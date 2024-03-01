using ServerApp.Models;
using ServerApp.Models.DTOs;
using ServerApp.Models.Interfaces;
using ServerApp.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ServerApp.Controllers
{
    public class ProductsController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return repository.GetAll();
        }

        public ProductDTO GetProduct(int id)
        {
            ProductDTO item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        
        public HttpResponseMessage PostProduct(ProductDTO item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<ProductDTO>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public void PutProduct(int id, ProductDTO product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduct(int id)
        {
            repository.Remove(id);
        }
    }

}