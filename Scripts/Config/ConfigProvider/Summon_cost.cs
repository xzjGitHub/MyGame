using LskConfig;



/// <summary>
/// Summon_costConfig配置表
/// </summary>
public partial class Summon_costConfig : TxtConfig<Summon_costConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Summon_cost";
    }

    public static Summon_cost GetSummon_Cost(int id)
    {
        for (int i = 0; i < Config._Summon_cost.Count; i++)
        {
            if (Config._Summon_cost[i].charID == id)
            {
                return Config._Summon_cost[i];
            }
        }
        return null;
    }
}
