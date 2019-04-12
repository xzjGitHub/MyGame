using LskConfig;
using System.Collections.Generic;


/// <summary>
/// Craft_templateConfig配置表
/// </summary>
public partial class Craft_templateConfig: TxtConfig<Craft_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Craft_template";
    }

    public static Craft_template GetCraft_template(int id)
    {
        return Config._Craft_template.Find(a => a.craftID == id);
    }

    public static List<List<int>> GetItemCost(int id)
    {
        List<List<int>> list = new List<List<int>>();

        Craft_template craft_Template = GetCraft_template(id);

        //list.Add(craft_Template.itemCost1);
        //list.Add(craft_Template.itemCost2);
        //list.Add(craft_Template.itemCost3);

        return list;
    }
}
