using LskConfig;



/// <summary>
/// Event_selectionConfig配置表
/// </summary>
public partial class Event_selectionConfig : TxtConfig<Event_selectionConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Event_selection";
    }

    public static Event_selection GetSelection(int id)
    {
        return Config._Event_selection.Find(a => a.selectionID == id);
    }
}
