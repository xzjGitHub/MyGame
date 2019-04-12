using LskConfig;



/// <summary>
/// Material_templateConfig配置表
/// </summary>
public partial class Material_templateConfig : TxtConfig<Material_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Material_template";
    }

    public static Material_template GetMaterial_Template(int id)
    {
        return Config._Material_template.Find(a => a.instanceID == id);
    }
}
