using LskConfig;



/// <summary>
/// Invasion_invasionsetConfig配置表
/// </summary>
public partial class Invasion_invasionsetConfig : TxtConfig<Invasion_invasionsetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Invasion_invasionset";
    }

    public static Invasion_invasionset GetInvasionInvasionset(int _id)
    {
        return Config._Invasion_invasionset.Find(a => a.invasionSetID == _id);
    }
}
