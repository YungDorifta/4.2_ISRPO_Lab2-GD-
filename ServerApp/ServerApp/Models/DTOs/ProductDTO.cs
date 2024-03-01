using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

}