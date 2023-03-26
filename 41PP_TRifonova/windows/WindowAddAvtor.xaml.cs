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
    /// Логика взаимодействия для WindowAddAvtor.xaml
    /// </summary>
    public partial class WindowAddAvtor : Window
    {
        public WindowAddAvtor()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (surname.Text == "" || name.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Authors authors=new Authors();
                authors.SurnameAuthor=surname.Text;
                authors.NameAuthor=name.Text;
                if(othestvo.Text=="")
                {
                    authors.OthestvoAuthor = null;
                }
                else
                {
                    authors.OthestvoAuthor = othestvo.Text;
                }
               
                BD.bD.Authors.Add(authors);
                BD.bD.SaveChanges();
                this.Close();
            }
        }
    }
}
