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
    
    public partial class BooksAndAuthors
    {
        public int BookAndAuthorID { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
    
        public virtual Authors Authors { get; set; }
        public virtual Books Books { get; set; }
    }
}
