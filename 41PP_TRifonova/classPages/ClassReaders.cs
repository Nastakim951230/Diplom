using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _41PP_TRifonova
{
    public partial class Reader

    {
        public string Nomer
        {
            get
            {
                return "Номер читательского билета: " + LibraryCardNumber;
            }
        }

        public string fio
        {
            get
            {
                return "ФИО: " + Surname+" "+Name+" "+Otshestvo;
            }
        }

        public string books
        {
            get
            {
                List<Application> applications = BD.bD.Application.Where(x => x.IDReader == LibraryCardNumber).ToList();

                string application = "";
                int nomer = 0;
                if (applications.Count > 0)
                {
                    foreach (Application books in applications)
                    {
                        List<BooksAndAuthors> booksAndAuthors = BD.bD.BooksAndAuthors.Where(x => x.BookID == books.IDBook).ToList();

                        string avtors = "";
                        if (booksAndAuthors.Count > 0)
                        {
                            foreach (BooksAndAuthors book in booksAndAuthors)
                            {
                                avtors += book.Authors.NameAuthor + " " + book.Authors.SurnameAuthor + " " + book.Authors.OthestvoAuthor + ", ";
                            }
                            avtors = avtors.Substring(0, avtors.Length - 2);
                        }
                        else
                        {
                           avtors= "автора нет";
                        }


                        nomer++;
                        application += "№" + nomer+"\n Название: "+books.Books.Nazvanie + "\n Автор: " + avtors + "\n Количество книг: " + books.countBooks + "шт.\n\n";
                    }
                    return application.Substring(0, application.Length - 2);
                }
                else
                {
                    return "заявки нет";
                }
            }
        }

        public SolidColorBrush brush
        {
            get
            {
                var brush = new BrushConverter();
                DateTime date = ReissuanceDate;
                date = date.AddYears(1);
                if (date<=DateTime.Today)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#CCE9F5");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFFFFF");
                }
            }
        }
        
        public string visib
        {
            get
            {
                int nomer = 1;
                List<IssueOrReturn> issues = BD.bD.IssueOrReturn.Where(x => x.IDReader == LibraryCardNumber).ToList();
                for (int i = 0; i < issues.Count; i++)
                {
                    if (issues[i].ReturnDate < DateTime.Today)
                    {
                        nomer = 0;
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
        public string visibDelet
        {
            get
            {
                int nomer = 1;
                List<IssueOrReturn> issues = BD.bD.IssueOrReturn.Where(x => x.IDReader == LibraryCardNumber).ToList();
                for (int i = 0; i < issues.Count; i++)
                {
                    if (issues[i].ReturnDate < DateTime.Today)
                    {
                        nomer = 0;
                    }
                }
                if (nomer == 0)
                {
                    return "Visible";
                }
                else
                {
                   
                    return "Collapsed";
                }
            }
        }

        public SolidColorBrush must
        {
            get
            {
                var brush = new BrushConverter();
                int nomer=1;
                List<IssueOrReturn> issues = BD.bD.IssueOrReturn.Where(x=>x.IDReader==LibraryCardNumber).ToList();
                for(int i=0;i<issues.Count;i++)
                {
                   if(issues[i].ReturnDate<DateTime.Today)
                    {
                        nomer = 0;
                    }
                }
                if(nomer==0)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#CCE9F5");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFFFFF");
                }

            }
            }
        public SolidColorBrush pereregistr
        {
            get
            {
                var brush = new BrushConverter();
                DateTime date = ReissuanceDate;
                date = date.AddYears(1);
                if (date <= DateTime.Today)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#fc1703");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#225496");
                }
            }
        }
        public string dateRegistr
        {
            get
            {
                string date = String.Format("{0:dd.MM.yyyy}",DateOfIssue);
                return "Дата регистрации: " + date;
            }
        }

        public string id
        {
            get
            {
                string idReader = Convert.ToString(LibraryCardNumber);
                return "Дата регистрации: " + idReader;
            }
        }
        public string datePeregistr
        {
            get
            {
                string datepereregistr = String.Format("{0:dd.MM.yyyy}", ReissuanceDate);
                return "Дата переригистрации: " + datepereregistr;
            }
        }
    }
}
