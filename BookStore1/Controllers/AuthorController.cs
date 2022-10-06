using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Models;
using BookStore1.Models.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookRepositry<Author> authorRepusetry;

        public AuthorController(IBookRepositry<Author> authorRepusetry)
        {
            this.authorRepusetry = authorRepusetry;
        }

        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = authorRepusetry.List();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepusetry.Find(id);

               
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            var author = new Author();
            return View(author);
        }

        // POST: AuthorController/Create
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepusetry.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepusetry.Find(id);
           
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {

            try
            {
                authorRepusetry.Update(id, author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepusetry.Find(id);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepusetry.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
