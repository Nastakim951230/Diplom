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
    /// Логика взаимодействия для PageSpisokEmployees.xaml
    /// </summary>
    public partial class PageSpisokEmployees : Page
    {
        Employees employees;
        public PageSpisokEmployees(Employees employees)
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
            CBRole.Items.Add("Роль не выбрана");
            List<Roles> roles = BD.bD.Roles.ToList();
            for(int i = 0; i < roles.Count; i++)
            {
                CBRole.Items.Add(roles[i].Role);
            }
            CBRole.SelectedIndex=0;
            listEmployees.ItemsSource = BD.bD.Employees.Where(x=>x.EmployeeID!=employees.EmployeeID && x.LibraryID==employees.LibraryID).ToList();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }


        /// <summary>
        /// поиск и фильтор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void filter()
        {
            List<Employees> employee = BD.bD.Employees.Where(x => x.EmployeeID != employees.EmployeeID && x.LibraryID == employees.LibraryID).ToList();
            if (!string.IsNullOrWhiteSpace(searhEmployees.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                employee = employee.Where(x => x.Surname.ToLower().Contains(searhEmployees.Text.ToLower())).ToList();
               
            }

            if(CBRole.SelectedIndex>0)
            {
                employee=employee.Where(x => x.RoleID==CBRole.SelectedIndex).ToList();
            }

            if (employee.Count > 0)
            {
                listEmployees.ItemsSource = employee;
            }

            else
            {
                searhEmployees.Text = "";
                MessageBox.Show("Данного сотрудника не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageAddAndUpdateEmployees(employees));
        }

        private void searhEmployees_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void CBRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }


        private void look_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 

           Employees employee=BD.bD.Employees.FirstOrDefault(x=>x.EmployeeID==index);
            FrameNavigate.per.Navigate(new PageLookEmployees(employees, employee));
        }
    }
}
