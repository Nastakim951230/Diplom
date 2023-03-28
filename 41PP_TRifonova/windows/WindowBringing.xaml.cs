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
    /// Логика взаимодействия для WindowBringing.xaml
    /// </summary>
    public partial class WindowBringing : Window
    {
        Books books;
        Employees employees;
        public WindowBringing(Books books, Employees employees)
        {
            InitializeComponent();
            this.books = books;
            this.employees = employees;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (add.Text == "")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                BooksAndLibraries booksAndLibraries=new BooksAndLibraries();
                booksAndLibraries.IDLibrary = employees.LibraryID;
                booksAndLibraries.IDBook = books.BookID;
                booksAndLibraries.count = Convert.ToInt32(add.Text);
                BD.bD.BooksAndLibraries.Add(booksAndLibraries);
                BD.bD.SaveChanges();
                this.Close();
            }
        }

        private void add_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
