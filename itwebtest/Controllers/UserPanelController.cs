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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            return View(booksTable);
        }

        // GET: UserPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Titre,Auteur,Type,DateP,Editeur,NBexmplr")] BooksTable booksTable)
        {
            if (ModelState.IsValid)
            {
                db.BooksTables.Add(booksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksTable);
        }

        // GET: UserPanel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            return View(booksTable);
        }

        // POST: UserPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Titre,Auteur,Type,DateP,Editeur,NBexmplr")] BooksTable booksTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksTable);
        }

        // GET: UserPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            return View(booksTable);
        }

        // POST: UserPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksTable booksTable = db.BooksTables.Find(id);
            db.BooksTables.Remove(booksTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
