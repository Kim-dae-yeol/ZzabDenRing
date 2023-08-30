using ZzabDenRing.Data;

namespace ZzabDenRing.Di;

public static class Container
{
    private static Repository _repo = Repository.GetInstance();


    public static Repository GetRepository() => _repo;
}