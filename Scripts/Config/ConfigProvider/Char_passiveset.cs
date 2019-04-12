using LskConfig;



/// <summary>
/// Char_passivesetConfig配置表
/// </summary>
public partial class Char_passivesetConfig : TxtConfig<Char_passivesetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_passiveset";
    }

    public static Char_passiveset GetPassiveset(int id)
    {
        return Config._Char_passiveset.Find(a => a.passiveSetID == id);
    }
}
