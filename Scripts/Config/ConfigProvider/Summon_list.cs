using LskConfig;



/// <summary>
/// Summon_listConfig配置表
/// </summary>
public partial class Summon_listConfig : TxtConfig<Summon_listConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Summon_list";
    }

    public static Summon_list GetSummon_List(int id)
    {
        for (int i = 0; i < Config._Summon_list.Count; i++)
        {
            if (Config._Summon_list[i].ScriptID == id)
            {
                return Config._Summon_list[i];
            }
        }
        return null;
    }


}
