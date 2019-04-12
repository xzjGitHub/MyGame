using LskConfig;



/// <summary>
/// MR_templateConfig配置表
/// </summary>
public partial class MR_templateConfig : TxtConfig<MR_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "MR_template";
    }

    public static MR_template GetTemplate(int id)
    {
        return Config._MR_template.Find(a => a.instanceID == id);
    }

}
