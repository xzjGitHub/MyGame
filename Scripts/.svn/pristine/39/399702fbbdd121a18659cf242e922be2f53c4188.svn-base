using System.Collections.Generic;

public class AtrDesInfo
{
    public string Des;
    public int Id;

    public AtrDesInfo() { }
}


public class EquipTipEx
{

    public static List<AtrDesInfo> GetEquipAttr(EquipAttribute attr)
    {
        List<AtrDesInfo> list = new List<AtrDesInfo>();

        Equip_template temp = Equip_templateConfig.GetEquip_template(attr.equipRnd.templateID);

        if(temp.equipSlot == (int)EquipPart.WuQi)
        {
            list.Add(GetDamgeInfo(attr));
        }

        List<Equip_display> disList = Equip_displayConfig.GetListByType(temp.displayType);
        for(int i = 0; i < disList.Count; i++)
        {
            float value = EquipAttributeUtil.GetAttrValue(attr,disList[i].field);
            if(value == 0)
                continue;

            AtrDesInfo info = new AtrDesInfo();
            info.Id = disList[i].id;
            float attrValue = (int)(value * 100) / (float)100;
            if(disList[i].isPercentage == 0)
            {
                if(disList[i].field == "finalItemLevel")
                {
                    if(attr.enchantRnd != null && attr.enchantRnd.enchantItemLevel > 0)
                    {
                        string s = "{0} + {1}";
                        string levelInfo = string.Format(GetDes(disList[i],(EquipPart)temp.equipSlot),attrValue);
                        info.Des = string.Format(s,levelInfo,attr.enchantRnd.finalItemLevel);
                    }
                    else
                    {
                        info.Des = string.Format(GetDes(disList[i],(EquipPart)temp.equipSlot),attrValue);
                    }
                }
                else
                {
                    info.Des = string.Format(GetDes(disList[i],(EquipPart)temp.equipSlot),attrValue);
                }
            }
            else
            {
                string newValue = Utility.GetPercent(attrValue,2);
                info.Des = string.Format(GetDes(disList[i],(EquipPart)temp.equipSlot),newValue);
            }
            list.Add(info);
        }
        return list;
    }


    private static string GetDes(Equip_display dis,EquipPart slot)
    {
        switch(slot)
        {
            case EquipPart.WuQi:
                return dis.weapon[0];
            case EquipPart.KuiJia:
                return dis.armor[0];
            case EquipPart.XiangLian:
                return dis.amulet[0];
            case EquipPart.JieZhi:
                return dis.ring[0];
            default:
                UnityEngine.Debug.LogError("错误的EquipSlot类型： " + (int)slot);
                return "{0}出错了。。";
        }
    }


    private static AtrDesInfo GetDamgeInfo(EquipAttribute attr)
    {
        AtrDesInfo info = new AtrDesInfo();
        info.Id = 1;
        float maxDamage = (int)(EquipAttributeUtil.GetAttrValue(attr,"maxDMG") * 100) / (float)100;
        float minDamage = (int)(EquipAttributeUtil.GetAttrValue(attr,"minDMG") * 100) / (float)100;
        info.Des = string.Format("伤害 {0}-{1}",minDamage,maxDamage);
        return info;
    }


    public static string GetEquipName(EquipAttribute attr)
    {
        string equipName = attr.GetItemData().itemName;
        if(string.IsNullOrEmpty(equipName))
        {
            Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
            equipName = item.itemName;
        }
        return equipName;
    }
}
