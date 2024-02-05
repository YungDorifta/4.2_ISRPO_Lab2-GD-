using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
            Add(new Product() { Name = "Tomato", Category = "AAA", Price = 123 });
            Add(new Product() { Name = "Banana", Category = "BBB", Price = 123 });
            Add(new Product() { Name = "Potato", Category = "CCC", Price = 123 });
        }
        public Product Add(Product item)
        {
            if (item == null)
                throw new ArgumentNullException();
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }

        public Product Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            if (item == null)
                throw new ArgumentNullException();
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }
    }

}