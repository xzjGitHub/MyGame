using LskConfig;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Enchant_displayConfig配置表
/// </summary>
public partial class Enchant_displayConfig: TxtConfig<Enchant_displayConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Enchant_display";
    }


    public static Enchant_display GetEnchant_display(int id)
    {
        return Config._Enchant_display.Find(a => a.id == id);
    }

    public static List<Enchant_display> GetAllEnchant_display()
    {
        return Config._Enchant_display;
    }


    public static List<Enchant_display> GetList()
    {
        //List<Enchant_display> list = new List<Enchant_display>();
        //for(int i = 1; i < Config._Enchant_display.Count; i++)
        //{
        //    list.Add(Config._Enchant_display[i]);
        //}
        return Config._Enchant_display.OrderBy(a => int.Parse(a.enchant[1])).ToList();
    }

    public static List<Enchant_display> GetList2()
    {
        //List<Enchant_display> list = new List<Enchant_display>();
        //for(int i = 1; i < Config._Enchant_display.Count; i++)
        //{
        //    list.Add(Config._Enchant_display[i]);
        //}
        return Config._Enchant_display.OrderBy(a => int.Parse(a.enchant2[1])).ToList();
    }

}
