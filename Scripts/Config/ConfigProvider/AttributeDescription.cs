using LskConfig;



/// <summary>
/// AttributeDescriptionConfig配置表
/// </summary>
public partial class AttributeDescriptionConfig : TxtConfig<AttributeDescriptionConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "AttributeDescription";
    }

    public static AttributeDescription GetAttributeDescription(int _id)
    {
        return Config._AttributeDescription.Find(a => a.ID == _id);
    }
}
