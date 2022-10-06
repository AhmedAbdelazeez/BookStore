using BookStore1.Models;
using BookStore1.Models.Repositoris;
using BookStore1.viewmodels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepositry<Book> bookRepositry;
        private readonly IBookRepositry<Author> authorrepositry;
        private readonly IHostingEnvironment hosting;


        // GET: BookController

        public BookController(IBookRepositry<Book> bookRepositry,
            IBookRepositry<Author> authorrepositry,
            IHostingEnvironment hosting)
        {
            this.bookRepositry = bookRepositry;
            this.authorrepositry = authorrepositry;
            this.hosting = hosting;
        }
      
        
        public ActionResult Index()
        {
            var books = bookRepositry.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books=bookRepositry.Find(id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new bookAuthorview
            {
                Authors = fillselectlist()

            };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bookAuthorview model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = string.Empty;

                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "Uploads");

                        filename = model.File.FileName;

                        string fullpath=Path.Combine(uploads, filename);

                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }

                    if (model.Authorid == -1)
                    {
                        ViewBag.Message = "please select an author from list ";


                       
                        return View(Getallauthers());
                    }

                    var author = authorrepositry.Find(model.Authorid);


                    Book book = new Book
                    {
                        id = model.bookid,
                        Title = model.title,
                        Description = model.description,
                        Author = author,
                        Imageurl = filename,
                        
                    };

                    bookRepositry.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
           
            ModelState.AddModelError("", "you have fil all the requerd fields");
            return View(Getallauthers());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var book=bookRepositry.Find(id);

            var authorid = book.Author == null ? book.Author.id = 0 : book.Author.id;


            var viewmodel = new bookAuthorview
            {
                bookid = book.id,
                title = book.Title,
                description = book.Description,
                Authorid = authorid,
                Authors= authorrepositry.List().ToList(),
                Imageurl=book.Imageurl
                

            };

            return View(viewmodel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( bookAuthorview viewmodel)
        {


            string filename = string.Empty;

            if (viewmodel.File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");

                filename = viewmodel.File.FileName;

                string fullpath = Path.Combine(uploads, filename);
                
                string old = viewmodel.Imageurl;
               
                string fullold=Path.Combine(uploads, filename);
            
                
                if(fullpath != old)
                { 
                
                    System.IO.File.Delete(fullold);
                    viewmodel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                
                }
            }



            var author = authorrepositry.Find(viewmodel.Authorid);
            Book book = new Book
            {
                id=viewmodel.bookid,
                Title = viewmodel.title,
                Description = viewmodel.description,
                Author = author,
                Imageurl=filename
            };

            bookRepositry.Update(viewmodel.bookid,book);

            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book= bookRepositry.Find(id);

            return View(book); ;
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {
                bookRepositry.Delete(id);   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    
        List<Author> fillselectlist()
        {
            var authors = authorrepositry.List().ToList();

            authors.Insert(0, new Author { id = -1, FullName = "---please select author" });

            return authors;

        }
    
        bookAuthorview Getallauthers()
        {
            var vmodel = new bookAuthorview
            {
                Authors = fillselectlist()

            };
        
            return vmodel;
        }
        public ActionResult Search( string term)
        {
            var resualt =bookRepositry.Search(term);
            return View("Index", resualt); 

        }
    }

}
