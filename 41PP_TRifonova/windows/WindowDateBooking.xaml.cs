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
    /// Логика взаимодействия для WindowDateBooking.xaml
    /// </summary>
    public partial class WindowDateBooking : Window
    {
        public WindowDateBooking()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (date.Text == "")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                PageBooking.date = Convert.ToDateTime(date.Text);
                this.Close();
            }
        }
    }
}
