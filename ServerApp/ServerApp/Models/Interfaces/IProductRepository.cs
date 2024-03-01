using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServerApp.Models.DTOs;

namespace ServerApp.Models.Interfaces
{
    interface IProductRepository
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO Get(int id);
        ProductDTO Add(ProductDTO item);
        void Remove(int id);
        bool Update(ProductDTO item);
    }

}