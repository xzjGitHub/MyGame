using LskConfig;



/// <summary>
/// Shop_templateConfig配置表
/// </summary>
public partial class Shop_templateConfig : TxtConfig<Shop_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Shop_template";
    }

    public static Shop_template GetShop_template(int id)
    {
        return Config._Shop_template.Find(a => a.sellListID == id);
    }

    public static Shop_template GetShop_template(string id)
    {
        int finalId = int.Parse(id);

        return Config._Shop_template.Find(a => a.sellListID == finalId);
    }
}
