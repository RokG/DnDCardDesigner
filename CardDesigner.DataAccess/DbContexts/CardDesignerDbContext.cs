using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardDesigner.Domain.Models;

namespace CardDesigner.Data.DbContexts
{
    public class CardDesignerDbContext : DbContext
    {
        public DbSet<CharacterModel> Characters { get; set; }
    }
}