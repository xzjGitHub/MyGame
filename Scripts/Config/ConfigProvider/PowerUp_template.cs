using LskConfig;



/// <summary>
/// PowerUp_templateConfig配置表
/// </summary>
public partial class PowerUp_templateConfig : TxtConfig<PowerUp_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "PowerUp_template";
    }

    public static PowerUp_template GeTemplate(int id)
    {
        return Config._PowerUp_template.Find(a => a.powerUpID == id);
    }
}
