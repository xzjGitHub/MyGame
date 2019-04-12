using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Parts_templateConfig配置表
/// </summary>
public partial class Parts_templateConfig: TxtConfig<Parts_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Parts_template";
    }

    public static Parts_template GetParts_template(int _id)
    {
        return Config._Parts_template.Find(a => a.instanceID == _id);
    }

    public static List<float> GetAddvalue(int _id)
    {
        List<float> list = new List<float>();
        Parts_template parts = GetParts_template(_id);
        list.Add(UnityEngine.Random.Range(parts.addEND[0],parts.addEND[1]));
        list.Add(UnityEngine.Random.Range(parts.addSHI[0],parts.addSHI[1]));
        list.Add(UnityEngine.Random.Range(parts.addARM[0],parts.addARM[1]));
        list.Add(UnityEngine.Random.Range(parts.addBLO[0],parts.addBLO[1]));
        list.Add(UnityEngine.Random.Range(parts.addAP[0],parts.addAP[1]));
        list.Add(UnityEngine.Random.Range(parts.addPRE[0],parts.addPRE[1]));
        list.Add(UnityEngine.Random.Range(parts.addCRT[0],parts.addCRT[1]));

        return list;
    }
}
