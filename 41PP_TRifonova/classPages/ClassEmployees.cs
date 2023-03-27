using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41PP_TRifonova
{
    public partial class Employees
    {
        public string fio
        {
            get
            {
                return Surname + " " + Name + " " + Otchestvo;
            }
        }
    }
}
