using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using itwebtest.Models;

namespace itwebtest.Controllers
{
    public class BooksAPIController : ApiController
    {
        private BooksEntities db = new BooksEntities();

        // GET: api/BooksAPI
        [HttpGet]
        public IQueryable<BooksTable> GetBooksTables()
        {
            return db.BooksTables;
        }

        // GET: api/BooksAPI/5
        [ResponseType(typeof(BooksTable))]
        

        public IHttpActionResult GetBooksTable(int id)
        {
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return NotFound();
            }

            return Ok(booksTable);
        }

        // PUT: api/BooksAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooksTable(int id, BooksTable booksTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booksTable.id)
            {
                return BadRequest();
            }

            db.Entry(booksTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BooksAPI
        [ResponseType(typeof(BooksTable))]
        public IHttpActionResult PostBooksTable(BooksTable booksTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BooksTables.Add(booksTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BooksTableExists(booksTable.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = booksTable.id }, booksTable);
        }

        // DELETE: api/BooksAPI/5
        [ResponseType(typeof(BooksTable))]
        public IHttpActionResult DeleteBooksTable(int id)
        {
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return NotFound();
            }

            db.BooksTables.Remove(booksTable);
            db.SaveChanges();

            return Ok(booksTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BooksTableExists(int id)
        {
            return db.BooksTables.Count(e => e.id == id) > 0;
        }
    }
}