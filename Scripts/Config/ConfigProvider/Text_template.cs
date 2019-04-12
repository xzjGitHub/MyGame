using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Text_templateConfig配置表
/// </summary>
public partial class Text_templateConfig : TxtConfig<Text_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Text_template";
    }
    public static Text_template GetText_config(int id)
    {
        return Config._Text_template.Find(a => a.textID == id);
    }

    public static List<string> GetStringList(List<int> ids)
    {
        List<string> list = new List<string>();
        for(int i = 0; i < ids.Count; i++)
        {
            list.Add(GetText_config(ids[i]).text);
        }
        return list;
    }
}
