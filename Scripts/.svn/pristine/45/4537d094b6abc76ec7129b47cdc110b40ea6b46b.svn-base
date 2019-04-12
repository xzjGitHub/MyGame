using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Tag_templateConfig配置表
/// </summary>
public partial class Tag_templateConfig : TxtConfig<Tag_templateConfig>
{
    //[建筑等级，tag列表]
    private static Dictionary<int,List<Tag_template>> allTagsInfo = new Dictionary<int,List<Tag_template>>();
    protected override void Init()
    {
        base.Init();
        Info.Name = "Tag_template";
    }

    public static Tag_template GetTemplate(int id)
    {
        return Config._Tag_template.Find(a => a.tagID == id);
    }

    public static Dictionary<int,List<Tag_template>> GetTagsInfo(List<int> tags)
    {
        if(allTagsInfo.Count == 0)
        {
            for(int i = 0; i < tags.Count; i++)
            {
                Tag_template tag_Template = GetTemplate(tags[i]);
                if (allTagsInfo.ContainsKey(tag_Template.tagPosition))
                {
                    allTagsInfo[tag_Template.tagPosition].Add(tag_Template);
                }
                else
                {
                    List<Tag_template> list = new List<Tag_template>();
                    list.Add(tag_Template);
                    allTagsInfo[tag_Template.tagPosition] = list;
                }
            }
        }
        return allTagsInfo.OrderBy(o => o.Key).ToDictionary(o => o.Key,p => p.Value); ;
    }
}

