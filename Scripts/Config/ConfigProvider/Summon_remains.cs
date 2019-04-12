using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Summon_remainsConfig配置表
/// </summary>
public partial class Summon_remainsConfig : TxtConfig<Summon_remainsConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Summon_remains";
    }

    public static Summon_remains GetSummon_Remains(int id)
    {
        for (int i = 0; i < Config._Summon_remains.Count; i++)
        {
            if (Config._Summon_remains[i].formulaID == id)
            {
                return Config._Summon_remains[i];
            }
        }
        return null;
    }

    public static List<Summon_remains> GetSummon_Remains()
    {
        return Config._Summon_remains;
    }
}
