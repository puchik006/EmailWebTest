using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmailWebTest.Models
{
    public class EmailContext: DbContext
    {
        public DbSet<EmailData> EmailTable { get; set; }

        public EmailContext(DbContextOptions<EmailContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
