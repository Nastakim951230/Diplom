﻿using Microsoft.Win32;
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
        Employees employees;
        string path = null;  // путь к картинке
        byte[] Barray = null;


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



        private byte[] photo()
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                                                            //OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
                OFD.ShowDialog();  // открываем диалоговое окно             
                path = OFD.FileName;  // считываем путь выбранного изображения

                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу
                ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                showImage(Barray, photoBook);
                return Barray;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
                return null;
            }
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
                //genres = genres.Where(x =>x.Ganre.ToLower().Contains(searhGanre.Text.ToLower())).ToList();
          
                if(CBCatalog.SelectedIndex>0)
            {
                genres = genres.Where(x => x.catalogInt == CBCatalog.SelectedIndex).ToList();
            }
                if (CBPodCatalog.SelectedIndex > 0)
            {
                genres=genres.Where(x=>x.DirectoryAndSubDirectoryID==Convert.ToInt32(CBPodCatalog.SelectedValuePath)).ToList();
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
            private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            photo();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageEmployees(employees));
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

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
                subDirectories = subDirectories.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();

               
                for (int i = 0; i < subDirectories.Count; i++)
                {

                    CBPodCatalog.Items.Add(subDirectories[i].SubDirectory1);

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
            if (CBPodCatalog.SelectedIndex > 0)
            {
               
                List<SubDirectory> subDirectorie = BD.bD.SubDirectory.ToList();
                subDirectorie = subDirectorie.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                if (CBPodCatalog.SelectedIndex > 0)
                {
                    for (int i = 0; i < subDirectorie.Count; i++)
                    {
                        if (CBPodCatalog.SelectedIndex == i + 1)
                        {
                            CBPodCatalog.SelectedValuePath = Convert.ToString(subDirectorie[i].SubDirectoryID);
                        }

                    }
                }

            }
            else
            {
                CBPodCatalog.SelectedIndex = 0;
                
            }
            filter();
        }

      
    }
}