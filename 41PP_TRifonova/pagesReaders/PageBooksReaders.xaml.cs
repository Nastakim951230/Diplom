using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _41PP_TRifonova
{
    /// <summary>
    /// Логика взаимодействия для PageBooksReaders.xaml
    /// </summary>
    public partial class PageBooksReaders : Page
    {
        Books books;
        public PageBooksReaders(Books books)
        {
            InitializeComponent();
            this.books = books;
            textDescription.Text = books.Description;
            if (books.Photo != null)
            {

            }
            textNazvanie.Text = books.Nazvanie;
            List<BooksAndAuthors> booksAndAuthors = BD.bD.BooksAndAuthors.Where(x => x.BookID == books.BookID).ToList();

            string avtors = "";
            if (booksAndAuthors.Count > 0)
            {
                foreach (BooksAndAuthors book in booksAndAuthors)
                {
                    avtors += book.Authors.NameAuthor + " " + book.Authors.SurnameAuthor + " " + book.Authors.OthestvoAuthor + ", ";
                }
                textAvtor.Text = avtors.Substring(0, avtors.Length - 2);
            }
            PublishingHouse publishingHouse = BD.bD.PublishingHouse.FirstOrDefault(x => x.PublishingHouseID == books.IDPublishingHouse);
            textIzdatelstvo.Text = publishingHouse.Nazvanie;
            textYear.Text = Convert.ToString(books.Year);
            tetxISBN.Text = books.ISBN;
            textPages.Text = Convert.ToString(books.Pages);

            List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDBook == books.BookID).ToList();
            string ganre = "";
            if (booksAndGanres.Count > 0)
            {
                foreach (BooksAndGanres ganres in booksAndGanres)
                {
                    ganre += ganres.Genres.Ganre + ", ";
                }
                textGanre.Text = ganre.Substring(0, ganre.Length - 2);
            }
            BooksAndGanres andGanres = BD.bD.BooksAndGanres.FirstOrDefault(x => x.IDBook == books.BookID);
            Catalogs catalogs = BD.bD.Catalogs.FirstOrDefault(x => x.CatalogID == andGanres.IDCatalog);
            catalogBook.Text = catalogs.catalog + ">";
            SubDirectory subDirectory = BD.bD.SubDirectory.FirstOrDefault(x => x.SubDirectoryID == andGanres.IDUnderTheDirectory);
            pogCatalog.Text = subDirectory.SubDirectory1;
            
           
            if (books.RestrictionsID != null)
            {
                AgeRestrictions restrictions = BD.bD.AgeRestrictions.FirstOrDefault(x => x.AgeRestrictionsID == books.RestrictionsID);
                restristons.Visibility = Visibility.Visible;
                textRestristons.Visibility = Visibility.Visible;
                textRestristons.Text = restrictions.Restrictions;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.perReader.Navigate(new PageReaders());
        }
        private void basket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
