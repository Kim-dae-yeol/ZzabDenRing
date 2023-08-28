namespace ZzabDenRing.Data;

public class Repository
{
    private IDataSource _source;

    public Repository(IDataSource source)
    {
        _source = source;
    }
}