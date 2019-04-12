using LskConfig;



/// <summary>
/// Token_costConfig配置表
/// </summary>
public partial class Token_costConfig : TxtConfig<Token_costConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Token_cost";
    }

    public static Token_cost GetToken_Cost()
    {
        return Config._Token_cost[0];
    }

}
