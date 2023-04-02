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

        }

        private void oformit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
