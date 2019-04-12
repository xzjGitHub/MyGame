using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// ConsumableAttribute
/// </summary>
public partial class ConsumableAttribute
{
    /// <summary>
    /// 消耗品模板
    /// </summary>
    public Consumable_instance consumable_instance;
    /// <summary>
    /// 最终奖励等级
    /// </summary>
    public float finalRewardLevel
    {
        get
        {
            return consumable_instance.baseRewardLevel[0];
        }
    }

}
