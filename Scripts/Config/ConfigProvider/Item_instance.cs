using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Item_instanceConfig配置表
/// </summary>
public partial class Item_instanceConfig : TxtConfig<Item_instanceConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Item_instance";
    }

    public static Item_instance GetItemInstance(int _id)
    {
        return Config._Item_instance.Find(a => a.instanceID == _id);
    }

    public static  List<Item_instance>  GetItemInstances()
    {
        return Config._Item_instance;
    }
}
