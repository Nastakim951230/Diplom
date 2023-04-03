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
    /// Логика взаимодействия для PageBasket.xaml
    /// </summary>
    public partial class PageBasket : Page
    {
        List<ClassBooksBasket> baskets;
        Reader reader;
        public PageBasket(Reader reader, List<ClassBooksBasket> baskets)
        {
            InitializeComponent();
            this.baskets=baskets;
            this.reader=reader;
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
            FIO.Text = "Читатель: " + reader.Surname + " " + name + ". " + othestvo + ".";
            
                listBooks.ItemsSource = baskets;
            
          
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            PageReaders.Basket = baskets;
            FrameNavigate.perReader.Navigate(new PageReaders());
        }

        private void oformit_Click(object sender, RoutedEventArgs e)
        {
            List<Application> booksApplicatio= BD.bD.Application.ToList();
            for (int i = 0; i < baskets.Count; i++)
            {
                Application applications = booksApplicatio.FirstOrDefault(x => x.IDReader == reader.LibraryCardNumber && x.IDBook == baskets[i].books.BookID);
                if (applications == null)
                {
                    Application application = new Application();
                    application.IDBook = baskets[i].books.BookID;
                    application.IDReader = reader.LibraryCardNumber;
                    application.IDLibrary = reader.IDLibrary;
                    application.countBooks = baskets[i].couint;
                    BD.bD.Application.Add(application);
                    BooksAndLibraries booksAndLibraries = BD.bD.BooksAndLibraries.FirstOrDefault(x => x.IDLibrary == application.IDLibrary && x.IDBook == application.IDBook);
                    int kolvo = booksAndLibraries.count;
                    kolvo = kolvo - application.countBooks;
                    booksAndLibraries.count = kolvo;
                }
                else
                {
                    int kolvo = applications.countBooks;
                    applications.countBooks = kolvo+baskets[i].couint;
                    BooksAndLibraries booksAndLibraries = BD.bD.BooksAndLibraries.FirstOrDefault(x => x.IDLibrary == applications.IDLibrary && x.IDBook == applications.IDBook);
                    int kol = booksAndLibraries.count;
                    kol = kol - baskets[i].couint;
                    booksAndLibraries.count = kol;

                }
                
            }
            BD.bD.SaveChanges();
            PageReaders.Basket = null;
            FrameNavigate.perReader.Navigate(new PageReaders());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BooksAndLibraries books=new BooksAndLibraries();
            ClassBooksBasket booksBasket = new ClassBooksBasket();
            TextBox id = (TextBox)sender;
            int index = Convert.ToInt32(id.Uid);
            booksBasket = baskets.FirstOrDefault(x => x.books.BookID == index);
            books=BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDLibrary==reader.IDLibrary && x.IDBook==index);
            if (id.Text != "")
            {
                if (books.count - Convert.ToInt32(id.Text) >= 0)
                {
                    if(Convert.ToInt32(id.Text)==0)
                    {
                        baskets.Remove(booksBasket);
                        if (baskets.Count > 0)
                        {
                            FrameNavigate.perReader.Navigate(new PageBasket(reader, baskets));
                        }
                        else
                        {
                            PageReaders.Basket = null;
                            FrameNavigate.perReader.Navigate(new PageReaders());
                        }
                    }
                }
                else
                {
                    id.Text = Convert.ToString(books.count);
                    MessageBox.Show("Максимальное количество книг в библиотеке: " + books.count + "", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            ClassBooksBasket booksBasket = new ClassBooksBasket();
            Button id=(Button)sender;
            int index = Convert.ToInt32(id.Uid);
            booksBasket = baskets.FirstOrDefault(x => x.books.BookID == index);
            baskets.Remove(booksBasket);
            if (baskets.Count > 0)
            {
                FrameNavigate.perReader.Navigate(new PageBasket(reader, baskets));
            }
            else
            {
                PageReaders.Basket = null;
                FrameNavigate.perReader.Navigate(new PageReaders());
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
