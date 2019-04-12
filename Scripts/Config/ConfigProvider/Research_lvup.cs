using LskConfig;



/// <summary>
/// Research_lvupConfig配置表
/// </summary>
public partial class Research_lvupConfig: TxtConfig<Research_lvupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Research_lvup";
    }
    public static Research_lvup GetResearch_lvup(int level)
    {
        return Config._Research_lvup.Find(a => a.researchLevel == level);
    }

    public static Research_lvup GetLast()
    {
        return Config._Research_lvup[Config._Research_lvup.Count - 1];
    }

    public static int GetMaxLevel()
    {
        return Config._Research_lvup[Config._Research_lvup.Count - 1].researchLevel;
    }

    public static int GetNowLevelByExp(float exp,int nowLevel)
    {
        //检测的时候 做个优化 避免每次都完全遍历

        if(exp < Config._Research_lvup[0].lvupExp)
            return 0;

        if(exp == Config._Research_lvup[0].lvupExp)
            return Config._Research_lvup[0].researchLevel;

        if(exp >= Config._Research_lvup[Config._Research_lvup.Count - 2].lvupExp)
            return Config._Research_lvup[Config._Research_lvup.Count - 1].researchLevel;

        int level = nowLevel;
        int starCheckIndex = nowLevel==0?0:nowLevel;
        Research_lvup lvup = null;
        for(int i = starCheckIndex-1; i< Config._Research_lvup.Count-1; i++)
        {
            lvup = Config._Research_lvup[i];
            if (exp >= lvup.lvupExp)
            {
                level=lvup.researchLevel+1;
            }
            else
            {
                break;
            }
        }
        return level;
    }

    public static int GetMaxAddEnchantLevel(Research_lvup res)
    {
        if(res == null)
        {
            return 0;
        }
        else
        {
            return res.addEnchantLevel.Count < 2 ? 0 : res.addEnchantLevel[1];
        }
    }

    public static int GetMinxAddEnchantLevel(Research_lvup res)
    {
        if(res == null)
        {
            return 0;
        }
        else
        {
            return res.addEnchantLevel.Count < 2 ? 0 : res.addEnchantLevel[0];
        }
    }
}
