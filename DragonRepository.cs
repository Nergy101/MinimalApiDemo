using System.Data.SQLite;

namespace MinimalApiDemo;
public class DragonRepository : IRepository<Dragon>
{
    public List<Dragon> Dragons { get; set; } = new List<Dragon>();

    public Dragon? GetById(long id)
    {
        return Dragons.SingleOrDefault(d => d.Id == id);
    }

    public void Add(Dragon dragon)
    {
        Dragons.Add(dragon);
    }

    public List<Dragon> GetAll()
    {
        return Dragons;
    }

    public Dragon Update(Dragon updateEntity)
    {
        Dragons[Dragons.FindIndex(d => d.Id == updateEntity.Id)] = updateEntity;
        return updateEntity;
    }

    public void Delete(long id)
    {
        Dragons.RemoveAt(Dragons.FindIndex(d => d.Id == id));
    }
}

public interface IRepository<T> where T : class
{
    List<T> GetAll();
    T? GetById(long id);
    void Add(T entity);
    T Update(T updateEntity);
    void Delete(long id);
}

public class SqLiteBaseRepository
{
    public static string DbFile
    {
        get { return Environment.CurrentDirectory + "\\SimpleDb.sqlite"; }
    }

    public static SQLiteConnection SimpleDbConnection()
    {
        return new SQLiteConnection("Data Source=" + DbFile);
    }
}
