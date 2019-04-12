using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Fort_templateConfig配置表
/// </summary>
public partial class Fort_templateConfig : TxtConfig<Fort_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Fort_template";
    }

    public static Fort_template GetFort_template(int _id)
    {
        return Config._Fort_template.Find(a => a.fortID == _id);
    }

    public static List<Fort_template> GetFort_List()
    {
        return Config._Fort_template;
    }
}
