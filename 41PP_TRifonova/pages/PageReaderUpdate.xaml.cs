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
    /// Логика взаимодействия для PageReaderUpdate.xaml
    /// </summary>
    public partial class PageReaderUpdate : Page
    {
        Employees employees;
        Reader reader;
        public static int backroundFon=1;
        public PageReaderUpdate(Employees employees,Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
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

            nomer.Text = " " + reader.LibraryCardNumber;
            fioReader.Text = " " + reader.Surname + " " + reader.Name + " " + reader.Otshestvo;
            DateTime birthDate = reader.DateOfBirth;
            int age = CalculateAge(birthDate);
            
            DRReader.Text = " "+ String.Format("{0:dd.MM.yyyy} года", reader.DateOfBirth)+"; "+age+" лет";
            TelefonReader.Text = " " + reader.TelefonNumber;
            RegistrReader.Text = " " + String.Format("{0:dd.MM.yyyy} года", reader.DateOfIssue);
            PereRegistrReader.Text=" "+ String.Format("{0:dd.MM.yyyy} года", reader.ReissuanceDate);
            var brush = new BrushConverter();
            DateTime date = reader.ReissuanceDate;
            date = date.AddYears(1);
            if (date <= DateTime.Today)
            {
                PereRegistrReader.Foreground = (SolidColorBrush)(Brush)brush.ConvertFrom("#fc1703");
                PereRegistratsiaButton.Visibility = Visibility.Visible;
            }
            else
            {
                if (backroundFon == 1)
                {
                    PereRegistrReader.Foreground = (SolidColorBrush)(Brush)brush.ConvertFrom("#225496");
                }
                else if (backroundFon == 2)
                {
                    PereRegistrReader.Foreground = (SolidColorBrush)(Brush)brush.ConvertFrom("#6A2296");
                }
                else if(backroundFon == 3)
                {
                    PereRegistrReader.Foreground = (SolidColorBrush)(Brush)brush.ConvertFrom("#225E96");
                }
            }
            List<IssueOrReturn> issueOrReturn = BD.bD.IssueOrReturn.Where(x => x.IDReader == reader.LibraryCardNumber).ToList();
            if (issueOrReturn.Count == 0)
            {
                NumberOfIssued.Text = " 0";
                DeletButton.Visibility = Visibility.Visible;
                NumberOfDuty.Text = " 0";
            }
            else
            {
                NumberOfIssued.Text =" "+ issueOrReturn.Count.ToString();
                int countDuty=0;
                for (int i = 0; i < issueOrReturn.Count; i++)
                {
                    if(issueOrReturn[i].ReturnDate<=DateTime.Today)
                    {
                        countDuty++;
                    }
                }
                NumberOfDuty.Text=" "+Convert.ToString(countDuty);
            }
            int bookingCount = 0;
            List<Booking> bookings = BD.bD.Booking.Where(x => x.ReaderID == reader.LibraryCardNumber).ToList();
            if(bookings.Count == 0)
            {
                bookingBooks.Text = " 0";
            }
            else
            {
                for(int i = 0; i < bookings.Count; i++)
                {
                    bookingCount += bookings[i].countBooks;
                }
            }
            bookingBooks.Text = Convert.ToString(bookingCount);
        }
        private static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - birthDate.Year;
            if (birthDate.AddYears(age) > today)
            {
                age--;
            }
            return age;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageReader(employees));
        }

        private void PereRegistratsiaButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите перерегистрировать данного читателя?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    reader.ReissuanceDate = DateTime.Today;
                    BD.bD.SaveChanges();
                    FrameNavigate.per.Navigate(new PageReaderUpdate(employees, reader));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка");
                }
            }

           
        }

        private void DeletButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить этого читателя?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    
                        BD.bD.Reader.Remove(reader);
                        BD.bD.SaveChanges();
                        MessageBox.Show("Читатель удален");
                        FrameNavigate.per.Navigate(new PageReader(employees));
                  
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }
    }
}
