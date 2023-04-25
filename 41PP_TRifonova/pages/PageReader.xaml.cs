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
    /// Логика взаимодействия для PageReader.xaml
    /// </summary>
    public partial class PageReader : Page
    {
        Employees employees;
        public PageReader(Employees employees)
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
            List<Reader> readers= BD.bD.Reader.Where(x => x.IDLibrary == employees.LibraryID).ToList();
            if (readers.Count>0)
            {
                listReader.ItemsSource = readers;
                tbEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbEmpty.Visibility = Visibility.Visible;
            }
            
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void addReader_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageAddReader(employees));
        }

        private void lookReader_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;  // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка 
            Reader reader=BD.bD.Reader.FirstOrDefault(x=>x.LibraryCardNumber==index);
            FrameNavigate.per.Navigate(new PageReaderUpdate(employees, reader));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Reader> readers = BD.bD.Reader.Where(x => x.IDLibrary == employees.LibraryID).ToList();
            
            if (!string.IsNullOrWhiteSpace(searhReader.Text))
            {
                readers = readers.Where(x => x.id.ToLower().Contains(searhReader.Text.ToLower())).ToList();
            }

            if(readers.Count>0)
            {
                listReader.ItemsSource=readers;
                tbEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                listReader.ItemsSource = readers;
                tbEmpty.Visibility = Visibility.Visible;
            }

        }
    }
}
