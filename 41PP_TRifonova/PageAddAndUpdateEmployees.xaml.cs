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
    /// Логика взаимодействия для PageAddAndUpdateEmployees.xaml
    /// </summary>
    public partial class PageAddAndUpdateEmployees : Page
    {
        Employees employees;
        Employees employee;
        bool update=false;
        public PageAddAndUpdateEmployees(Employees employees)
        {
            InitializeComponent();
            this.employees = employees;
            fioEmployees();
           
        }


        public PageAddAndUpdateEmployees(Employees employees, Employees employee)
        {
            InitializeComponent();
            this.employees = employees;
            this.employee = employee;
            fioEmployees();

            update=true;
            surnameEmployees.Text=employee.Surname;
            nameEmployees.Text=employee.Name;
            othestvoEmployees.Text = employees.Otchestvo;
            login.Text=employee.Login;
            password.Text=employee.Password;
            CBRoles.SelectedIndex = employee.RoleID;
            CBLibrary.SelectedIndex=employee.LibraryID;
            CBGender.SelectedIndex = employee.IDGender;
            addressEmployees.Text = employee.Address;
            dataOfBirth.Text = String.Format("{0:dd MM yyyy}", employee.DateOfBirth);
            string telefonnomer = "";
            for(int i=0;i<employee.Telefon.Length;i++)
            {
                if(employee.Telefon[i]=='(')
                {
                    
                }
                else if (employee.Telefon[i] == ')')
                {

                }
                else if(employee.Telefon[i] == '-')
                {

                }
                else
                {
                    telefonnomer += employee.Telefon[i];
                }
            }
            telefonNumber.Text = telefonnomer;
        }

        /// <summary>
        /// Вывод ФИО сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void fioEmployees()
        {
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


            CBGender.Items.Add("Пол не выбран");
            List<Gender> genders = BD.bD.Gender.ToList();
            for (int i = 0; i < genders.Count; i++)
            {
                CBGender.Items.Add(genders[i].gender1);
            }
            CBGender.SelectedIndex = 0;
            telefonNumber.Text = "+7";
            CBRoles.Items.Add("Роль не выбрана");
            List<Roles> roles = BD.bD.Roles.ToList();
            for (int j = 0; j < roles.Count; j++)
            {
                CBRoles.Items.Add(roles[j].Role);
            }
            CBRoles.SelectedIndex = 0;
            CBLibrary.Items.Add("Место работы не выбрано");
            List<Libraries> libraries = BD.bD.Libraries.ToList();
            for (int i = 0; i < libraries.Count; i++)
            {
                CBLibrary.Items.Add(libraries[i].library);
            }
            CBLibrary.SelectedIndex = 0;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (update == false)
            {
                FrameNavigate.per.Navigate(new PageSpisokEmployees(employees));
            }
            else
            {
                FrameNavigate.per.Navigate(new PageLookEmployees(employees, employee));
            }
        }

        private void telefonNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!(Char.IsDigit(ch)))
                {
                    e.Handled = true;
                    break;
                }
            }
        }


        public string Telefon(string a)
        {
            string nomer = "";
            if(a.Length==12)
            {
                for(int i = 0; i < a.Length; i++)
                {
                    if(i==2)
                    {
                        nomer += "(" + a[i];
                    }
                    else if(i==5)
                    {
                        nomer += ")" + a[i];
                    }
                    else if(i==8 || i==10)
                    {
                        nomer += "-" + a[i];
                    }
                    else
                    {
                        nomer +=  a[i];
                    }
                }
                return nomer;
            }
            else if(a.Length==11)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if(i==0)
                    {
                        nomer += "+7";
                    }
                   else if (i == 1)
                    {
                        nomer += "(" + a[i];
                    }
                    else if (i == 4)
                    {
                        nomer += ")" + a[i];
                    }
                    else if (i == 7 || i == 9)
                    {
                        nomer += "-" + a[i];
                    }
                    else
                    {
                        nomer += a[i];
                    }
                }
                return nomer;
            }
            else
            {
                return "1";
            }
        }
        public bool numberTelefon(string b)
        {
            string a=Telefon(b);
            if (a == "1")
            {
                return false;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (i == 0)
                    {
                        if (a[i] == '+')
                        {
                            count++;
                        }
                    }
                    if (i == 2)
                    {
                        if (a[i] == '(')
                        {
                            count++;
                        }
                    }
                    if (i == 6)
                    {
                        if (a[i] == ')')
                        {
                            count++;
                        }
                    }
                    if (i == 10 || i == 13)
                    {
                        if (a[i] == '-')
                        {
                            count++;
                        }
                    }
                }
                if (count == 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(surnameEmployees.Text==""|| nameEmployees.Text==""|| login.Text==""|| password.Text==""|| CBRoles.SelectedIndex==0|| CBLibrary.SelectedIndex==0 || CBGender.SelectedIndex==0|| dataOfBirth.Text==""|| telefonNumber.Text==""|| addressEmployees.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if(numberTelefon(telefonNumber.Text))
                {
                    if(update==false)
                    {
                        employee = new Employees();
                    }
                    employee.Surname=surnameEmployees.Text;
                    employee.Name=nameEmployees.Text;
                    if(othestvoEmployees.Text=="")
                    {
                        employee.Otchestvo = null;
                    }
                    else
                    {
                        employee.Otchestvo = othestvoEmployees.Text;
                    }
                    employee.Login=login.Text;
                    employee.Password=password.Text;
                    employee.RoleID=CBRoles.SelectedIndex;
                    employee.Address=addressEmployees.Text;
                    employee.LibraryID=CBLibrary.SelectedIndex;
                    employee.Telefon=Telefon(telefonNumber.Text);
                    employee.IDGender=CBGender.SelectedIndex;
                    employee.DateOfBirth = Convert.ToDateTime(dataOfBirth.Text);
                    if (update == false)
                    {
                        BD.bD.Employees.Add(employee);
                    }
                    BD.bD.SaveChanges();
                    MessageBox.Show("Сотрудник добавлен");
                    FrameNavigate.per.Navigate(new PageSpisokEmployees(employees));

                }
                else
                {
                    MessageBox.Show("Неправильно введен телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void surnameEmployees_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (Char.IsDigit(ch))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

       
    }
}
