﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseBD : DbContext
    {
        public BaseBD()
            : base("name=BaseBD")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AgeRestrictions> AgeRestrictions { get; set; }
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BooksAndAuthors> BooksAndAuthors { get; set; }
        public virtual DbSet<BooksAndGanres> BooksAndGanres { get; set; }
        public virtual DbSet<BooksAndLibraries> BooksAndLibraries { get; set; }
        public virtual DbSet<Catalogs> Catalogs { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Libraries> Libraries { get; set; }
        public virtual DbSet<PublishingHouse> PublishingHouse { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubDirectory> SubDirectory { get; set; }
    }
}
