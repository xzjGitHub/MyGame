using LskConfig;



/// <summary>
/// ResultEffectConfigConfig配置表
/// </summary>
public partial class ResultEffectConfigConfig : TxtConfig<ResultEffectConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "ResultEffectConfig";
    }

    public static ResultEffectConfig GetResultEffectConfig(int id)
    {
        return Config._ResultEffectConfig.Find(a => a.resultTypeID == id);
    }
}
