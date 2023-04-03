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
        Libraries library;
        Reader reader;
        int count=0;
        List<ClassBooksBasket> baskets = new List<ClassBooksBasket>();
        public static List<ClassBooksBasket> Basket;
        public PageBooksReaders(Books books, Libraries library,Reader reader)
        {
            InitializeComponent();
            this.books = books;
            this.library = library;
            this.reader = reader;
            if(Basket.Count!=0)
            {
                baskets = Basket;
                ClassBooksBasket classBooks=new ClassBooksBasket();
                classBooks = baskets.FirstOrDefault(x => x.books.BookID == books.BookID);
                if(classBooks==null)
                {
                    count=0;
                }
                else
                {
                    count=classBooks.couint;
                }
                
            }
            if(reader != null)
            {
                FIO.Visibility = Visibility.Visible;
                string name = "";
                string othestvo = "";
                for (int i = 0; i < reader.Name.Length; i++)
                {
                    if (i == 0)
                    {
                        name += reader.Name[i];
                    }
                }
                for (int i = 0; i < reader.Otshestvo.Length; i++)
                {
                    if (i == 0)
                    {
                        othestvo += reader.Otshestvo[i];
                    }
                }
                //Вывод ФИО читателя
                FIO.Text = "Читатель: "+reader.Surname + " " + name + ". " + othestvo + ".";
            }
            textDescription.Text = books.Description;
            if (books.Photo != null)
            {
                BitmapImage img = new BitmapImage(new Uri(books.Photo, UriKind.RelativeOrAbsolute));
                photoBook.Source = img;
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
           
           

            BooksAndLibraries booksAndLibraries =BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDLibrary==library.LibraryID && x.IDBook==books.BookID);
            if(booksAndLibraries!=null)
            {
                if(booksAndLibraries.count!=0)
                {
                    if (booksAndLibraries.count - count != 0)
                    {
                        basket.Visibility = Visibility.Visible;
                        count = booksAndLibraries.count;
                    }
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            PageReaders.Basket = baskets;
            FrameNavigate.perReader.Navigate(new PageReaders());
        }
        private void basket_Click(object sender, RoutedEventArgs e)
        {
            if(reader==null)
            {
                WindowAvtorizatsiaReaders windowAvtorizatsiaReaders = new WindowAvtorizatsiaReaders(library);
                windowAvtorizatsiaReaders.ShowDialog();
                FrameNavigate.perReader.Navigate(new PageReaders());
            }
            else
            {
                bool kolvo = false;
                foreach (ClassBooksBasket productBasket in baskets)
                {
                    if (productBasket.books == books )
                    {
                        productBasket.couint = productBasket.couint += 1;
                        kolvo = true;
                    }
                }
                if (!kolvo)
                {
                    ClassBooksBasket product = new ClassBooksBasket();
                    product.books = books;
                    
                    product.couint = 1;
                    baskets.Add(product);
                }
                count--;
              if(count==0)
                {
                    basket.Visibility = Visibility.Collapsed;
                }
                MessageBox.Show("Книга добавлена в корзину");
                PageReaders.Basket = baskets;
            }
        }
    }
}
