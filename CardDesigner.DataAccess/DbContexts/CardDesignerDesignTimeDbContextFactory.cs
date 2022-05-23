using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Data.DbContexts
{
    public class CardDesignerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CardDesignerDbContext>
    {
        public CardDesignerDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=carddesign.db").Options;
            return new CardDesignerDbContext(options);
        }
    }
}