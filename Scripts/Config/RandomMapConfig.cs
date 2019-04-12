using System;
using System.Collections.Generic;
using System.Linq;
using LskConfig;

public partial class RandomMapConfigConfig : TxtConfig<RandomMapConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "RandomMapConfig";
    }

    public static List<RandomMapConfig> GetRandomMapConfig(int _mapID)
    {
        return Config._randomMapConfigs.Where(item => item.mapID == _mapID).ToList().OrderBy(a => a.WPId).ToList(); ; 
    }

}

