using BookStore1.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore1.viewmodels
{
    public class bookAuthorview
    {
        public int bookid { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(5)]
       
        public String title { get; set; }
        [Required]
       
        
        [StringLength(120,MinimumLength =5)]
        
        public string description { get; set; }
        
        public int Authorid { get; set; }
       

        public List<Author> Authors { get; set; }

        public string Imageurl { get; set; }
        public IFormFile File { get; set; }


    }
}
