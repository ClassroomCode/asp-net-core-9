using EComm.Entities;

namespace EComm.DataAccess;

public static class ECommDbFactory
{
    public static IECommDb Create() =>
        new ECommDb();
}
