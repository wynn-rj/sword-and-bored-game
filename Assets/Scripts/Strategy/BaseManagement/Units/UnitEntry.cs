using SwordAndBored.GameData.Units;
using UnityEngine;

public class UnitEntry : ScriptableObject
{
    public string UnitName;
    public string Role;
    public string CurrentCity;

    private IStats stats;
    private int id;

    public void Init(int id, IUnit unit, IStats stats)
    {
        this.id = id;
        this.Role = unit.Role.Name;
        this.UnitName = unit.Name;
        /*
         * TO DO: parse units and stats into string data
         */
        //this.UnitName = unit.Name;
        //this.Role = unit.Role.Name;
    }

    public static UnitEntry CreateInstance(int id, IUnit unit, IStats stats)
    {
        var data = ScriptableObject.CreateInstance<UnitEntry>();
        data.Init(id, unit, stats);
        return data;
    }
}
