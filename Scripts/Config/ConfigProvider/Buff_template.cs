using LskConfig;



/// <summary>
/// Buff_templateConfig配置表
/// </summary>
public partial class Buff_templateConfig: TxtConfig<Buff_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Buff_template";
    }

    public static Buff_template GetBuff_Template(int buffID)
    {
        return Config._Buff_template.Find(a => a.buffID == buffID);
    }

    public static int GetMaxBuffId()
    {
        return Config._Buff_template[Config._Buff_template.Count - 1].buffID;
    }
}
