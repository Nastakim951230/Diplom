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
        public static int idLibrary;
        
        public  string btn_update
        {
            get
            {
               
                int id = 0;
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.Where(x => x.IDBook == BookID).ToList();
                for (int i = 0; i < booksAndLibraries.Count; i++)
                {
                    if (booksAndLibraries[i].IDLibrary == idLibrary)
                    {
                        id = 1;
                    }
                }
                if (id == 0)
                {
                    return "Collapsed";
                }
                else
                {
                   
                    return "Visible";
                }

               
            }
          
        }
        public string btn_booking
        {
            get
            {

                int id = 0;
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.Where(x => x.IDBook == BookID).ToList();
                if (booksAndLibraries.Count > 1)
                {
                    for (int i = 0; i < booksAndLibraries.Count; i++)
                    {

                        if (booksAndLibraries[i].IDLibrary == idLibrary && booksAndLibraries[i].count != 0)
                        {
                            id = 1;
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < booksAndLibraries.Count; i++)
                    {

                        if (booksAndLibraries[i].IDLibrary == idLibrary && booksAndLibraries[i].count == 0)
                        {
                            id = 1;
                        }
                        if (booksAndLibraries[i].IDLibrary == idLibrary && booksAndLibraries[i].count != 0)
                        {
                            id = 1;
                        }

                    }
                }
                if (id == 0)
                {
                    return "Visible";

                }
                else
                {
                    return "Collapsed";
                   
                }


            }

        }
        public string btn_bringing
        {
            get
            {

                int id = 0;
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.Where(x => x.IDBook == BookID).ToList();
                for (int i = 0; i < booksAndLibraries.Count; i++)
                {
                    if (booksAndLibraries[i].IDLibrary == idLibrary)
                    {
                        id = 1;
                    }
                }
                if (id == 0)
                {
                    return "Visible";

                }
                else
                {
                    return "Collapsed";
                   
                   

                }


            }

        }
    }
}
