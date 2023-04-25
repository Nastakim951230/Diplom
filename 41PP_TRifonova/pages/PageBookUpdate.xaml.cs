using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для PageBookUpdate.xaml
    /// </summary>
    public partial class PageBookUpdate : Page
    {
        
       
        byte[] Barray = null;
        Employees employees;
        Books books;
        int id;
      
        public PageBookUpdate(Employees employees, Books books, int id)
        {
            InitializeComponent();
            this.employees = employees;
            this.books = books;
            this.id = id;
            if(id== 0)
            {
                updateCount.Visibility = Visibility.Visible;
                bookingBooks.Visibility = Visibility.Collapsed;
                updatePhoto.Visibility=Visibility.Visible;
                updateNazvanie.Visibility = Visibility.Visible;

            }
            else if (id == 1)
            {
                updateCount.Visibility = Visibility.Collapsed;
                bookingBooks.Visibility = Visibility.Visible;
                updatePhoto.Visibility = Visibility.Collapsed;
                updateNazvanie.Visibility = Visibility.Collapsed;

            }
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

            textDescription.Text = books.Description;
            if(books.Photo!=null)
            {

                byte[] Barray = books.Photo;
                showImage(Barray, photoBook);  // отображаем картинку

            }
            textNazvanie.Text= books.Nazvanie;
             List<BooksAndAuthors> booksAndAuthors=BD.bD.BooksAndAuthors.Where(x=>x.BookID==books.BookID).ToList();
           
            string avtors = "";
            if(booksAndAuthors.Count > 0)
            {
                foreach(BooksAndAuthors book in booksAndAuthors)
                {
                    avtors += book.Authors.NameAuthor + " " + book.Authors.SurnameAuthor + " " + book.Authors.OthestvoAuthor + ", ";
                }
                textAvtor.Text= avtors.Substring(0,avtors.Length-2);
            }
            PublishingHouse publishingHouse = BD.bD.PublishingHouse.FirstOrDefault(x => x.PublishingHouseID == books.IDPublishingHouse);
            textIzdatelstvo.Text = publishingHouse.Nazvanie;
            textYear.Text = Convert.ToString(books.Year);
            tetxISBN.Text=books.ISBN;
            textPages.Text = Convert.ToString(books.Pages);

            List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDBook == books.BookID).ToList();
            string ganre = "";
            if(booksAndGanres.Count > 0)
            {
                foreach(BooksAndGanres ganres in booksAndGanres)
                {
                    ganre += ganres.Genres.Ganre + ", ";
                }
                textGanre.Text = ganre.Substring(0, ganre.Length - 2);
            }
            BooksAndGanres andGanres = BD.bD.BooksAndGanres.FirstOrDefault(x => x.IDBook == books.BookID);
            Catalogs catalogs = BD.bD.Catalogs.FirstOrDefault(x => x.CatalogID == andGanres.IDCatalog);
            catalogBook.Text = catalogs.catalog +">";
            SubDirectory subDirectory = BD.bD.SubDirectory.FirstOrDefault(x => x.SubDirectoryID == andGanres.IDUnderTheDirectory);
            pogCatalog.Text = subDirectory.SubDirectory1;
            if (id == 0)
            {
                BooksAndLibraries libraries = BD.bD.BooksAndLibraries.FirstOrDefault(x => x.IDLibrary == employees.LibraryID && x.IDBook == books.BookID);
                textCount.Text = Convert.ToString(libraries.count);
                textganr.Visibility = Visibility.Visible;
            }
            if(id==1)
            {
                addressBook.Visibility = Visibility.Visible;
                List<BooksAndLibraries> booksAndLibraries=BD.bD.BooksAndLibraries.Where(x=>x.IDBook==books.BookID && x.IDLibrary!=employees.LibraryID).ToList();


                string library = "";
                if (booksAndLibraries.Count > 0)
                {
                    foreach (BooksAndLibraries libraries in booksAndLibraries)
                    {
                        library += libraries.Libraries.library + ", в количестве: "+libraries.count+" шт\n";
                    }
                    textaddressBook.Text = library.Substring(0, library.Length - 1);
                }

            }
            if (books.RestrictionsID!=null)
            {
                AgeRestrictions restrictions=BD.bD.AgeRestrictions.FirstOrDefault(x=>x.AgeRestrictionsID==books.RestrictionsID);
                restristons.Visibility = Visibility.Visible;
                textRestristons.Visibility= Visibility.Visible;
                textRestristons.Text=restrictions.Restrictions;
            }

            
        }

        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void updatePhoto_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
                openFileDialog.ShowDialog(); // показываем
                Barray = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
                books.Photo = Barray;
                BD.bD.SaveChanges();
                showImage(Barray, photoBook);  // отображаем картинку
            }
            catch
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void updateNazvanie_Click(object sender, RoutedEventArgs e)
        {
            WindowUpdateNazvanie windowUpdateNazvanie=new WindowUpdateNazvanie(books);
            windowUpdateNazvanie.ShowDialog();
            FrameNavigate.per.Navigate(new PageBookUpdate(employees, books,0));
        }

        private void updateCount_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCount count = new WindowAddCount(employees, books);
            count.ShowDialog();
            FrameNavigate.per.Navigate(new PageBookUpdate(employees, books,0));
        }

        private void bookingBooks_Click(object sender, RoutedEventArgs e)
        {
            WindowBooking windowBooking = new WindowBooking(employees,books);
            windowBooking.ShowDialog();

        }
    }
}
