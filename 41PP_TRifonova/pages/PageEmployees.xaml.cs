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
    /// Логика взаимодействия для PageEmployees.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        Employees employees;
        int index;
        int indexGanre;
        public PageEmployees(Employees employees)
        {
            InitializeComponent();
            this.employees = employees;
            Books.idLibrary = employees.LibraryID;
            if (employees.RoleID==1)
            {
                employee.Visibility=Visibility.Visible;  
            }

            string name="";
            string othestvo="";
            for(int i=0; i<employees.Name.Length; i++)
            {
                if(i ==0)
                {
                    name+= employees.Name[i];
                }
            }
            for (int i = 0; i < employees.Otchestvo.Length; i++)
            {
                if (i == 0)
                {
                    othestvo += employees.Otchestvo[i];
                }
            }
            //Вывод ФИО сотрудника
            FIO.Text = employees.Surname + " "+name+". "+othestvo+".";

            //Заполнение списка каталог
            CBCatalog.Items.Add("Все каталоги");
          List<Catalogs> catalogs=BD.bD.Catalogs.ToList();
            for(int i = 0; i < catalogs.Count; i++)
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

            if(inStock.IsChecked==true)
            {
                List<Books> booksLibrary = new List<Books>();
                List<Books> book = new List<Books>();
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.Where(x=>x.IDLibrary==employees.LibraryID).ToList();
                for(int i = 0; i < booksAndLibraries.Count; i++)
                {
                    book=books.Where(x=>x.BookID==booksAndLibraries[i].IDBook).ToList();
                    for(int j=0;j<book.Count;j++)
                    {
                        booksLibrary.Add(book[j]);
                    }
                }
                books=booksLibrary;
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
            if(CBPodCatalog.SelectedIndex > 0)
            {

                List<Books> bookGanre = new List<Books>();
                List<Books> book = new List<Books>();
               
                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x =>x.IDUnderTheDirectory == index).ToList();
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
               if(CBGanre.SelectedIndex>0)
                {
                    CBGanre.SelectedIndex = 0;
                }
              else if (CBPodCatalog.SelectedIndex>0)
                {
                    CBPodCatalog.SelectedIndex = 0;
                    CBGanre.SelectedIndex = 0;
                }
               else if(CBCatalog.SelectedIndex>0)
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
                List<SubDirectory> subDirectories= BD.bD.SubDirectory.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                for(int i = 0; i < subDirectories.Count+1; i++)
                {
                    if(i== CBPodCatalog.SelectedIndex)
                    {
                        index=subDirectories[i-1].SubDirectoryID;
                    }
                }
                List<Genres> genre=BD.bD.Genres.Where(x=>x.DirectoryAndSubDirectoryID==index).ToList();
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
                for(int i=0; i<genre.Count+1;i++)
                {
                    if(i==CBGanre.SelectedIndex)
                    {
                        indexGanre = genre[i - 1].GenreID;
                    }
                }

            }
           
            filter();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
           
            Environment.Exit(0);
        }

        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageAddBook(employees));
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 

            Books books=BD.bD.Books.FirstOrDefault(x=>x.BookID==index);

            FrameNavigate.per.Navigate(new PageBookUpdate(employees,books,0));
        }

        private void booking_Click(object sender, RoutedEventArgs e)
        {
           FrameNavigate.per.Navigate(new PageBooking(employees));
        }

        private void toBook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 

            Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == index);

            FrameNavigate.per.Navigate(new PageBookUpdate(employees, books, 1));
        }

        private void libraruBook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 

            Books books = BD.bD.Books.FirstOrDefault(x => x.BookID == index);
            WindowBringing windowBringing = new WindowBringing(books, employees);
            windowBringing.ShowDialog();
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

      

        private void employee_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageSpisokEmployees(employees));
        }

        private void inStock_Checked(object sender, RoutedEventArgs e)
        {
            filter();
        }

        private void inStock_Unchecked(object sender, RoutedEventArgs e)
        {
            filter();
        }

        private void addReader_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageReader(employees));
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageApplication(employees));
        }

        private void returnBook_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageReturn(employees));
        }
    }
}
