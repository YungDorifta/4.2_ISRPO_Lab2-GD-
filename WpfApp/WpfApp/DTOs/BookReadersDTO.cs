using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.DTOs
{
    public class BookReadersDTO
    {
        public int ID { get; set; }
        public int ID_book { get; set; }
        public int ID_reader { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
