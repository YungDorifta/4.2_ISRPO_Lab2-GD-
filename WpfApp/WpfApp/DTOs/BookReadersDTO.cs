using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//!!! нужно ли передавать книгу и читателя объектами?

namespace WpfApp.DTOs
{
    public class BookReadersDTO
    {
        public int ID { get; set; }
        public BooksDTO book { get; set; }
        public ReadersDTO reader { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
