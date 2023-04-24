using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> EmpOptions)
            : base(EmpOptions)
        {

        }

        //Letting the DB context know we have two maore tables 
        public DbSet<Employees> EmployeesDB { get; set; }
        public DbSet<Users> EmpUsers { get; set; }

        //establishing a communication within the database
        //go to the startup class


    }
}
