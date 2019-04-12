using LskConfig;
using System.Collections.Generic;


/// <summary>
/// EnchantType_templateConfig配置表
/// </summary>
public partial class EnchantType_templateConfig: TxtConfig<EnchantType_templateConfig>
{

    protected override void Init()
    {
        base.Init();
        Info.Name = "EnchantType_template";
    }

    public static EnchantType_template GetEnchant_Template(int id)
    {
        return Config._EnchantType_template.Find(a => a.enchantTypeID == id);
    }

    public static List<EnchantType_template> GetAll()
    {
        return Config._EnchantType_template;
    }
}

