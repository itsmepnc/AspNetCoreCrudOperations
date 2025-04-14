using AspNetCoreCRUD.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNetCoreCRUD.DbConnection
{
    public class DbConnect : DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
            
        }
        public DbSet<StudentRegistration> StudentRegistration { get; set; }
    }
}
