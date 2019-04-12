using LskConfig;



/// <summary>
/// Mob_timelineConfig配置表
/// </summary>
public partial class Mob_timelineConfig : TxtConfig<Mob_timelineConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Mob_timeline";
    }

    public static Mob_timeline GetTimeline(int id)
    {
        return Config._Mob_timeline.Find(a => a.timeLineID == id);
    }
}
