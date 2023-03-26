using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41PP_TRifonova
{
    public partial class Books
    {

        public string avtors
        {
            get
            {
                BooksAndAuthors booksAndAuthors=BD.bD.BooksAndAuthors.FirstOrDefault(x=>x.BookID==BookID);
                Authors authors=BD.bD.Authors.FirstOrDefault(x=>x.AuthorID==booksAndAuthors.AuthorID);
                return authors.NameAuthor + " " + authors.SurnameAuthor;
            }
        }

        
    }
}
