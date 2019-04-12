using System.Collections.Generic;

public partial class EnchantAttriUtil
{

    public static List<AtrDesInfo> GetEquipAttr(EquipAttribute attr,out string speDes)
    {
        List<AtrDesInfo> list = new List<AtrDesInfo>();

        List<AtrDesInfo> temp = new List<AtrDesInfo>();

        List<Enchant_display> allList = Enchant_displayConfig.GetList();

        for(int i = 0; i < allList.Count; i++)
        {
            float value = GetAttrValue(attr,allList[i].field);
            if(value <= 0)
                continue;
            AtrDesInfo info = new AtrDesInfo();
            info.Id = allList[i].id;
            //float attrValue = (int)(value * 100) / (float)100;
            if(allList[i].isPercentage == 0)
            {
                info.Des = string.Format(allList[i].enchant[0],value);
            }
            else
            {
                string newValue = Utility.GetPercent(value,5);
                info.Des = string.Format(allList[i].enchant[0],newValue);
            }

            if(allList[i].doudou == 0)
                list.Add(info);
            else
                temp.Add(info);
        }

        if(list.Count > 4)
        {
            LogHelperLSK.LogError("附魔基本属性超过四条，配置有问题");
        }
        if(temp.Count > 4)
        {
            LogHelperLSK.LogError("附魔特殊属性超过四条，配置有问题");
        }
        speDes = temp.Count > 0 ? temp[0].Des : string.Empty;
        return list;
    }

}

