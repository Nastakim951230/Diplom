using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _41PP_TRifonova
{
    public partial class Booking
    {

        public string fio
        {
            get
            {
                Reader reader = BD.bD.Reader.FirstOrDefault(x => x.LibraryCardNumber == ReaderID);
                return "ФИО: " + reader.Surname + " " + reader.Name + " " + reader.Otshestvo;
            }
        }
        public string telefon
        {
            get
            {
                Reader reader = BD.bD.Reader.FirstOrDefault(x => x.LibraryCardNumber == ReaderID);
                return "Номер телефона: " + reader.TelefonNumber;
            }
        }

        public string Nomer
        {
            get
            {
                return "Номер читательского билета: " + ReaderID;
            }
        }
        public string avtor
        {
            get
            {

                Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == BookID);
                string avtors = "";
                if (books != null)
                {
                    List<BooksAndAuthors> booksAndAuthors = BD.bD.BooksAndAuthors.Where(x => x.BookID == books.BookID).ToList();


                    if (booksAndAuthors.Count > 0)
                    {
                        foreach (BooksAndAuthors book in booksAndAuthors)
                        {
                            avtors += " " + book.Authors.NameAuthor + " " + book.Authors.SurnameAuthor + " " + book.Authors.OthestvoAuthor + ", ";
                        }
                        avtors = avtors.Substring(0, avtors.Length - 2);
                    }
                    else
                    {
                        avtors = " автора нет";
                    }
                }
                return avtors;

            }
        }
        public string book
        {
            get
            {
                Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == BookID);
                return " " + books.Nazvanie;
            }
        }

        public string countbooks
        {
            get
            {

                return " " + countBooks + "шт.";
            }
        }

        public string toWhere
        {
            get
            {
                Libraries libraries = BD.bD.Libraries.FirstOrDefault(x => x.LibraryID == ToWhere);
                return " " + libraries.library;
            }
        }

        public SolidColorBrush must
        {
            get
            {
                var brush = new BrushConverter();
                int nomer = 1;
                BooksAndLibraries booksAndLibraries=BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDLibrary==FromWhere && x.IDBook==BookID);
                if (booksAndLibraries!=null)
                {
                    if (booksAndLibraries.count > 0)
                    {
                        nomer = 0;
                    }
                }

                if (nomer == 0)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#CCE9F5");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFFFFF");
                }

            }
        }

        public SolidColorBrush text
        {
            get
            {
                var brush = new BrushConverter();
                int nomer = 1;
               List<IssueOrReturn> issueOrReturns = BD.bD.IssueOrReturn.Where(x => x.IDReader == ReaderID).ToList();
                if (issueOrReturns.Count>0)
                {
                    for (int i = 0; i < issueOrReturns.Count; i++)
                    {
                        if (issueOrReturns[i].ReturnDate <DateTime.Today)
                        {
                            nomer = 0;
                        }
                    }
                }

                if (nomer == 0)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#eb0e0e");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#000000");
                }

            }
        }

        public string visib
        {
            get
            {
              
                int nomer = 1;
                List<IssueOrReturn> issueOrReturns = BD.bD.IssueOrReturn.Where(x => x.IDReader == ReaderID).ToList();
                if (issueOrReturns.Count > 0)
                {
                    for (int i = 0; i < issueOrReturns.Count; i++)
                    {
                        if (issueOrReturns[i].ReturnDate < DateTime.Today)
                        {
                            nomer = 0;
                        }
                    }
                }
                if (nomer != 0)
                {
                    BooksAndLibraries booksAndLibraries = BD.bD.BooksAndLibraries.FirstOrDefault(x => x.IDLibrary == FromWhere && x.IDBook == BookID);
                    if (booksAndLibraries != null)
                    {
                        if (booksAndLibraries.count == 0)
                        {
                            nomer = 0;
                        }

                    }
                }

                if (nomer == 0)
                {
                    return "Collapsed";

                }
                else
                {
                    return "Visible";

                }

            }
        }


      

    }
}
