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
    /// Логика взаимодействия для PageApplication.xaml
    /// </summary>
    public partial class PageApplication : Page
    {
        Employees employees;
        public PageApplication(Employees employees)
        {
            InitializeComponent();
            this.employees = employees;

            string name = "";
            string othestvo = "";
            for (int i = 0; i < employees.Name.Length; i++)
            {
                if (i == 0)
                {
                    name += employees.Name[i];
                }
            }
            for (int i = 0; i < employees.Otchestvo.Length; i++)
            {
                if (i == 0)
                {
                    othestvo += employees.Otchestvo[i];
                }
            }
            //Вывод ФИО сотрудника
            FIO.Text = employees.Surname + " " + name + ". " + othestvo + ".";

          List<Reader> reader = BD.bD.Reader.ToList();
            List<Reader> readers = new List<Reader>();
            List<Application> applicaciones = BD.bD.Application.ToList();
            for (int i = 0; i < reader.Count; i++)
            {
                List<Application> applications = applicaciones.Where(x=>x.IDReader==reader[i].LibraryCardNumber).ToList();
                if(applications.Count>0)
                {
                    readers.Add(reader[i]);
                }
            }
            listApplication.ItemsSource = readers;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void issued_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
