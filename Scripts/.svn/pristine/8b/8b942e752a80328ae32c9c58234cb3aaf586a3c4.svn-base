using LskConfig;



/// <summary>
/// Event_infoConfig配置表
/// </summary>
public partial class Event_infoConfig : TxtConfig<Event_infoConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Event_info";
    }

    public static Event_info GetEvent_Info(int type)
    {
        return Config._Event_info.Find(a => a.eventType == type);
    }
}
