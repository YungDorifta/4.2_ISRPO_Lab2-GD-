using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApp.Models.DTOs
{
    public class BookReadersDTO
    {
        public int ID { get; set; }
        public int readerID { get; set; }
        public string FIO { get; set; }
        public int bookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}