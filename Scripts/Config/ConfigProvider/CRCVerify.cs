using LskConfig;
using System.Collections.Generic;

/// <summary>
/// CRCVerifyConfig配置表
/// </summary>
public partial class CRCVerifyConfig : TxtConfig<CRCVerifyConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "CRCVerify";
    }

    public static List<CRCVerify> GetCRCVerifys()
    {
        return Config._CRCVerify;
    }
}
