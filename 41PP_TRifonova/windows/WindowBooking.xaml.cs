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
using System.Windows.Shapes;

namespace _41PP_TRifonova
{
    /// <summary>
    /// Логика взаимодействия для WindowBooking.xaml
    /// </summary>
    public partial class WindowBooking : Window
    {
        Employees employees;
        Books books;
        int index;
        public WindowBooking(Employees employees, Books books)
        {
            InitializeComponent();
            BD.bD = new BaseBD();
            this.employees = employees;
            this.books = books;
            cbLibrary.Items.Add("Не выбрана библиотека");
            List<Libraries> libraries=BD.bD.Libraries.ToList();
            List<BooksAndLibraries> booksAndLibraries=BD.bD.BooksAndLibraries.ToList();
            
            for(int i=0;i<libraries.Count;i++)
            {
                if (libraries[i].LibraryID != employees.LibraryID)
                {
                    List<BooksAndLibraries> librariesAndBooks = new List<BooksAndLibraries>();
                    librariesAndBooks = booksAndLibraries.Where(x => x.IDBook == books.BookID && x.IDLibrary == libraries[i].LibraryID).ToList();
                    if (librariesAndBooks.Count > 0)
                    {
                        cbLibrary.Items.Add(libraries[i].library);
                    }
                }
                
            }
            cbLibrary.SelectedIndex = 0;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(cbLibrary.SelectedIndex == 0 || reader.Text==""|| count.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int id=Convert.ToInt32(reader.Text);
                int coint = 0;
                Reader readers=BD.bD.Reader.FirstOrDefault(x=>x.IDLibrary==employees.LibraryID && x.LibraryCardNumber==id);
                if(readers!=null)
                {
                    List<IssueOrReturn> issues=BD.bD.IssueOrReturn.Where(x=>x.IDReader==readers.LibraryCardNumber).ToList();
                    for(int i=0;i<issues.Count;i++)
                    {
                        if(issues[i].ReturnDate < DateTime.Today)
                        {
                            coint = 1;
                        }
                    }
                    if(coint==1)
                    {
                        MessageBox.Show("У читателя есть долги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Booking booking=new Booking();
                        booking.ReaderID = Convert.ToInt32(reader.Text);
                        booking.BookID = books.BookID;
                        booking.FromWhere = employees.LibraryID;
                        booking.ToWhere = index;
                        booking.countBooks = Convert.ToInt32(count.Text);
                        booking.look = 0;
                        BD.bD.Booking.Add(booking);
                        BD.bD.SaveChanges();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Такого читателя нету", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private void reader_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void cbLibrary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLibrary.SelectedIndex > 0)
            {
                List<BooksAndLibraries> booksAndLibraries = BD.bD.BooksAndLibraries.ToList();
                List<Libraries> libraries = BD.bD.Libraries.ToList();
                List<Libraries> librari = new List<Libraries>();
                for (int i = 0; i < libraries.Count; i++)
                {
                    if (libraries[i].LibraryID != employees.LibraryID)
                    {
                        List<BooksAndLibraries> librariesAndBooks = new List<BooksAndLibraries>();
                        librariesAndBooks = booksAndLibraries.Where(x => x.IDBook == books.BookID && x.IDLibrary == libraries[i].LibraryID).ToList();
                        if (librariesAndBooks.Count > 0)
                        {
                            librari.Add(libraries[i]);
                        }
                    }
                }
                for (int i = 0; i < librari.Count+1; i++)
                {
                    if (i == cbLibrary.SelectedIndex)
                    {
                       
                      index = librari[i-1].LibraryID;
                      
                    }
                }
            }
        }
    }
}
