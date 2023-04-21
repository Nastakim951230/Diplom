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
    /// Логика взаимодействия для PageReaders.xaml
    /// </summary>
    public partial class PageReaders : Page
    {
        public static Reader reader;
        public static Libraries libraries;
        int index;
        int indexGanre;

        List<ClassBooksBasket> basket = new List<ClassBooksBasket>();
        public static List<ClassBooksBasket> Basket;
        public PageReaders()
        {
            InitializeComponent();
            if (Basket != null)
            {
                if (Basket.Count != 0)
                {
                    basket = Basket;
                    BasketButton.Visibility = Visibility.Visible;
                }
            }

            //Заполнение списка каталог
            CBCatalog.Items.Add("Все каталоги");
            List<Catalogs> catalogs = BD.bD.Catalogs.ToList();
            for (int i = 0; i < catalogs.Count; i++)
            {
                CBCatalog.Items.Add(catalogs[i].catalog);
            }
            CBCatalog.SelectedIndex = 0;

            //Заполнение списка подкаталог
            CBPodCatalog.Items.Add("Все подкаталоги");
            List<SubDirectory> subDirectories = BD.bD.SubDirectory.ToList();
            for (int i = 0; i < subDirectories.Count; i++)
            {
                CBPodCatalog.Items.Add(subDirectories[i].SubDirectory1);
            }
            CBPodCatalog.SelectedIndex = 0;

            //Заполнение списка жанр
            CBGanre.Items.Add("Все жанры");
            List<Genres> genres = BD.bD.Genres.ToList();
            for (int i = 0; i < genres.Count; i++)
            {
                CBGanre.Items.Add(genres[i].Ganre);
            }
            CBGanre.SelectedIndex = 0;

            listBook.ItemsSource = BD.bD.Books.ToList();
            TextCountBook.Text = "Количество книг: " + BD.bD.Books.ToList().Count;
            if(reader != null)
            {
                FIO.Visibility=Visibility.Visible;
                entrance.Visibility=Visibility.Collapsed;
                exit.Visibility=Visibility.Visible;

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
                FIO.Text = reader.Surname + " " + name + ". " + othestvo + ".";
            }
          
           
        }


        /// <summary>
        /// Поиск, фильтрация
        /// </summary>
        void filter()
        {
            List<Books> books = BD.bD.Books.ToList();
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                books = books.Where(x => x.Nazvanie.ToLower().Contains(search.Text.ToLower())).ToList();
            }

            if (inStock.IsChecked == true)
            {
                List<Books> booksLibrary = new List<Books>();
                List<Books> book = new List<Books>();
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.Where(x => x.IDLibrary == libraries.LibraryID).ToList();
                for (int i = 0; i < booksAndLibraries.Count; i++)
                {
                    book = books.Where(x => x.BookID == booksAndLibraries[i].IDBook).ToList();
                    for (int j = 0; j < book.Count; j++)
                    {
                        booksLibrary.Add(book[j]);
                    }
                }
                books = booksLibrary;
            }

            if (CBCatalog.SelectedIndex > 0)
            {
                List<Books> bookGanre = new List<Books>();
                List<Books> book = new List<Books>();
                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDCatalog == CBCatalog.SelectedIndex).ToList();
                for (int i = 0; i < booksAndGanres.Count; i++)
                {
                    book = books.Where(x => x.BookID == booksAndGanres[i].IDBook).ToList();
                    for (int j = 0; j < book.Count; j++)
                    {
                        bookGanre.Add(book[j]);
                    }
                }
                books = bookGanre;
            }
            if (CBPodCatalog.SelectedIndex > 0)
            {

                List<Books> bookGanre = new List<Books>();
                List<Books> book = new List<Books>();

                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDUnderTheDirectory == index).ToList();
                for (int i = 0; i < booksAndGanres.Count; i++)
                {
                    book = books.Where(x => x.BookID == booksAndGanres[i].IDBook).ToList();
                    for (int j = 0; j < book.Count; j++)
                    {
                        bookGanre.Add(book[j]);
                    }
                }
                books = bookGanre;

            }

            if (CBGanre.SelectedIndex > 0)
            {

                List<Books> bookGanre = new List<Books>();
                List<Books> book = new List<Books>();

                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDGanre == indexGanre).ToList();
                for (int i = 0; i < booksAndGanres.Count; i++)
                {
                    book = books.Where(x => x.BookID == booksAndGanres[i].IDBook).ToList();
                    for (int j = 0; j < book.Count; j++)
                    {
                        bookGanre.Add(book[j]);
                    }
                }
                books = bookGanre;

            }

            if (books.Count > 0)
            {
                listBook.ItemsSource = books;
                TextCountBook.Text = "Количество книг: " + books.Count;
            }
            else
            {
                if (CBGanre.SelectedIndex > 0)
                {
                    CBGanre.SelectedIndex = 0;
                }
                else if (CBPodCatalog.SelectedIndex > 0)
                {
                    CBPodCatalog.SelectedIndex = 0;
                    CBGanre.SelectedIndex = 0;
                }
                else if (CBCatalog.SelectedIndex > 0)
                {
                    CBCatalog.SelectedIndex = 0;
                    CBPodCatalog.SelectedIndex = 0;
                    CBGanre.SelectedIndex = 0;
                }
                search.Text = "";
                MessageBox.Show("Данной книги не существуеь", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void CBCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBCatalog.SelectedIndex > 0)
            {
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.ToList();

                for (int i = 0; i < subDirectories.Count; i++)
                {
                    CBPodCatalog.Items.Remove(subDirectories[i].SubDirectory1);
                }
                List<SubDirectory> directories = BD.bD.SubDirectory.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();

                for (int i = 0; i < directories.Count; i++)
                {

                    CBPodCatalog.Items.Add(directories[i].SubDirectory1);

                }

                CBPodCatalog.SelectedIndex = 0;
                CBPodCatalog.Visibility = Visibility.Visible;

            }
            else
            {
                CBPodCatalog.Visibility = Visibility.Collapsed;
                CBGanre.Visibility = Visibility.Collapsed;
            }
            filter();
        }

        private void CBPodCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBPodCatalog.SelectedIndex > 0)
            {

                List<Genres> genres = BD.bD.Genres.ToList();

                for (int i = 0; i < genres.Count; i++)
                {
                    CBGanre.Items.Remove(genres[i].Ganre);
                }
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                for (int i = 0; i < subDirectories.Count + 1; i++)
                {
                    if (i == CBPodCatalog.SelectedIndex)
                    {
                        index = subDirectories[i - 1].SubDirectoryID;
                    }
                }
                List<Genres> genre = BD.bD.Genres.Where(x => x.DirectoryAndSubDirectoryID == index).ToList();
                if (genre.Count > 0)
                {
                    for (var i = 0; i < genre.Count; i++)
                    {
                        CBGanre.Items.Add(genre[i].Ganre);
                    }
                    CBGanre.SelectedIndex = 0;
                    CBGanre.Visibility = Visibility.Visible;
                }
                else
                {
                    CBGanre.Visibility = Visibility.Collapsed;
                }

            }
            else
            {
                CBPodCatalog.SelectedIndex = 0;
                CBGanre.Visibility = Visibility.Collapsed;
            }
            filter();
        }

        private void CBGanre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBGanre.SelectedIndex > 0)
            {

                List<Genres> genre = BD.bD.Genres.Where(x => x.DirectoryAndSubDirectoryID == index).ToList();
                for (int i = 0; i < genre.Count + 1; i++)
                {
                    if (i == CBGanre.SelectedIndex)
                    {
                        indexGanre = genre[i - 1].GenreID;
                    }
                }

            }
            filter();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            PageReaders.reader = null;
            PageReaders.Basket=null;
            FrameNavigate.perReader.Navigate(new PageReaders());
        }

        private void entrance_Click(object sender, RoutedEventArgs e)
        {
            WindowAvtorizatsiaReaders windowAvtorizatsiaReaders = new WindowAvtorizatsiaReaders(libraries);
            windowAvtorizatsiaReaders.ShowDialog();
            FrameNavigate.perReader.Navigate(new PageReaders());
        }

        private void inStock_Checked(object sender, RoutedEventArgs e)
        {
            filter();
        }

        private void lookBooks_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 

            Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == index);
            PageBooksReaders.Basket = basket;
            FrameNavigate.perReader.Navigate(new PageBooksReaders(books,libraries, reader));
        }

        private void BasketButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.perReader.Navigate(new PageBasket(reader, basket));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть программу?", "Вопрос",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
           

        }
    }
}
