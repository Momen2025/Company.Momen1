﻿using Company.Momen1.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.DAL.Data.Contexts
{
    //CLR
   public class CompanyDbContext : IdentityDbContext<AppUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) :base(options)
        {

                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyMomen1; Trusted_Connection = True; TrustServerCertificate = True ");
        //}
        public DbSet<Department>  Departments { get; set; }
        public DbSet<Employee>  Employees { get; set; }


    }
}
