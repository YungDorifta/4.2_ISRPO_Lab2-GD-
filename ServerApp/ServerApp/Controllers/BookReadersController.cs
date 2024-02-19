using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerApp.Controllers
{
    public class BookReadersController : Controller
    {
        // GET: BookReaders
        public ActionResult Index()
        {
            return View();
        }
    }
}