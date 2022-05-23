using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CardDesigner.DataAccess.DbContexts
{
    public class CardDesignerDbContextFactory
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public CardDesignerDbContextFactory(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public CardDesignerDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString).Options;

            return new CardDesignerDbContext(options, _mapper);
        }
    }
}