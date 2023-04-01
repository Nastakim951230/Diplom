using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibraryReaders
{
    public class LibraryReader
    {

        public static List<char> simvol = new List<char>() { 'A', 'B', 'E', 'K', 'M', 'H', 'O', 'P', 'C', 'T', 'Y', 'X' };

        //Метод для проверки правильности номера знака
        public static bool CheckNomer(string nomer)
        {
            try
            {
                Regex regex = new Regex("^[ABEKMHOPCTYX]{1}[0-9]{5}");
                bool a = regex.IsMatch(nomer);
                if (a)
                {

                    string registKod = nomer.Substring(nomer.Length - 5);
                    if (registKod == "00000")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public static string GetNextNomerAfter(string nomer)
        {
            try
            {
                string stringNomer = "";
                string registKod = nomer.Substring(nomer.Length - 5);
                if (registKod == "99999")
                {
                    registKod = "00001";
                    if (nomer[0] == simvol[simvol.Count - 1])
                    {
                        return "error";
                    }
                    else
                    {
                        int indexSimvola = -1;
                        for (int i = 0; i < simvol.Count; i++)
                        {
                            if (simvol[i] == nomer[0])
                            {
                                indexSimvola = i;
                            }
                        }
                        stringNomer = simvol[indexSimvola + 1] + registKod;

                    }
                }
                else
                {
                    int kod = Convert.ToInt32(registKod) + 1;
                    stringNomer = nomer[0] + "" + kod;

                }

                return stringNomer;
            }
            catch
            {
                return "error";

            }
        }


    }
}
