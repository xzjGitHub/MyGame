using LskConfig;



/// <summary>
/// Mob_skillsetConfig配置表
/// </summary>
public partial class Mob_skillsetConfig : TxtConfig<Mob_skillsetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Mob_skillset";
    }

    public static Mob_skillset GetMobSkillset(int id)
    {
        return Config._Mob_skillset.Find(a => a.skillSetID == id);
    }
}
