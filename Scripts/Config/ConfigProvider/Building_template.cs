using LskConfig;
using System.Collections.Generic;

/// <summary>
/// Building_templateConfig配置表
/// </summary>
public partial class Building_templateConfig: TxtConfig<Building_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Building_template";
    }

    public static Building_template GetBuildingTemplate(int arcId)
    {
        Building_template bui = Config._Building_template.Find(a => a.templateID == arcId);
        if(bui != null)
        {
            return bui;
        }
        else
        {
            LogHelper_MC.Log("传入的建筑id有误：" + arcId);
            return null;
        }
    }

    public static bool HaveReachMaxLevel(int arcId,int level)
    {
        //Building_template bui = GetBuildingTemplate(arcId);
        //if(level>=bui.buildingLevelup[bui.buildingLevelup.Count - 1])
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        return false;
    }

    public static List<Building_template> GetAllBuiliding()
    {
        return Config._Building_template;
    }
}
