namespace KL.Persistence
{
  public static class ConfigureDatabase
  {
    public static string DbConnectionString
    {
      get { return "Data Source=ADMIN\\SQLEXPRESS2008R2;Initial Catalog=DevelopmentDB;User ID=sa;Password=Password1!"; }
    }
  }
}
