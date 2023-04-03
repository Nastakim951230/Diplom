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
                int index = Convert.ToInt32(CBPodCatalog.SelectedValuePath);
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
                int index = Convert.ToInt32(CBGanre.SelectedValuePath);
                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDGanre == index).ToList();
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

            if (CBCatalog.SelectedIndex == 0 && search.Text == "" && inStock.IsChecked == false)
            {

                books = BD.bD.Books.ToList();
                CBPodCatalog.SelectedIndex = 0;
                CBGanre.SelectedIndex = 0;
            }

            if (books.Count > 0)
            {
                listBook.ItemsSource = books;
                TextCountBook.Text = "Количество книг: " + books.Count;
            }
            else
            {
                CBGanre.SelectedIndex = 0;
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
                subDirectories = subDirectories.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();


                for (int i = 0; i < subDirectories.Count; i++)
                {

                    CBPodCatalog.Items.Add(subDirectories[i].SubDirectory1);

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
                List<SubDirectory> subDirectorie = BD.bD.SubDirectory.ToList();
                subDirectorie = subDirectorie.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                if (CBPodCatalog.SelectedIndex > 0)
                {
                    for (int i = 0; i < subDirectorie.Count; i++)
                    {
                        if (CBPodCatalog.SelectedIndex == i + 1)
                        {
                            string id = Convert.ToString(subDirectorie[i].SubDirectoryID);
                            CBPodCatalog.SelectedValuePath = id;
                        }

                    }
                    int index = Convert.ToInt32(CBPodCatalog.SelectedValuePath);
                    genres = genres.Where(x => x.DirectoryAndSubDirectoryID == index).ToList();
                    if (genres.Count > 1)
                    {
                        for (int i = 0; i < genres.Count; i++)
                        {

                            CBGanre.Items.Add(genres[i].Ganre);
                        }

                        CBGanre.SelectedIndex = 0;
                        CBGanre.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CBGanre.Visibility = Visibility.Collapsed;
                    }
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
                int index = Convert.ToInt32(CBPodCatalog.SelectedValuePath);
                List<Genres> genres = BD.bD.Genres.Where(x => x.DirectoryAndSubDirectoryID == index).ToList();
                for (int i = 0; i < genres.Count; i++)
                {
                    if (CBGanre.SelectedIndex == i + 1)
                    {
                        CBGanre.SelectedValuePath = Convert.ToString(genres[i].GenreID);
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
    }
}
