using LskConfig;
using System.Collections.Generic;

/// <summary>
/// StateMap_templateConfig配置表
/// </summary>
public partial class StateMap_templateConfig: TxtConfig<StateMap_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "StateMap_template";
    }

    public static StateMap_template GetState_template(int id)
    {
        return Config._StateMap_template.Find(a => a.StateID == id);
    }

    public static List<StateMap_template> GetAllMap()
    {
        return Config._StateMap_template;
    }
}
