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
    /// Логика взаимодействия для WindowAvtorizatsiaReaders.xaml
    /// </summary>
    public partial class WindowAvtorizatsiaReaders : Window
    {
        Libraries libraries;
        public WindowAvtorizatsiaReaders(Libraries libraries)
        {
            InitializeComponent();
            BD.bD = new BaseBD();
            this.libraries = libraries;
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if (readersAvtorizatsia.Text == "")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int nomer = Convert.ToInt32(readersAvtorizatsia.Text);
               Reader reader=BD.bD.Reader.FirstOrDefault(x=>x.LibraryCardNumber==nomer && x.IDLibrary==libraries.LibraryID);
                if (reader!=null)
                {

                    PageReaders.reader=reader;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не зарегистрирован в данной библиотеки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
               
               
                
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
