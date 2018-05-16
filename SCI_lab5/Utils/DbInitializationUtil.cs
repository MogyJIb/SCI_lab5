using DbDataLibrary.Data;

namespace SCI_lab5.Utils
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
