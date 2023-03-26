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
using System.Windows.Shapes;

namespace _41PP_TRifonova
{
    /// <summary>
    /// Логика взаимодействия для WindowEmployees.xaml
    /// </summary>
    public partial class WindowEmployees : Window
    {
        Employees employees;
     
        public WindowEmployees(Employees employees)
        {
            InitializeComponent();
            BD.bD = new BaseBD();
            perehod.Navigate(new PageEmployees(employees));
            FrameNavigate.per = perehod;
            

            this.employees = employees;
            
           
        }

      

    
    }
}
