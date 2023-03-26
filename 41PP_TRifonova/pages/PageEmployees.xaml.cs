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
    /// Логика взаимодействия для PageEmployees.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        Employees employees;
        public PageEmployees(Employees employees)
        {
            InitializeComponent();
            this.employees = employees;
            string name="";
            string othestvo="";
            for(int i=0; i<employees.Name.Length; i++)
            {
                if(i ==0)
                {
                    name+= employees.Name[i];
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
            FIO.Text = employees.Surname + " "+name+". "+othestvo+".";

            //Заполнение списка каталог
            CBCatalog.Items.Add("Все каталоги");
          List<Catalogs> catalogs=BD.bD.Catalogs.ToList();
            for(int i = 0; i < catalogs.Count; i++)
            {
                CBCatalog.Items.Add(catalogs[i].catalog);
            }
            CBCatalog.SelectedIndex = 0;

            //Заполнение списка подкаталог
            CBPodCatalog.Items.Add("Все подкаталоги");
            List<SubDirectory> subDirectories = BD.bD.SubDirectory.ToList();
            for (int i = 0; i < subDirectories.Count; i++)
            {
                CBPodCatalog.Items.Add(subDirectories[i].SubDirectory1);
            }
            CBPodCatalog.SelectedIndex = 0;

            //Заполнение списка жанр
            CBGanre.Items.Add("Все жанры");
            List<Genres> genres = BD.bD.Genres.ToList();
            for (int i = 0; i < genres.Count; i++)
            {
                CBGanre.Items.Add(genres[i].Ganre);
            }
            CBGanre.SelectedIndex = 0;


        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {

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

                //CBPodCatalog.ItemsSource = subDirectories;

                //CBPodCatalog.DisplayMemberPath = "SubDirectory1";
                //CBPodCatalog.SelectedValuePath = "SubDirectoryID";
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
                CBGanre.Visibility = Visibility.Collapsed;
            }
        }

        private void CBPodCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBPodCatalog.SelectedIndex > 0)
            {
                List<Genres> genres = BD.bD.Genres.ToList();

                for (int i = 0; i < genres.Count; i++)
                {
                    CBGanre.Items.Remove(genres[i].Ganre);
                }
                List<SubDirectory> subDirectorie = BD.bD.SubDirectory.ToList();
                subDirectorie = subDirectorie.Where(x => x.IDCatolog == CBCatalog.SelectedIndex).ToList();
                if (CBPodCatalog.SelectedIndex > 0)
                {
                    for (int i = 0; i < subDirectorie.Count; i++)
                    {
                        if (CBPodCatalog.SelectedIndex == i+1)
                        {
                            CBPodCatalog.SelectedValuePath = Convert.ToString(subDirectorie[i].SubDirectoryID);
                        }

                    }

                    genres = genres.Where(x => x.DirectoryAndSubDirectoryID == Convert.ToInt32(CBPodCatalog.SelectedValuePath)).ToList();
                    if (genres.Count > 1)
                    {
                        for (int i = 0; i < genres.Count; i++)
                        {

                            CBGanre.Items.Add(genres[i].Ganre);
                        }

                        CBGanre.SelectedIndex = 0;
                        CBGanre.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CBGanre.Visibility = Visibility.Collapsed;
                    }
                }
                
            }
            else
            {
                CBPodCatalog.SelectedIndex = 0;
                CBGanre.Visibility = Visibility.Collapsed;
            }
        }

        private void CBGanre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            
     
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            //Application.Current.Shutdown();
           
        }

        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.per.Navigate(new PageAddBook(employees));
        }
    }
}
