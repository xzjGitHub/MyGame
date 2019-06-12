using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public partial class MobTeamAttribute
{
    public List<CombatUnit> CombatUnits;
    public int FinalTeamInitiative { get { return _finalTeamInitiative; } }


    public int MobTeamID { get { return mobTeamID; } }

    public MobTeamAttribute(int mobTeamID, int mobLevel = 1, int addTeamInitiative = 0)
    {
        this.mobLevel = mobLevel;
        this.mobTeamID = mobTeamID;
        mob_mobteam = Mob_mobteamConfig.GetMobMobteam(mobTeamID);
        if (mob_mobteam == null)
        {
            return;
        }
        CombatUnits = CreatemMobUnits();
        _finalTeamInitiative = mob_mobteam.baseTeamInitiative + addTeamInitiative;
    }

    /// <summary>
    /// 创建怪物
    /// </summary>
    /// <returns></returns>
    private List<CombatUnit> CreatemMobUnits()
    {
        List<CombatUnit> units = new List<CombatUnit>();
        int index = 0;
        foreach (int item in mob_mobteam.mobList)
        {
            units.Add(new CombatUnit(new MobAttribute(new CharCreate(item, mobLevel)), index, mobTeamID));
            index++;
        }
        return units;
    }

    //
    private int mobLevel;
    private int mobTeamID;
    private int _finalTeamInitiative;
}

