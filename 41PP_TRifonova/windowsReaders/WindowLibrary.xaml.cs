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
    /// Логика взаимодействия для WindowLibrary.xaml
    /// </summary>
    public partial class WindowLibrary : Window
    {
        public WindowLibrary()
        {
            InitializeComponent();
            BD.bD = new BaseBD();
            CBLibrary.Items.Add("Библиотека не выбрана");
            List<Libraries> libraries=BD.bD.Libraries.ToList();
            for(int i=0;i<libraries.Count;i++)
            {
                CBLibrary.Items.Add(libraries[i].library);
            }
            CBLibrary.SelectedIndex = 0;
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if(CBLibrary.SelectedIndex == 0)
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Libraries libraries=BD.bD.Libraries.FirstOrDefault(x=>x.LibraryID==CBLibrary.SelectedIndex);
                PageReaders.libraries=libraries;
                WindowReader windowReader = new WindowReader();
                this.Close();
                windowReader.ShowDialog();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
