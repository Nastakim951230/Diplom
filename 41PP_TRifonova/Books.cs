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
    
    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            this.Application = new HashSet<Application>();
            this.BooksAndAuthors = new HashSet<BooksAndAuthors>();
            this.BooksAndGanres = new HashSet<BooksAndGanres>();
            this.BooksAndLibraries = new HashSet<BooksAndLibraries>();
        }
    
        public int BookID { get; set; }
        public string Nazvanie { get; set; }
        public int IDPublishingHouse { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public Nullable<int> RestrictionsID { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual AgeRestrictions AgeRestrictions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Application { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksAndAuthors> BooksAndAuthors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksAndGanres> BooksAndGanres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksAndLibraries> BooksAndLibraries { get; set; }
    }
}
