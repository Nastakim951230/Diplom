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
    /// Логика взаимодействия для PageAddReader.xaml
    /// </summary>
    public partial class PageAddReader : Page
    {
        Employees employees;
        public PageAddReader(Employees employees)
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
            GenderReader.Items.Add("Пол не выбран");
            genderReader.Items.Add("Пол не выбран");
            GenderReaderRoditel.Items.Add("Пол не выбран");
            List<Gender> genders = BD.bD.Gender.ToList();
            for(int i = 0; i < genders.Count; i++)
            {
                GenderReader.Items.Add(genders[i].gender1);
                genderReader.Items.Add(genders[i].gender1);
                GenderReaderRoditel.Items.Add(genders[i].gender1);
            }
            genderReader.SelectedIndex = 0;
            GenderReader.SelectedIndex = 0;
            GenderReaderRoditel.SelectedIndex = 0;
            TelefonReaderRoditel.Text = "+7";
            TelefonReader.Text = "+7";
            telefonReader.Text = "+7";
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageReader(employees));
        }

        private void SaveReader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveReader_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public string Telefon(string a)
        {
            string nomer = "";
            if (a.Length == 12)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (i == 2)
                    {
                        nomer += "(" + a[i];
                    }
                    else if (i == 5)
                    {
                        nomer += ")" + a[i];
                    }
                    else if (i == 8 || i == 10)
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
            else if (a.Length == 11)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (i == 0)
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
            string a = Telefon(b);
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
    }
}
