using LskConfig;



/// <summary>
/// Event_templateConfig配置表
/// </summary>
public partial class Event_templateConfig : TxtConfig<Event_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Event_template";
    }

    public static Event_template GetEventTemplate(int _id)
    {
        return Config._Event_template.Find(a => a.eventID == _id);
    }
}
