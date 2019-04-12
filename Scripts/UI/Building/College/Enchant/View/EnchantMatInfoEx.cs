using System.Collections.Generic;

public partial class EnchantAttriUtil
{
    public static List<AtrDesInfo> GetEquipAttr2(EquipAttribute attr1,EquipAttribute attr2 = null)
    {
        List<AtrDesInfo> list = new List<AtrDesInfo>();

        List<Enchant_display> allList = Enchant_displayConfig.GetList2();

        for(int i = 0; i < allList.Count; i++)
        {
            AtrDesInfo info = new AtrDesInfo();
            info.Id = allList[i].id;

            float value1 = GetAttrValue(attr1,allList[i].field);
            if(value1 <= 0)
                continue;
            if(allList[i].isPercentage == 0)
            {
                info.Des = string.Format(allList[i].enchant2[0],value1);
            }
            else
            {
                string newValue1 = Utility.GetPercent(value1,5); 
                info.Des = string.Format(allList[i].enchant2[0],newValue1);
            }
            list.Add(info);
        }
        return list;
    }

}

