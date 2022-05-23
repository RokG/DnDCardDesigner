using CardDesigner.Domain.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CardDesigner.DataAccess.DbContexts
{
    public class CardDesignerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CardDesignerDbContext>
    {
        public CardDesignerDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=carddesign.db").Options;

            return new CardDesignerDbContext(options, CardDesignerMapper.CreateMapper());
        }
    }
}