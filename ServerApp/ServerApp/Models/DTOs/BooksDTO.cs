using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.DTOs
{
    public class BooksDTO
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
    }
}