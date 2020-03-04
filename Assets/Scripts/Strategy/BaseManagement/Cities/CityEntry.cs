using SwordAndBored.GameData.Units;
using UnityEngine;

public class CityEntry : ScriptableObject
{
    public ITown town;

    public string townName;

    public void Init(ITown town)
    {
        this.town = town;
        this.townName = town.Name;
    }

    public static CityEntry CreateInstance(ITown town)
    {
        var data = ScriptableObject.CreateInstance<CityEntry>();
        data.Init(town);
        return data;
    }
}
