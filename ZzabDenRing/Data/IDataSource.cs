using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public interface IDataSource
{
    public Task<Character[]> GetCharacters();

    public Task SaveData(Character[] characters);
}