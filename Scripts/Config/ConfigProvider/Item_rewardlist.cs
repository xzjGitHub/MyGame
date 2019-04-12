using LskConfig;



/// <summary>
/// Item_rewardlistConfig配置表
/// </summary>
public partial class Item_rewardlistConfig : TxtConfig<Item_rewardlistConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Item_rewardlist";
    }


    public static Item_rewardlist GetItemRewardlist(int id)
    {
        return Config._Item_rewardlist.Find(a => a.rewardListID == id);
    }
}
