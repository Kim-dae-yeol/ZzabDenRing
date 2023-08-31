using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public interface IDataSource
{
    public Character[] GetCharacters();

    public void SaveData(Character[] characters);
}