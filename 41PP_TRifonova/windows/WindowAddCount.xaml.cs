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
    /// Логика взаимодействия для WindowAddCount.xaml
    /// </summary>
    public partial class WindowAddCount : Window
    {
        Books books;
        Employees employees;
        
        public WindowAddCount(Employees employees,Books books)
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
                BooksAndLibraries libraries= BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDLibrary==employees.LibraryID && x.IDBook==books.BookID);
                libraries.count = Convert.ToInt32(add.Text);
               
                BD.bD.SaveChanges();
                this.Close();
            }
        }
    }
}
