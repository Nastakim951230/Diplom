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
    /// Логика взаимодействия для PageBooking.xaml
    /// </summary>
    public partial class PageBooking : Page
    {
        Employees employees;
        public PageBooking(Employees employees)
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
            listBooking.ItemsSource=BD.bD.Booking.Where(x=>x.FromWhere==employees.LibraryID).ToList();
            listBookingLibrary.ItemsSource = BD.bD.Booking.Where(x => x.ToWhere == employees.LibraryID && x.look!=1).ToList();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void ToCancelReservation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Booking booking = BD.bD.Booking.FirstOrDefault(x => x.BookingID == index);
        }

        private void sent_Click(object sender, RoutedEventArgs e)
        {
            Button btn=(Button)sender;
            int index=Convert.ToInt32(btn.Uid);
            int bookCount = 0;
            Booking booking = BD.bD.Booking.FirstOrDefault(x => x.BookingID == index);
            BooksAndLibraries booksAndLibraries=BD.bD.BooksAndLibraries.FirstOrDefault(x=>x.IDLibrary==booking.ToWhere && x.IDBook==booking.BookID);
            bookCount = booksAndLibraries.count;
            booksAndLibraries.count = bookCount - booking.countBooks;
            booking.look = 1;
            BD.bD.SaveChanges();
            FrameNavigate.per.Navigate(new PageBooking(employees));
        }
    }
}
