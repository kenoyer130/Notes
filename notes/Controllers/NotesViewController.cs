using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace notes.Controllers
{
    public class NotesViewController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}
