using System.Collections.Generic;
using System.Linq;

public enum EquipCategory
{
    All,
    /// <summary>
    /// 武器
    /// </summary>
    Weapon,
    /// <summary>
    /// 盔甲
    /// </summary>
    armor,
    /// <summary>
    /// 头盔
    /// </summary>
    Helmet,
    /// <summary>
    /// 靴子
    /// </summary>
    Boots,
    /// <summary>
    /// 项链
    /// </summary>
    Necklace,
    /// <summary>
    /// 戒指
    /// </summary>
    Ring
}


public partial class ItemSystem
{
    public EquipAttribute GetEquipAttribute(int id)
    {
        return itemAttributes[id] as EquipAttribute;
    }

    public ItemAttribute GetItemAttributeByTemplateId(int templateId)
    {
        ItemAttribute itemAttribute = null;
        foreach(var item in itemAttributes)
        {
            if(item.Value.instanceID == templateId)
            {
                itemAttribute = item.Value;
                break;
            }
        }
        return itemAttribute;
    }

    public List<ItemAttribute> GetWuQiList(CharAttribute attr = null)
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        foreach(var item in itemAttributes)
        {
            if(attr != null)
            {
                Item_instance it = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
                if(attr.charID != it.charIDReq && it.charIDReq != 0)
                    continue;
            }

            EquipAttribute equipAttribute = item.Value as EquipAttribute;
            if(equipAttribute != null)
            {
                Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttribute.equipRnd.templateID);
                if(equip.equipSlot == (int)EquipCategory.Weapon)
                {
                    if(attr != null)
                    {
                        if(attr.char_template.weaponType == equipAttribute.equipRnd.equip_template.equipType)
                            list.Add(item.Value);
                    }
                    else
                    {
                        list.Add(item.Value);
                    }
                }
            }
        }
        return list;
    }

    public List<ItemAttribute> GetHuJiaList(CharAttribute attr = null)
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        foreach(var item in itemAttributes)
        {
            if(attr != null)
            {
                Item_instance it = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
                if(attr.charID != it.charIDReq && it.charIDReq != 0)
                    continue;
            }

            EquipAttribute equipAttribute = item.Value as EquipAttribute;
            if(equipAttribute != null)
            {
                Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttribute.equipRnd.templateID);
                if(equip.equipSlot == (int)EquipCategory.Helmet ||
                    equip.equipSlot == (int)EquipCategory.armor ||
                    equip.equipSlot == (int)EquipCategory.Boots)
                {
                    //if(attr != null)
                    //{
                    //    if(attr.char_template.weaponType == equipAttribute.equipRnd.equip_template.equipType)
                    //        list.Add(item.Value);
                    //}
                    //else
                    //{
                    //    list.Add(item.Value);
                    //}

                    list.Add(item.Value);
                }
            }
        }
        return list;
    }

    public List<ItemAttribute> GetSiPinList(CharAttribute attr = null)
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        List<ItemAttribute> list1 = new List<ItemAttribute>();
        List<ItemAttribute> list2 = new List<ItemAttribute>();
        foreach(var item in itemAttributes)
        {

            if(attr != null)
            {
                Item_instance it = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
                if(attr.charID != it.charIDReq && it.charIDReq != 0)
                    continue;
            }

            EquipAttribute equipAttribute = item.Value as EquipAttribute;
            if(equipAttribute != null)
            {
                Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttribute.equipRnd.templateID);
                if(equip.equipSlot == (int)EquipCategory.Necklace)
                {
                    //if(attr != null)
                    //{
                    //    if(attr.char_template.weaponType == equipAttribute.equipRnd.equip_template.equipType)
                    //        list.Add(item.Value);
                    //}
                    //else
                    //{
                    //    list.Add(item.Value);
                    //}
                    list1.Add(item.Value);
                }

                if(equip.equipSlot == (int)EquipCategory.Ring)
                {
                    //if(attr != null)
                    //{
                    //    if(attr.char_template.weaponType == equipAttribute.equipRnd.equip_template.equipType)
                    //        list.Add(item.Value);
                    //}
                    //else
                    //{
                    //    list.Add(item.Value);
                    //}

                    list2.Add(item.Value);
                }
            }
        }
        list.AddRange(list1);
        list.AddRange(list2);
        return list;
    }

    public List<ItemAttribute> GetMatList()
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        foreach(var item in itemAttributes)
        {
            Item_instance item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)ItemType.ZhuCai ||
                item_Instance.itemType == (int)ItemType.FuCai ||
                item_Instance.itemType == (int)ItemType.XiSu)
            {
                list.Add(new ItemAttribute(item.Value.GetItemData()));
            }
        }
        return list;
    }

    public List<ItemAttribute> GetItemListByItemType(ItemType itemType)
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        Item_instance item_Instance = null;
        foreach(var item in itemAttributes)
        {
            item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)itemType)
            {
                list.Add(new ItemAttribute(item.Value.GetItemData()));
            }
        }
        return list;
    }

    public List<ItemAttribute> GetRecastItemList()
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        Item_instance item_Instance = null;
        foreach(var item in itemAttributes)
        {
            item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)ItemType.YiWu || item_Instance.isRelic==1)
            {
                list.Add(new ItemAttribute(item.Value.GetItemData()));
            }
        }
        return list;
    }


    public List<ItemAttribute> GetZaWu()
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        Item_instance item_Instance = null;
        foreach(var item in itemAttributes)
        {
            EquipAttribute equip = item.Value as EquipAttribute;
            if(equip != null)
                continue;

            item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)ItemType.ZhuCai)
                continue;
            if(item_Instance.itemType == (int)ItemType.FuCai)
                continue;
            if(item_Instance.itemType == (int)ItemType.XiSu)
                continue;

            list.Add(new ItemAttribute(item.Value.GetItemData()));
        }
        return list;
    }

    public List<ItemAttribute> GetAll()
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        list.AddRange(itemAttributes.Values);
        return list;
    }

    public void EquipEnchant(int equipId,int rareId,int researchLevel)
    {
        (itemAttributes[equipId] as EquipAttribute).EquipEnchanted(rareId,researchLevel);
    }

    public List<ItemAttribute> GetAllEquip(CharAttribute attr = null)
    {
        List<ItemAttribute> list = new List<ItemAttribute>();
        foreach(var values in itemAttributes)
        {
            if(attr != null)
            {
                Item_instance item = Item_instanceConfig.GetItemInstance(values.Value.instanceID);
                if(attr.charID != item.charIDReq && item.charIDReq != 0)
                    continue;
            }

            EquipAttribute equipAttribute = values.Value as EquipAttribute;
            if(equipAttribute != null)
            {
                if(attr != null)
                {
                    if(equipAttribute.equipRnd.equip_template.equipSlot == (int)EquipCategory.Weapon)
                    {
                        if(attr.char_template.weaponType == equipAttribute.equipRnd.equip_template.equipType)
                            list.Add(values.Value);
                    }
                    else
                    {
                        list.Add(values.Value);
                    }
                }
                else
                {
                    list.Add(values.Value);
                }
            }
        }
        return list;
    }

    /// <summary>
    /// 移除物品
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="sum"></param>
    public void RemoveItemByuTemplateId(int templateId,int sum)
    {
        ItemAttribute attr = GetItemAttributeByTemplateId(templateId);
        attr.sum -= sum;
        if(attr.sum <= 0)
        {
            itemAttributes.Remove(attr.itemID);
            return;
        }
        CheckRemainsBounty(attr);
    }



    public List<ItemAttribute> GetEquipMakeZc(ItemType itemType,int forgeId)
    {
        Forge_config forgeCofig = Forge_configConfig.GetForge_config(forgeId);
        List<ItemAttribute> list = new List<ItemAttribute>();
        Item_instance item_Instance = null;
        Material_template mat = null;
        foreach(var item in itemAttributes)
        {
            item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)itemType)
            {
                mat = Material_templateConfig.GetMaterial_Template(item.Value.instanceID);
                if(mat!=null) {
                    if(forgeCofig.materialType.Contains(mat.materialType))
                        list.Add(new ItemAttribute(item.Value.GetItemData()));
                }
            }
        }
        return list;
    }

    public List<ItemAttribute> GetEquipMakeFc(ItemType itemType,int forgeId)
    {
        Forge_config forgeCofig = Forge_configConfig.GetForge_config(forgeId);
        List<ItemAttribute> list = new List<ItemAttribute>();
        Item_instance item_Instance = null;
        Parts_template part = null;
        foreach(var item in itemAttributes)
        {
            item_Instance = Item_instanceConfig.GetItemInstance(item.Value.instanceID);
            if(item_Instance.itemType == (int)itemType)
            {
                part = Parts_templateConfig.GetParts_template(item.Value.instanceID);
                if(part!=null) {
                    if(forgeCofig.partsType.Contains(part.partsType))
                        list.Add(new ItemAttribute(item.Value.GetItemData()));
                }
            }
        }
        return list;
    }
}
