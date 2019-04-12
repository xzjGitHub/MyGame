using LskConfig;



/// <summary>
/// Char_skillsetConfig配置表
/// </summary>
public partial class Char_skillsetConfig : TxtConfig<Char_skillsetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_skillset";
    }

    public static Char_skillset GetSkillset(int id)
    {
        return Config._Char_skillset.Find(a => a.skillSetID == id);
    }
}
