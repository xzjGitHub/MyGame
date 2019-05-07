using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// itemCommonAttribute
/// </summary>
public partial class itemCommonAttribute
{
    /// <summary>
    /// 物品模板
    /// </summary>
    public Item_instance item_instance;
    /// <summary>
    /// 最终售价
    /// </summary>
    public float finalSellPrice
    {
        get
        {
            return (float)(item_instance.baseSellPrice);
        }
    }

}
