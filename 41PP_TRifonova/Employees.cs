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
    
    public partial class Employees
    {
        public int EmployeeID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public int LibraryID { get; set; }
        public string Telefon { get; set; }
        public int IDGender { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual Libraries Libraries { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
