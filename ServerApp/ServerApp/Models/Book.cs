using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string NameOfBook { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
    }
}