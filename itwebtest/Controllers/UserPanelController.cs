using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using itwebtest.Models;

namespace itwebtest.Controllers
{
    public class UserPanelController : Controller
    {
        private BooksEntities db = new BooksEntities();

        // GET: UserPanel
        public ActionResult Index()
        {
            return View(db.BooksTables.ToList());
        }

        [HttpGet]

        public ActionResult Index(string searchText)
        {

            ViewData["GetBooks"] = searchText;
            var qry = from x in db.BooksTables select x;
            if (!String.IsNullOrWhiteSpace(searchText))
            {

                qry = qry.Where(x => x.Titre.Contains(searchText));
            }

            return View(qry.AsNoTracking().ToList());
        }


        // GET: UserPanel/Details/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
