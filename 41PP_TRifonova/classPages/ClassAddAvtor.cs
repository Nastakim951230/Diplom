using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41PP_TRifonova
{
    public partial class Authors
    {

        public string avtor
        { 
        get
            {
               
                //Вывод ФИО сотрудника
                if (OthestvoAuthor == null)
                {
                    return SurnameAuthor + " " + NameAuthor;
                }
               else
                {
                    return SurnameAuthor + " " + NameAuthor + " " + OthestvoAuthor ;
                }
            }
        }
            
    }
}
