using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Drawing;






namespace _41PP_TRifonova
{
    /// <summary>
    /// Логика взаимодействия для PageAddBook.xaml
    /// </summary>
    public partial class PageAddBook : Page
    {
        byte[] Barray = null;
        Employees employees;
       

        string newFilePath = null;  // путь к картинке
       
        Books books;
        int index;

        
        public PageAddBook(Employees employees)
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

            List<AgeRestrictions> ageRestrictions = BD.bD.AgeRestrictions.ToList();
            restrictions.Items.Add("без возрастного ограничения");
            for(int i = 0; i < ageRestrictions.Count; i++)
            {
                restrictions.Items.Add(ageRestrictions[i].Restrictions);
            }
            restrictions.SelectedIndex = 0;

            List<PublishingHouse> publishingHouse = BD.bD.PublishingHouse.ToList();
            izdatelstvo.Items.Add("не выбрано издательство");
            for(var i = 0; i < publishingHouse.Count; i++)
            {
                izdatelstvo.Items.Add(publishingHouse[i].Nazvanie);
            }
            izdatelstvo.Items.Add("Добавить издательство");
            izdatelstvo.SelectedIndex = 0;
            listAvtor.ItemsSource = BD.bD.Authors.ToList();
            listganre.ItemsSource=BD.bD.Genres.ToList();

            //Заполнение списка каталог
            CBCatalog.Items.Add("Все каталоги");
            List<Catalogs> catalogs = BD.bD.Catalogs.ToList();
            for (int i = 0; i < catalogs.Count; i++)
            {
                CBCatalog.Items.Add(catalogs[i].catalog);
            }
            CBCatalog.SelectedIndex = 0;

            //Заполнение списка подкаталог
            CBPodCatalog.Items.Add("Все подкаталоги");
           
            CBPodCatalog.SelectedIndex = 0;

            

        }

        
        void filter()
        {
            List<SubDirectory> subDirectories = BD.bD.SubDirectory.ToList();
            List<Genres> genres = BD.bD.Genres.ToList();
            List<Genres> genresList= new List<Genres>();
            //List<Genres> genres = BD.bD.Genres.Where(x => x.Ganre !="").ToList();

            //поиск название


            if (!string.IsNullOrWhiteSpace(searhGanre.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                subDirectories = subDirectories.Where(x => x.SubDirectory1.ToLower().Contains(searhGanre.Text.ToLower())).ToList();
                for (int i = 0; i < subDirectories.Count; i++)
                {
                    List<Genres> genre = new List<Genres>();
                    genre =genres.Where(x => x.DirectoryAndSubDirectoryID == subDirectories[i].SubDirectoryID).ToList();
                    for(int j = 0; j < genre.Count; j++)
                    {
                        genresList.Add(genre[j]);
                    }
                }
                genres = genresList;
            }
               
          
                if(CBCatalog.SelectedIndex>0)
            {
                genres = genres.Where(x => x.catalogInt == CBCatalog.SelectedIndex).ToList();
            }
                if (CBPodCatalog.SelectedIndex > 0)
            {
                genres=genres.Where(x=>x.DirectoryAndSubDirectoryID==index).ToList();
            }

             

            if (genres.Count > 0)
            {
                listganre.ItemsSource = genres;
            }
            
            else
            {
                searhGanre.Text = "";
                MessageBox.Show("Данного жанра не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var tb = (TextBox)e.OriginalSource;
        //    if (tb.SelectionStart != 0)
        //    {
        //        CB.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
        //    }
        //    if (tb.SelectionStart == 0 && CB.SelectedItem == null)
        //    {
        //        CB.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
        //    }

        //    CB.IsDropDownOpen = true;
        //    if (CB.SelectedItem == null)
        //    {
        //        // Если элемент не выбран менять фильтр
        //        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
        //        cv.Filter = s => ((string)s).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        //    }

        //}

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
        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
                openFileDialog.ShowDialog(); // показываем
                Barray = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
                showImage(Barray, photoBook);  // отображаем картинку
            }
            catch
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите добавить эту книгу?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (nazvanie.Text == "" || data.Text == "" || izdatelstvo.SelectedIndex == 0 || isbn.Text == "" || countStr.Text == "" ||  count.Text == "" || listganre.SelectedItem==null || listAvtor.SelectedItem==null)
                    {
                        MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        DateTime dateTime = DateTime.Today;
                        Books book=BD.bD.Books.FirstOrDefault(x=>x.Nazvanie==nazvanie.Text);

                        if (Convert.ToInt32(data.Text) <= Convert.ToInt32(dateTime.ToString("yyyy")))
                        {
                            if (book == null)
                            {
                                books = new Books();




                                books.Nazvanie = nazvanie.Text;

                                books.IDPublishingHouse = izdatelstvo.SelectedIndex;
                                books.Year = Convert.ToInt32(data.Text);
                                books.ISBN = isbn.Text;
                                books.Pages = Convert.ToInt32(countStr.Text);
                                if (restrictions.SelectedIndex == 0)
                                {
                                    books.RestrictionsID = null;
                                }
                                else
                                {
                                    books.RestrictionsID = restrictions.SelectedIndex;
                                }

                                if (descrition.Text == "")
                                {
                                    books.Description = null;

                                }
                                else
                                {
                                    books.Description = descrition.Text;
                                }
                                if (newFilePath == null)
                                {
                                    books.Photo = null;
                                }
                                else
                                {
                                    books.Photo = Barray;
                                    
                                }
                                BD.bD.Books.Add(books);


                                List<BooksAndAuthors> authors = BD.bD.BooksAndAuthors.Where(x => x.BookID == books.BookID).ToList();
                                // если список не пустой, удаляем из него всех авторов
                                if (authors.Count > 0)
                                {
                                    foreach (BooksAndAuthors t in authors)
                                    {
                                        BD.bD.BooksAndAuthors.Remove(t);
                                    }
                                }


                                foreach (Authors bf in listAvtor.SelectedItems)
                                {
                                    BooksAndAuthors booksAndAuthors = new BooksAndAuthors()  // объект для записи в таблицу TraitsCat
                                    {
                                        BookID = books.BookID,
                                        AuthorID = bf.AuthorID,
                                    };
                                    BD.bD.BooksAndAuthors.Add(booksAndAuthors);

                                }


                                List<BooksAndGanres> booksAndGanres = BD.bD.BooksAndGanres.Where(x => x.IDBook == books.BookID).ToList();
                                // если список не пустой, удаляем из него все жанры
                                if (booksAndGanres.Count > 0)
                                {
                                    foreach (BooksAndGanres t in booksAndGanres)
                                    {
                                        BD.bD.BooksAndGanres.Remove(t);
                                    }
                                }

                                foreach (Genres bf in listganre.SelectedItems)
                                {

                                    BooksAndGanres ganres = new BooksAndGanres()
                                    {
                                        IDCatalog = bf.catalogInt,
                                        IDUnderTheDirectory = bf.DirectoryAndSubDirectoryID,
                                        IDGanre = bf.GenreID,
                                        IDBook = books.BookID,
                                    };
                                    BD.bD.BooksAndGanres.Add(ganres);

                                }

                                BooksAndLibraries libraries = new BooksAndLibraries();
                                libraries.IDBook = books.BookID;
                                libraries.IDLibrary = employees.LibraryID;
                                libraries.count = Convert.ToInt32(count.Text);

                                BD.bD.BooksAndLibraries.Add(libraries);
                                BD.bD.SaveChanges();
                                MessageBox.Show("Книга добавлена");
                                FrameNavigate.per.Navigate(new PageEmployees(employees));

                            }
                            else
                            {
                                MessageBox.Show("Неправильно введен год создания книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else 
                        {
                            MessageBox.Show("Данная книга уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка");
                }
            }
          
        }
       
        private void izdatelstvo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<PublishingHouse> publishingHouse = BD.bD.PublishingHouse.ToList();
            if (izdatelstvo.SelectedIndex ==(publishingHouse.Count+1))
            {
                
                WindowAddIzdatelstvo windowAdd = new WindowAddIzdatelstvo();
                windowAdd.ShowDialog();

                List<PublishingHouse> publishinghouse = BD.bD.PublishingHouse.ToList();
                izdatelstvo.Items.Remove("не выбрано издательство");
                for (var i = 0; i < publishinghouse.Count; i++)
                {
                    izdatelstvo.Items.Remove(publishinghouse[i].Nazvanie);
                }
                izdatelstvo.Items.Remove("Добавить издательство");
                izdatelstvo.Items.Add("не выбрано издательство");
                for (var i = 0; i < publishinghouse.Count; i++)
                {
                    izdatelstvo.Items.Add(publishinghouse[i].Nazvanie);
                }
                izdatelstvo.Items.Add("Добавить издательство");
                izdatelstvo.SelectedIndex = 0;
            }
        }

        private void searhAvtor_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Authors> authors = new List<Authors>();
            authors = BD.bD.Authors.ToList();
            //поиск название

            if (!string.IsNullOrWhiteSpace(searhAvtor.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                authors = authors.Where(x => x.SurnameAuthor.ToLower().Contains(searhAvtor.Text.ToLower())).ToList();
            }
            if(authors.Count > 0)
            {
                listAvtor.ItemsSource = authors;
            }
            else
            {
                searhAvtor.Text = "";
                MessageBox.Show("Данного автора не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addAvtor_Click(object sender, RoutedEventArgs e)
        {
            WindowAddAvtor windowAddAvtor= new WindowAddAvtor();
            windowAddAvtor.ShowDialog();
            listAvtor.ItemsSource = BD.bD.Authors.ToList();
        }

        private void searhGanre_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void CBCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBCatalog.SelectedIndex > 0)
            {
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.ToList();

                for (int i = 0; i < subDirectories.Count; i++)
                {
                    CBPodCatalog.Items.Remove(subDirectories[i].SubDirectory1);
                }
                List<SubDirectory> directories = BD.bD.SubDirectory.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();

                for (int i = 0; i < directories.Count; i++)
                {

                    CBPodCatalog.Items.Add(directories[i].SubDirectory1);

                }

                CBPodCatalog.SelectedIndex = 0;
                CBPodCatalog.Visibility = Visibility.Visible;

            }
            else
            {
                CBPodCatalog.Visibility = Visibility.Collapsed;
               
            }
            filter();
        }

        private void CBPodCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (CBPodCatalog.SelectedIndex > 0)
            //{

            //    List<SubDirectory> subDirectorie = BD.bD.SubDirectory.ToList();
            //    subDirectorie = subDirectorie.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
            //    if (CBPodCatalog.SelectedIndex > 0)
            //    {
            //        for (int i = 0; i < subDirectorie.Count; i++)
            //        {
            //            if (CBPodCatalog.SelectedIndex == i + 1)
            //            {
            //                CBPodCatalog.SelectedValuePath = Convert.ToString(subDirectorie[i].SubDirectoryID);
            //            }

            //        }
            //    }

            //}
            //else
            //{
            //    CBPodCatalog.SelectedIndex = 0;

            //}

            if (CBPodCatalog.SelectedIndex > 0)
            {

                
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                for (int i = 0; i < subDirectories.Count + 1; i++)
                {
                    if (i == CBPodCatalog.SelectedIndex)
                    {
                        index = subDirectories[i - 1].SubDirectoryID;
                    }
                }
            }
           
            filter();
        }

        private void data_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) )
            {
                e.Handled = true;
            }
        }

        private void isbn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            foreach (var ch in e.Text)
            {
                if (!(Char.IsDigit(ch) || ch.Equals('-')))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void countStr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
