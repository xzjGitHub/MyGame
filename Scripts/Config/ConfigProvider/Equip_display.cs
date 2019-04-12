using System.Collections.Generic;
using LskConfig;
using System.Linq;


/// <summary>
/// Equip_displayConfig配置表
/// </summary>
public partial class Equip_displayConfig: TxtConfig<Equip_displayConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Equip_display";
    }

    public static Equip_display GetEquip_instance(int id)
    {
        return Config._Equip_display.Find(a => a.id == id);
    }

    public static List<Equip_display> GetList()
    {
        Config._Equip_display.OrderBy(a => a.amulet);
        return Config._Equip_display;
    }

    public static List<Equip_display> GetListByType(int equipType)
    {
        List<Equip_display> list = new List<Equip_display>();
        for(int i = 1; i < Config._Equip_display.Count; i++)
        {
            if(Config._Equip_display[i].field != "minDMG" && Config._Equip_display[i].field != "maxDMG"
                && Config._Equip_display[i].field != "tempAttack")
                list.Add(Config._Equip_display[i]);
        }
        switch(equipType)
        {
            case 1:
                return list.OrderBy(a => int.Parse(a.weapon[1])).ToList();
            case 2:
                return list.OrderBy(a => int.Parse(a.armor[1])).ToList();
            case 3:
                return list.OrderBy(a => int.Parse(a.amulet[1])).ToList();
            case 4:
                return list.OrderBy(a => int.Parse(a.ring[1])).ToList();
            default:
                throw new System.Exception("没有这个装备类型: " + equipType);
        }
    }
}

