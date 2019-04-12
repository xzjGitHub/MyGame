using System;
using System.Collections.Generic;
using System.Linq;
using Char.View;

public enum PlayerType
{
    All,
    Defense,
    Attack,
    FuZhu,
    None = 100
}

public partial class CharSystem
{

    public List<CharAttribute> GetCharterListByType(PlayerType type = PlayerType.All)
    {
        List<CharAttribute> list = new List<CharAttribute>();
        if(type == PlayerType.All)
        {
            list.AddRange(charAttributeList.Values);
            return list;
        }

        foreach(var value in charAttributeList.Values)
        {
            if(value.char_template.charRole == (int)type)
            {
                list.Add(value);
            }
        }

        //return list.OrderByDescending(a => a.charLevel)
        //   .ThenByDescending(a => a.CharLevel)
        //   .ThenBy(a => a.char_template.templateID)
        //   .ToList();
        return list;
    }

    public List<CharAttribute> GetCharinCharList(PlayerType playerType)
    {
        List<CharAttribute> list = new List<CharAttribute>();
        foreach(var item in charAttributeList.Values)
        {
            if(item.Status == CharStatus.Idle || item.Status == CharStatus.Train)
            {
                if(playerType == PlayerType.All)
                {
                    list.Add(item);
                }
                else
                {
                    if(item.char_template.charRole == (int)playerType)
                    {
                        list.Add(item);
                    }
                }
            }
        }
        return list;
    }

    public List<CharAttribute> GetCharinCharListByStatusAndPlayerType(int status,PlayerType playerType)
    {
        List<CharAttribute> list = new List<CharAttribute>();
        foreach(var item in charAttributeList.Values)
        {
            if(((int)item.Status & status) > 0 &&
                item.char_template.charRole == (int)playerType)
            {
                list.Add(item);
            }
        }
        return list;
    }


    public void RemoveChar(int id)
    {
        if(charAttributeList.ContainsKey(id))
        {
            if(charAttributeList[id].Status == CharStatus.Idle)
            {
                CharAttribute attr = charAttributeList[id];
                for(int i = 0; i < attr.equipAttribute.Count; i++)
                {
                    CharStripEquipment(attr.equipAttribute[i].itemID,id);
                }
                charAttributeList.Remove(id);
            }
        }
    }

    public Dictionary<EquipPart,EquipmentData> GetCharAllEquipPartInfo(int charId)
    {
        Dictionary<EquipPart,EquipmentData> dict = new Dictionary<EquipPart,EquipmentData>();
        dict.Add(EquipPart.JiaoBu,null);
        dict.Add(EquipPart.JieZhi,null);
        dict.Add(EquipPart.KuiJia,null);
        dict.Add(EquipPart.TouKui,null);
        dict.Add(EquipPart.WuQi,null);
        dict.Add(EquipPart.XiangLian,null);

        List<EquipAttribute> list = CharAttributeList[charId].equipAttribute;
        for(int i = 0; i < list.Count; i++)
        {
            int part = Equip_templateConfig.GetEquip_template(list[i].equipRnd.templateID).equipSlot;
            dict[(EquipPart)part] = list[i].GetItemData() as EquipmentData;
        }

        return dict;
    }

    /// <summary>
    /// 获取显示的主动技能列表
    /// </summary>
    /// <param name="attr"></param>
    /// <returns></returns>
    public List<int> GetCharShowActiveSkill(CharAttribute attr)
    {
        /*
         * 第1个技能：manualSkill [0]
         * 第2个技能：manualSkill [1]
         * 第3个技能：commonSkill[0]
         * 第4个技能：如果charClass=6，则需要额外显示1个治疗技能，治疗技能是combatSkillList中，index = 1的技能 
         */
        List<int> list = new List<int>();
        if(attr.ManualSkills.Count > 0)
        {
            list.Add(attr.ManualSkills[0]);
        }
        if(attr.ManualSkills.Count > 1)
        {
            list.Add(attr.ManualSkills[1]);
        }
        if(attr.CommonSkills.Count > 1)
        {
            if(attr.CommonSkills[0] != 0)
                list.Add(attr.CommonSkills[0]);
        }
        if(attr.char_template.charClass == 6)
        {
            if(attr.CombatSkills.Count > 1)
            {
                list.Add(attr.CombatSkills[1]);
            }
        }
        return list;
    }
}
