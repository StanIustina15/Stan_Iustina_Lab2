using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stan_Iustina_Lab2.Models;

namespace Stan_Iustina_Lab2.Data
{
    public class Stan_Iustina_Lab2Context : DbContext
    {
        public Stan_Iustina_Lab2Context (DbContextOptions<Stan_Iustina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Stan_Iustina_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Stan_Iustina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Stan_Iustina_Lab2.Models.Author> Author { get; set; } = default!;
    }
}
