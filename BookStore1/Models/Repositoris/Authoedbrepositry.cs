using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore1.Models.Repositoris
{
    public class Authoedbrepositry : IBookRepositry<Author>
    {

        BookstoreDbcontext db;
    public Authoedbrepositry(BookstoreDbcontext _db)
    {
            this.db = _db;
      
    }


    public void Add(Author entity)
    {
     //   entity.id = db.Authors.Max(a => a.id) + 1;

        db.Authors.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
    {
        var author = Find(id);

            db.Authors.Remove(author);
            db.SaveChanges();



        }

        public Author Find(int id)
    {
        var author = db.Authors.SingleOrDefault(c => c.id == id);

        return author;
    }

    public IList<Author> List()
    {
        return db.Authors.ToList();
    }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newauthor)
    {
            db.Update(newauthor);
            db.SaveChanges();



    }
}
}