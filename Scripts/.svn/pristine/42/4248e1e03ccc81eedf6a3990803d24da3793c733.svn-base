using LskConfig;



/// <summary>
/// Bounty_templateConfig配置表
/// </summary>
public partial class Bounty_templateConfig : TxtConfig<Bounty_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Bounty_template";
    }


    public static Bounty_template GetBounty_template(int _id)
    {
        return Config._Bounty_template.Find(a => a.bountyID == _id);
    }
}
