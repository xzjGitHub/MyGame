using LskConfig;



/// <summary>
/// Dialog_templateConfig配置表
/// </summary>
public partial class Dialog_templateConfig : TxtConfig<Dialog_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Dialog_template";
    }

    public static Dialog_template GetDialog_template(int _id)
    {
        return Config._Dialog_template.Find(a => a.dialogID == _id);
    }
}
