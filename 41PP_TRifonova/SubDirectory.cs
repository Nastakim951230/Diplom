//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _41PP_TRifonova
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubDirectory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubDirectory()
        {
            this.BooksAndGanres = new HashSet<BooksAndGanres>();
            this.Genres = new HashSet<Genres>();
        }
    
        public int SubDirectoryID { get; set; }
        public int IDCatolog { get; set; }
        public string SubDirectory1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksAndGanres> BooksAndGanres { get; set; }
        public virtual Catalogs Catalogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genres> Genres { get; set; }
    }
}
