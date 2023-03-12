using Microsoft.EntityFrameworkCore;

namespace CardDesigner.DataAccess.DbContexts
{
    public class CardDesignerDbContextFactory
    {
        #region Private fields

        private readonly string _connectionString;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="mapper">Mapper object</param>
        public CardDesignerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Create Database Context Factory method
        /// </summary>
        /// <returns></returns>
        public CardDesignerDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString).Options;

            return new CardDesignerDbContext(options);
        }
    }
}