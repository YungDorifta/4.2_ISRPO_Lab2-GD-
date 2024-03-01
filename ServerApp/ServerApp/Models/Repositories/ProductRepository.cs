using ServerApp.Models.Interfaces;
using ServerApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<ProductDTO> Readers = new List<ProductDTO>();
        private int _nextId = 1;

        public ProductRepository()
        {
            Add(new ProductDTO() { Name = "Tomato", Category = "AAA", Price = 123 });
            Add(new ProductDTO() { Name = "Banana", Category = "BBB", Price = 123 });
            Add(new ProductDTO() { Name = "Potato", Category = "CCC", Price = 123 });
        }

        public ProductDTO Add(ProductDTO item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.Id = _nextId++;
            Readers.Add(item);
            return item;
        }

        public ProductDTO Get(int id)
        {
            return Readers.Find(p => p.Id == id);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            return Readers;
        }

        public void Remove(int id)
        {
            Readers.RemoveAll(p => p.Id == id);
        }

        public bool Update(ProductDTO item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = Readers.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;
            Readers.RemoveAt(index);
            Readers.Add(item);
            return true;
        }
    }

}