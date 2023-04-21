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
using System.Windows.Threading;

namespace _41PP_TRifonova
{
    /// <summary>
    /// Логика взаимодействия для PageApplication.xaml
    /// </summary>
    public partial class PageApplication : Page
    {
        Employees employees;
        public static DateTime date;
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
            List<ApplicationBooks> applicaciones = BD.bD.ApplicationBooks.ToList();
            for (int i = 0; i < reader.Count; i++)
            {
                List<ApplicationBooks> applications = applicaciones.Where(x=>x.IDReader==reader[i].LibraryCardNumber && x.IDLibrary==employees.LibraryID).ToList();
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
            Button btn=(Button)sender;
            int index=Convert.ToInt32(btn.Uid);
            WindowDate windowDate = new WindowDate();
            windowDate.ShowDialog();
            if (date > DateTime.Today)
            {
                List<ApplicationBooks> applications = BD.bD.ApplicationBooks.Where(x => x.IDReader == index).ToList();
                for (int i = 0; i < applications.Count; i++)
                {
                    IssueOrReturn issueOrReturn = new IssueOrReturn();
                    issueOrReturn.IDBook = applications[i].IDBook;
                    issueOrReturn.IDReader = applications[i].IDReader;
                    issueOrReturn.IDLibrary = applications[i].IDLibrary;
                    issueOrReturn.countBooks = applications[i].countBooks;
                    issueOrReturn.DateOfIssue = DateTime.Today;
                    issueOrReturn.ReturnDate = date;
                    BD.bD.IssueOrReturn.Add(issueOrReturn);
                    BD.bD.ApplicationBooks.Remove(applications[i]);
                }
                BD.bD.SaveChanges();
                FrameNavigate.per.Navigate(new PageApplication(employees));
            }
            else
            {
                MessageBox.Show("Введенная дата меньше чем сегодняшняя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(30);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();
        }
        private void loadedData()
        {
            List<Reader> reader = BD.bD.Reader.ToList();
            List<Reader> readers = new List<Reader>();
            List<ApplicationBooks> applicaciones = BD.bD.ApplicationBooks.ToList();
            for (int i = 0; i < reader.Count; i++)
            {
                List<ApplicationBooks> applications = applicaciones.Where(x => x.IDReader == reader[i].LibraryCardNumber).ToList();
                if (applications.Count > 0)
                {
                    readers.Add(reader[i]);
                }
            }
            listApplication.ItemsSource = readers;
        }
        private void dtTicker(object sender, EventArgs e)
        {
            loadedData();
        }

        private void delet_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы точно хотите удалить заявку?", "Вопрос",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                List<ApplicationBooks> applications = BD.bD.ApplicationBooks.Where(x => x.IDReader == index).ToList();
                for (int i = 0; i < applications.Count; i++)
                {
                    BD.bD.ApplicationBooks.Remove(applications[i]);
                }
                BD.bD.SaveChanges();
                FrameNavigate.per.Navigate(new PageApplication(employees));
            }
        }
    }
}
