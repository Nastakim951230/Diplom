using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _41PP_TRifonova
{
    public partial class IssueOrReturn
    {
        public static int backroundFon;
        public string fio
        {
            get
            {
                Reader reader = BD.bD.Reader.FirstOrDefault(x => x.LibraryCardNumber == IDReader);
                return "ФИО: " + reader.Surname + " " + reader.Name + " " + reader.Otshestvo;
            }
        }

        public string Nomer
        {
            get
            {
                return "Номер читательского билета: " + IDReader;
            }
        }


        public string avtor
        {
            get
            {

                Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == IDBook);
                string avtors = "";
                if (books != null)
                {
                    List<BooksAndAuthors> booksAndAuthors = BD.bD.BooksAndAuthors.Where(x => x.BookID == books.BookID).ToList();


                    if (booksAndAuthors.Count > 0)
                    {
                        foreach (BooksAndAuthors book in booksAndAuthors)
                        {
                            avtors += " "+book.Authors.NameAuthor + " " + book.Authors.SurnameAuthor + " " + book.Authors.OthestvoAuthor + ", ";
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
                Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == IDBook);
                return " " + books.Nazvanie;
            }
        }
        public string dateOfIssue
        {
            get
            {

                return " " + String.Format("{0:dd.MM.yyyy}", DateOfIssue);
            }
        }

        public string IDnomer
        {
            get
            {
                return IDReader+"";
            }
        }
        public string returnDate
        {
            get
            {

                return " " + String.Format("{0:dd.MM.yyyy}", ReturnDate);
            }
        }
        public string countbooks
        {
            get
            {

                return " " + countBooks+"шт.";
            }
        }
        public string visib
        {
            get
            {
               
                if (ReturnDate<DateTime.Today)
                {

                    return "Visible";

                }
                else
                {
                    return "Collapsed";

                }
            }
        }
        public string visibBTN
        {
            get
            {

                if (ReturnDate < DateTime.Today)
                {

                    
                    return "Collapsed";

                }
                else
                {
                    return "Visible";

                }
            }
        }
        public SolidColorBrush must
        {
            get
            {
                var brush = new BrushConverter();
                int nomer = 1;
               
                    if (ReturnDate < DateTime.Today)
                    {
                        nomer = 0;
                    }
                
                if (nomer == 0)
                {
                    if (backroundFon == 1)
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#CCE9F5");
                    }
                    else if (backroundFon == 2)
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFEACCF5");
                    }
                    else
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFCCDFF5");
                    }
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFFFFF");
                }

            }
        }

        public string fines
        {
            get
            {


                if (ReturnDate < DateTime.Today)
                {
                    DateTime date = ReturnDate;
                    TimeSpan x=DateTime.Today - date;
                    int day= Convert.ToInt32( ((int)x.TotalDays).ToString());
                    int Fines = day * 10;
                    return " " +Fines+ " руб.";
                }
                else
                {
                    return " 0 руб.";
                }

              
            }
        }

    }

}

