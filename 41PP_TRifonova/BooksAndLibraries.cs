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
    
    public partial class BooksAndLibraries
    {
        public int BookAndLibrary { get; set; }
        public int IDBook { get; set; }
        public int IDLibrary { get; set; }
        public int count { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual Libraries Libraries { get; set; }
    }
}
