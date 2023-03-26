using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41PP_TRifonova
{
    public partial class Genres
    {
        public string catalogGamr
            {
            get
            {
                string catalog="";
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.Where(x => x.SubDirectoryID == DirectoryAndSubDirectoryID).ToList();
                for(int i = 0; i < subDirectories.Count; i++)
                {
                    catalog = subDirectories[i].Catalogs.catalog;
                    
                }
                return catalog;
            }
            }
        public int catalogInt
        {
            get
            {
                int catalog = 0;
                List<SubDirectory> subDirectories = BD.bD.SubDirectory.Where(x => x.SubDirectoryID == DirectoryAndSubDirectoryID).ToList();
                for (int i = 0; i < subDirectories.Count; i++)
                {
                    catalog = subDirectories[i].Catalogs.CatalogID;

                }
                return catalog;
            }
        }

    }
}
