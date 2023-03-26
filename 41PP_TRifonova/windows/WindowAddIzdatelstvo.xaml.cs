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
    /// Логика взаимодействия для WindowAddIzdatelstvo.xaml
    /// </summary>
    public partial class WindowAddIzdatelstvo : Window
    {
        public WindowAddIzdatelstvo()
        {
            InitializeComponent();
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
                PublishingHouse publishingHouse = new PublishingHouse();
                publishingHouse.Nazvanie = add.Text;
                BD.bD.PublishingHouse.Add(publishingHouse);
                BD.bD.SaveChanges();
                this.Close();
            }
        }
    }
}
