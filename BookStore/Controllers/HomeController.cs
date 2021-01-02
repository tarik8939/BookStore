using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Threading.Tasks;
using System.Net;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

//using PagingApp.Models;
using PagedList.Mvc;
using PagedList;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        List<Book> Books;
        public HomeController()
        {
            Books = new List<Book>();
            Books = db.Books.ToList();
            ViewBag.Authors = db.Books.Select(s => s.Author).Distinct();
        }
        //[Authorize]
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Books.ToPagedList(pageNumber, pageSize));
            //return View(await db.Books.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public async Task<string> Buy(Purchase purchase, Book book)
        {
                purchase.Date = DateTime.Now;
                purchase.BookId = book.Id;
            //purchase.Books.Add(book);
                purchase.Books.Add(book);
                //purchase.Books.Add(db.Books.Find(purchase.BookId));
                db.Purchases.Add(purchase);
                db.SaveChangesAsync();
                return $"Thank you {purchase.Person} for the purchase.{purchase.BookId}";
            
        }
        [HttpGet]
        public async Task<ActionResult> EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        public async Task<ActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid) 
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Author,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Added;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(book);

        }
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            //var a = db.Purchases.FirstOrDefault(c => c.BookId == id);
            //ViewBag.det = a;
            ViewBag.Book = book;
            return View(book);
        }
        [HttpPost]
        public ActionResult BookSearch(string name)
        {
            var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
            if (allbooks.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allbooks);
        }
        [HttpPost]
        public String Find(string name)
        {

            return name;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}