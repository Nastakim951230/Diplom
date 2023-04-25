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
    /// Логика взаимодействия для PageReturn.xaml
    /// </summary>
    public partial class PageReturn : Page
    {
        Employees employees;
        public static DateTime date;
        public PageReturn(Employees employees)
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
            List<IssueOrReturn> issues = BD.bD.IssueOrReturn.ToList();
            if (issues.Count>0)
            {
                listReturn.ItemsSource = issues;
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

        private void searhReader_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<IssueOrReturn> issueOrReturns = BD.bD.IssueOrReturn.Where(x => x.IDLibrary == employees.LibraryID).ToList();
            if (!string.IsNullOrWhiteSpace(searhReader.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                issueOrReturns = issueOrReturns.Where(x => x.IDnomer.ToLower().Contains(searhReader.Text.ToLower())).ToList();

            }

           

            if (issueOrReturns.Count > 0)
            {
                listReturn.ItemsSource = issueOrReturns;
                tbEmpty.Visibility=Visibility.Collapsed;
            }

            else
            {
                listReturn.ItemsSource = issueOrReturns;
                tbEmpty.Visibility = Visibility.Visible;
            }


        }

        private void extend_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            IssueOrReturn issueOrReturn = BD.bD.IssueOrReturn.FirstOrDefault(x => x.IssueOrReturnID == index);
            WindowExtend windowExtend = new WindowExtend();
            windowExtend.ShowDialog();
            if (date > DateTime.Today)
            {
                issueOrReturn.ReturnDate = date;
                BD.bD.SaveChanges();
                FrameNavigate.per.Navigate(new PageReturn(employees));
            }
            else
            {
                MessageBox.Show("Введенная дата меньше чем сегодняшняя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void returnBook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            IssueOrReturn issueOrReturn = BD.bD.IssueOrReturn.FirstOrDefault(x => x.IssueOrReturnID == index);
            BooksAndLibraries booksAndLibraries=BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDBook==issueOrReturn.IDBook && x.IDLibrary==issueOrReturn.IDLibrary);
            int bookCount = booksAndLibraries.count + issueOrReturn.countBooks;
            booksAndLibraries.count = bookCount;
            BD.bD.IssueOrReturn.Remove(issueOrReturn);
            BD.bD.SaveChanges();
            FrameNavigate.per.Navigate(new PageReturn(employees));

        }
    }
}
