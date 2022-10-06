using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore1.Models.Repositoris
{
    public class BookRepositry : IBookRepositry<Book>
    {
        List<Book> books;
        public BookRepositry()
        {
            books = new List<Book>()
            {
                new Book
                {
                    id=1,Title="c# programing",
                    Description="no description ",
                    Imageurl="c#.png",
                    Author=new Author{id=2}
                },
                new Book
                {
                    id=2,Title="python good language",
                    Description="good description",
                    Imageurl="payton.png",
                    Author=new Author()
                },
                new Book
                {
                    id=3,Title="java programing " ,
                    Description="ex description",
                    Imageurl="java.png",
                    Author=new Author()
                },
            };

        }

        public void Add(Book entity)
        {
            entity.id=books.Max(x=>x.id) +1 ;
            
            books.Add(entity);  
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
            
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.id == id);

            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }

        public void Update( int id, Book newbook )
        {
            var book =Find(id);

            book.Title = newbook.Title;  

            book.Description= newbook.Description; 
            
            book.Author=newbook.Author;
            book.Imageurl=newbook.Imageurl;

        }
    }
}
