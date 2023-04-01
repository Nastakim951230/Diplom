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
            if (SurnameReader.Text == "" || NameReader.Text == "" || DateReader.Text == "" || GenderReader.SelectedIndex == 0 || TelefonReader.Text == ""|| SurnameReaderRoditel.Text==""|| NameReaderRoditel.Text==""|| DateReaderRoditel.Text==""|| GenderReaderRoditel.SelectedIndex==0|| TelefonReaderRoditel.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (numberTelefon(TelefonReader.Text))
                {
                    if (numberTelefon(TelefonReaderRoditel.Text))
                    {
                        InformationAboutParents informationAboutParents = new InformationAboutParents();
                        informationAboutParents.Surname = SurnameReaderRoditel.Text;
                        informationAboutParents.Name = NameReaderRoditel.Text;
                        if (OthestvoReaderRoditel.Text == "")
                        {
                            informationAboutParents.Otshestvo = null;
                        }
                        else
                        {
                            informationAboutParents.Otshestvo = OthestvoReaderRoditel.Text;
                        }
                        informationAboutParents.DateOfBirth = Convert.ToDateTime(DateReaderRoditel.Text);
                        informationAboutParents.GenderID = GenderReaderRoditel.SelectedIndex;
                        informationAboutParents.TelefonNumber = TelefonReaderRoditel.Text;
                        if(AddressRegistrasiiRoditel.Text=="")
                        {
                            informationAboutParents.Address = null;
                        }
                        else
                        {
                            informationAboutParents.Address= AddressRegistrasiiRoditel.Text;
                        }

                        if (AddressFactionsRoditel.Text == "")
                        {
                            informationAboutParents.PlaceOfWork = null;
                        }
                        else
                        {
                            informationAboutParents.PlaceOfWork = AddressFactionsRoditel.Text;
                        }

                        if (email.Text == "")
                        {
                            informationAboutParents.Email = null;
                        }
                        else
                        {
                            informationAboutParents.Email = email.Text;
                        }
                        BD.bD.InformationAboutParents.Add(informationAboutParents);

                        Reader reader = new Reader();
                        reader.Surname = SurnameReader.Text;
                        reader.Name = NameReader.Text;
                        if (OthestvoReader.Text == "")
                        {
                            reader.Otshestvo = null;
                        }
                        else
                        {
                            reader.Otshestvo = OthestvoReader.Text;
                        }
                        reader.DateOfBirth = Convert.ToDateTime(DateReader.Text);
                        reader.GenderID = GenderReader.SelectedIndex;
                        reader.TelefonNumber = Telefon(TelefonReader.Text);
                        if (AddressRegistrasii.Text == "")
                        {
                            reader.Address = null;
                        }
                        else
                        {
                            reader.Address = AddressRegistrasii.Text;
                        }

                       
                            reader.PlaceOfWork = null;
                        
                       
                        if (addressStudy.Text == "")
                        {
                            reader.PlaceOfStudy = null;
                        }
                        else
                        {
                            reader.PlaceOfStudy = addressStudy.Text;
                        }
                       
                            reader.Email = null;
                       
                        reader.InformationAboutParentsID = informationAboutParents.ID;
                        reader.IDLibrary = employees.LibraryID;
                        reader.DateOfIssue = DateTime.Today;
                        reader.ReissuanceDate = DateTime.Today;
                        BD.bD.Reader.Add(reader);
                        BD.bD.SaveChanges();
                        MessageBox.Show("Читатель добавлен");
                        FrameNavigate.per.Navigate(new PageReader(employees));
                    }
                    else
                    {
                        MessageBox.Show("Неправильно введен телефон у родителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Неправильно введен телефон у ребенка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void saveReader_Click_1(object sender, RoutedEventArgs e)
        {
            if (surnameReader.Text == "" || nameReader.Text==""|| dateReader.Text=="" || genderReader.SelectedIndex==0|| telefonReader.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (numberTelefon(telefonReader.Text))
                {
                    Reader reader = new Reader();
                    reader.Surname = surnameReader.Text;
                    reader.Name = nameReader.Text;
                    if (othestvoReader.Text == "")
                    {
                        reader.Otshestvo = null;
                    }
                    else
                    {
                        reader.Otshestvo = othestvoReader.Text;
                    }
                    reader.DateOfBirth = Convert.ToDateTime(dateReader.Text);
                    reader.GenderID = genderReader.SelectedIndex;
                    reader.TelefonNumber = Telefon(telefonReader.Text);
                    if (addressRegistrasii.Text == "")
                    {
                        reader.Address = null;
                    }
                    else
                    {
                        reader.Address = addressRegistrasii.Text;
                    }

                    if (addressFactions.Text == "")
                    {
                        reader.PlaceOfWork = null;
                    }
                    else
                    {
                        reader.PlaceOfWork = addressFactions.Text;
                    }
                    if (AddressStudy.Text == "")
                    {
                        reader.PlaceOfStudy = null;
                    }
                    else
                    {
                        reader.PlaceOfStudy = AddressStudy.Text;
                    }
                    if (Email.Text == "")
                    {
                        reader.Email = null;
                    }
                    else
                    {
                        reader.Email = Email.Text;
                    }
                    reader.InformationAboutParentsID = null;
                    reader.IDLibrary = employees.LibraryID;
                    reader.DateOfIssue = DateTime.Today;
                    reader.ReissuanceDate=DateTime.Today;
                    BD.bD.Reader.Add(reader);
                    BD.bD.SaveChanges();
                    MessageBox.Show("Читатель добавлен");
                    FrameNavigate.per.Navigate(new PageReader(employees));
                }
                else
                {
                    MessageBox.Show("Неправильно введен телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
          
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
