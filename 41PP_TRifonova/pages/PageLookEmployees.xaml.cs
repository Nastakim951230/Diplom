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
    /// Логика взаимодействия для PageLookEmployees.xaml
    /// </summary>
    public partial class PageLookEmployees : Page
    {
        Employees employees;
        Employees employee;
        public PageLookEmployees(Employees employees, Employees employee)
        {
            InitializeComponent();
            this.employees = employees;
            this.employee = employee;
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
            //Вывод данных о сотруднике
            FIO.Text = employees.Surname + " " + name + ". " + othestvo + ".";
            fioEmployees.Text = employee.Surname + " " + employee.Name + " " + employee.Otchestvo;
            loginEmployees.Text = employee.Login;
            passwordEmployees.Text = employee.Password;
            Roles roles=BD.bD.Roles.FirstOrDefault(x=>x.ID_role==employee.RoleID);
            rolesEmployees.Text = roles.Role;
            Libraries libraries=BD.bD.Libraries.FirstOrDefault(x=>x.LibraryID==employee.LibraryID);
            libraryEmployees.Text = libraries.library;
            telefoEmployees.Text = employee.Telefon;
            Gender gender = BD.bD.Gender.FirstOrDefault(x => x.GenderID == employee.IDGender);
            genderEmployees.Text = gender.gender1;
            drEmployees.Text = String.Format("{0:dd MMMM yyyy} года.", employee.DateOfBirth);
            addresEmployees.Text = employee.Address;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageSpisokEmployees(employees));
        }

        private void delet_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить этого сотрудника?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    Employees sotrudnik = BD.bD.Employees.FirstOrDefault(x => x.EmployeeID == employee.EmployeeID);
                    BD.bD.Employees.Remove(sotrudnik);
                    BD.bD.SaveChanges();
                    MessageBox.Show("Сотрудника удален");
                    FrameNavigate.per.Navigate(new PageSpisokEmployees(employees));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void updateEmployees_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageAddAndUpdateEmployees(employees, employee));
        }
    }
}
