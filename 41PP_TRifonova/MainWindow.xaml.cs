﻿using System;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void btSotrudnik_Click(object sender, RoutedEventArgs e)
        {
        
            Avtorizatsia avtorizatsia = new Avtorizatsia();
            this.Close();
           avtorizatsia.ShowDialog();
            
        }

        private void btPolzpvatel_Click(object sender, RoutedEventArgs e)
        {
            WindowLibrary window = new WindowLibrary();
            this.Close();
            window.ShowDialog();
        }
    }
}
