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
    /// Логика взаимодействия для Avtorizatsia.xaml
    /// </summary>
    public partial class Avtorizatsia : Window
    {
        Employees employees;
        public Avtorizatsia()
        {
            InitializeComponent();
            BD.bD=new BaseBD();
        }

        public void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if(PbPassword.Password.Length > 0)
            {
                TxPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxPassword.Visibility = Visibility.Visible;
            }
        }
        private void avtorizatsiaBT_Click(object sender, RoutedEventArgs e)
        {
            if(tbLogin.Text=="" || PbPassword.Password=="")
            {
                MessageBox.Show("Обязательные поля не заполнены","Ошибка", MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                employees=BD.bD.Employees.FirstOrDefault(x=>x.Login==tbLogin.Text && x.Password==PbPassword.Password);
                if (employees == null)
                {
                    MessageBox.Show("Данного пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    WindowEmployees window = new WindowEmployees(employees);
                    
                    this.Close();
                    window.Show();
                }
            }

        }
    }
}
