using LskConfig;



/// <summary>
/// Mob_mobteamConfig配置表
/// </summary>
public partial class Mob_mobteamConfig : TxtConfig<Mob_mobteamConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Mob_mobteam";
    }

    public static Mob_mobteam GetMobMobteam(int _id)
    {
        return Config._Mob_mobteam.Find(a => a.mobTeamID == _id);
    }

}
