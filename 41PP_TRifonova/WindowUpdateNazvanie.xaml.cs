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
    /// Логика взаимодействия для WindowUpdateNazvanie.xaml
    /// </summary>
    public partial class WindowUpdateNazvanie : Window
    {
        Books books;
        public WindowUpdateNazvanie(Books books)
        {
            InitializeComponent();
            this.books = books;
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

                books.Nazvanie = add.Text;

                BD.bD.SaveChanges();
                this.Close();
            }
        }
    }
}
