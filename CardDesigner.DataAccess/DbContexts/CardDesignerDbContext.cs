using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Entities;

namespace CardDesigner.Data.DbContexts
{
    public class CardDesignerDbContext : DbContext
    {
        public CardDesignerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
    }
}