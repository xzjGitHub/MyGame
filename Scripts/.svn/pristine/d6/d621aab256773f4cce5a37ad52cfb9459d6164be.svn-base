using LskConfig;



/// <summary>
/// EventOptionsIntroConfig配置表
/// </summary>
public partial class EventOptionsIntroConfig : TxtConfig<EventOptionsIntroConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "EventOptionsIntro";
    }

    public static EventOptionsIntro GetEventOptionsIntro(int _id)
    {
        return Config._EventOptionsIntro.Find(a => a.ID == _id);
    }
}
