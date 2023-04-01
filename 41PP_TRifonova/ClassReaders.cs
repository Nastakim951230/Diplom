using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _41PP_TRifonova
{
    public partial class Reader

    {
        public string Nomer
        {
            get
            {
                return "Номер читательского билета: " + LibraryCardNumber;
            }
        }
        
        public SolidColorBrush brush
        {
            get
            {
                var brush = new BrushConverter();
                DateTime date = ReissuanceDate;
                date = date.AddYears(1);
                if (date<=DateTime.Today)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#CCE9F5");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#FFFFFF");
                }
            }
        }
        public SolidColorBrush pereregistr
        {
            get
            {
                var brush = new BrushConverter();
                DateTime date = ReissuanceDate;
                date = date.AddYears(1);
                if (date <= DateTime.Today)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#fc1703");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#225496");
                }
            }
        }
        public string dateRegistr
        {
            get
            {
                string date = String.Format("{0:dd.MM.yyyy}",DateOfIssue);
                return "Дата регистрации: " + date;
            }
        }

        public string id
        {
            get
            {
                string idReader = Convert.ToString(LibraryCardNumber);
                return "Дата регистрации: " + idReader;
            }
        }
        public string datePeregistr
        {
            get
            {
                string datepereregistr = String.Format("{0:dd.MM.yyyy}", ReissuanceDate);
                return "Дата переригистрации: " + datepereregistr;
            }
        }
    }
}
