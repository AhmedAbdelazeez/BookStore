using Microsoft.EntityFrameworkCore;


namespace BookStore1.Models
{
    public class BookstoreDbcontext:DbContext
    {
        public BookstoreDbcontext(DbContextOptions<BookstoreDbcontext>options):base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> books { get; set; }


    }
}
 