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
    
    public partial class IssueOrReturn
    {
        public int IssueOrReturnID { get; set; }
        public int IDBook { get; set; }
        public int IDReader { get; set; }
        public int IDLibrary { get; set; }
        public int countBooks { get; set; }
        public System.DateTime DateOfIssue { get; set; }
        public System.DateTime ReturnDate { get; set; }
    }
}