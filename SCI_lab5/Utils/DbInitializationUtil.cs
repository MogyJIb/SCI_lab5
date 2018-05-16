using DbDataLibrary.Data;

namespace lab4.Utils
{
    public class DbInitializationUtil
    {
        public static void Init()
        {
            ToursSqliteDbContext dbContext = new ToursSqliteDbContext();
            DbInitializer<ToursSqliteDbContext>.Initialize(dbContext);
        }
   
    }
}
