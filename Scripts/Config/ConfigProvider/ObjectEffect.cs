using LskConfig;



/// <summary>
/// EffectObjectConfigConfig配置表
/// </summary>
public partial class ObjectEffectConfig : TxtConfig<ObjectEffectConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "ObjectEffect";
    }

    public static ObjectEffect GetEffectObjectConfig(int id)
    {
        return Config._ObjectEffect.Find(a => a.effectObjectID == id);
    }
}
