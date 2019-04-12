using LskConfig;
using System.Collections.Generic;


/// <summary>
/// Case_templateConfig配置表
/// </summary>
public partial class Case_templateConfig : TxtConfig<Case_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Case_template";
    }

    public static Case_template GetCase_template(int id)
    {
        return Config._Case_template.Find(a => a.caseID == id);
    }

    public static List<int> GetAllId()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < Config._Case_template.Count; i++)
        {
            list.Add(Config._Case_template[i].caseID);
        }
        return list;
    }
}
