using EComm.Entities;

namespace EComm.DataAccess;

public static class ECommDbFactory
{
    public static IECommDb Create(string connStr)
    {
        var db = new ECommDb(connStr);
        db.Database.EnsureCreated();
        return db;
    }
}
