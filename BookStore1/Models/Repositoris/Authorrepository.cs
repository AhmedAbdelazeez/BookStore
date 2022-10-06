using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore1.Models.Repositoris
{
    public class authorRepusetry : IBookRepositry<Author>
    {
        IList<Author> authors;

        public authorRepusetry()
        {
            authors = new List<Author>
            {
                new Author{id = 1, FullName="ahmed abelazeez"},
                new Author{id = 2, FullName="reem abelazeez"},
                new Author{id = 3, FullName="walled abelazeez"},

            };
        }


        public void Add(Author entity)
        {
            entity.id=authors.Max(a=>a.id)+1;

            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);

            authors.Remove(author);

            
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(c => c.id == id);

            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
          
            author.FullName=newauthor.FullName;

           
            
        }
    }
}
