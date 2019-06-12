using LskConfig;



/// <summary>
/// Script_storyLineConfig配置表
/// </summary>
public partial class Script_storyLineConfig: TxtConfig<Script_storyLineConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Script_storyLine";
    }

    public static Script_storyLine GeScript_storyLine(int _templateID)
    {
        return Config._Script_storyLine.Find(a => a.storyLineID == _templateID);
    }

    public static Script_storyLine GetScByUid(int uid)
    {
        for(int i = 0; i < Config._Script_storyLine.Count; i++)
        {
            if(Config._Script_storyLine[i].uid == uid)
            {
                return Config._Script_storyLine[i];
            }
        }
        return null;
    }
}
