using LskConfig;



/// <summary>
/// Bounty_bountySetConfig配置表
/// </summary>
public partial class Bounty_bountySetConfig : TxtConfig<Bounty_bountySetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Bounty_bountySet";
    }

    public static Bounty_bountySet GetBountyBountySet(int _id)
    {
        return Config._Bounty_bountySet.Find(a => a.bountySetID == _id);
    }
}
