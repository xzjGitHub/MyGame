using LskConfig;



/// <summary>
/// EventDisplayTemplateConfig配置表
/// </summary>
public partial class EventDisplayTemplateConfig : TxtConfig<EventDisplayTemplateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "EventDisplayTemplate";
    }

    public static EventDisplayTemplate GetEventDisplayTemplate(int _id)
    {
        return Config._EventDisplayTemplate.Find(a => a.sortingOrder == _id);
    }
}
