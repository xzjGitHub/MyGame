using LskConfig;



/// <summary>
/// CharShow_templateConfig配置表
/// </summary>
public partial class CharRPackConfig : TxtConfig<CharRPackConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "CharRPack";
    }

    public static CharRPack GeCharShowTemplate(int _templateID)
    {
        return Config._CharRPack.Find(a => a.templateD == _templateID);
    }
}
