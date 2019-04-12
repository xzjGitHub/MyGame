using LskConfig;



/// <summary>
/// PerkSet_listConfig配置表
/// </summary>
public partial class PerkSet_listConfig : TxtConfig<PerkSet_listConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "PerkSet_list";
    }

    public static PerkSet_list GetPerkSet_List(int id)
    {
        return Config._PerkSet_list.Find(a => a.PerkSetID == id);
    }
}
