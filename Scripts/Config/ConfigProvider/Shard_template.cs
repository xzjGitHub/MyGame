using LskConfig;



/// <summary>
/// Shard_templateConfig配置表
/// </summary>
public partial class Shard_templateConfig : TxtConfig<Shard_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Shard_template";
    }

    public static Shard_template GetShard_Template(int id)
    {
        return Config._Shard_template.Find(a => a.instanceID == id);
    }
}
