using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore1.Models.Repositoris
{
    public class Bookdbreposetry : IBookRepositry<Book>
    {
        BookstoreDbcontext db;
        public Bookdbreposetry(BookstoreDbcontext _db)
        {
            this.db = _db;

        }
      
       

        public void Add(Book entity)
        {
         

            db.books.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.books.Remove(book);
            db.SaveChanges();


        }

        public Book Find(int id)
        {
            var book = db.books.Include(x => x.Author).SingleOrDefault(b => b.id == id);

            return book;
        }

        public IList<Book> List()
        {
            return db.books.Include(x=>x.Author).ToList();
        }

        public void Update(int id, Book newbook)
        {
            db.Update(newbook);
            db.SaveChanges();

        }

        public List<Book> Search(string term)
        {
            var result = db.books.Include(a => a.Author)
            .Where(b => b.Title.Contains(term)
               
               || b.Description.Contains(term)
               || b.Author.FullName.Contains(term)).ToList();


            return result;
        }
    }
}
