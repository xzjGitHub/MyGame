using System;
using System.Collections.Generic;
using System.Linq;

public partial class CharSystem
{
    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="_charAttribute">角色属性</param>
    public void AddChar(CharAttribute _charAttribute)
    {
        var charData = _charAttribute.GetCharData();
        charData.charID = charAttributeList.Count == 0 ? 1 : charAttributeList.Last().Key + 1;
        charAttributeList.Add(charData.charID, new CharAttribute(charData));
    }


    /// <summary>
    /// 创建角色_直接
    /// </summary>
    public CharAttribute CreateChar(CharCreate create, bool isSave = true)
    {
        create.charID = charAttributeList.Count == 0 ? 1 : charAttributeList.Last().Key + 1;
        if (!isSave)
        {
            return new CharAttribute(create);
        }
        charAttributeList.Add(create.charID, new CharAttribute(create));
        return charAttributeList[create.charID];
    }
    /// <summary>
    /// 角色添加经验
    /// </summary>
    public CharUpgradeInfo CharAddExp(int _charId, float _exp)
    {
        if (!charAttributeList.ContainsKey(_charId)) return null;
        return charAttributeList[_charId].SetCharExp(_exp);
    }
    /// <summary>
    /// 创建角色
    /// </summary>
    public void CreateChar(List<CharCreate> creates)
    {
        foreach (var item in creates)
        {
            CreateChar(item);
        }
    }
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="charId"></param>
    public void DeleteChar(int charId)
    {
        if (!charAttributeList.ContainsKey(charId)) return;

        charAttributeList.Remove(charId);
    }

    public Action<int, CharStatus> CharStatusChangeEvent;
    public void SetCharSate(int _charId, CharStatus _type)
    {
        if (!charAttributeList.ContainsKey(_charId)) return;
        charAttributeList[_charId].SetCharSate(_type);
        if (CharStatusChangeEvent != null)
        {
            CharStatusChangeEvent(_charId, _type);
        }
    }
    /// <summary>
    /// 角色穿戴装备
    /// </summary>
    /// <param name="_equipmentID"></param>
    /// <param name="_charID"></param>
    public void CharWearEquipment(int _equipmentID, int _charID)
    {
        EquipAttribute _equipAttribute = ItemSystem.Instance.GetItemAttribute(_equipmentID) as EquipAttribute;
        //
        //穿戴装备
        ItemSystem.Instance.WearEquipment(_equipmentID, _charID);
        charAttributeList[_charID].CharWearEquipment(_equipAttribute);
    }
    /// <summary>
    /// 角色卸载装备
    /// </summary>
    /// <param name="_equipmentID"></param>
    /// <param name="_charID"></param>
    public void CharStripEquipment(int _equipmentID, int _charID)
    {
        ItemSystem.Instance.RemoveEquipment(_equipmentID);
        //
        charAttributeList[_charID].CharStripEquipment(_equipmentID);
        //
    }

    /// <summary>
    /// 清除战斗休息状态
    /// </summary>
    public void ClearCombatRestStatus()
    {
        foreach (var item in CharAttributeList)
        {
            if (item.Value.Status != CharStatus.CombatRest) continue;
            item.Value.SetCharSate(CharStatus.Idle);
        }
    }
    /// <summary>
    /// 清除角色远征位置
    /// </summary>
    public void ClearExpeditionPos()
    {
        foreach (var item in CharAttributeList)
        {
            if (item.Value.Pos != CharPos.Expedition) continue;
            item.Value.SetCharPos(CharPos.Idle);
            if (item.Value.Status != CharStatus.CombatRest) continue;
            item.Value.SetCharSate(CharStatus.Idle);
        }
    }

    /// <summary>
    /// 更新角色位置
    /// </summary>
    /// <param name="_charId"></param>
    /// <param name="_charPos"></param>
    public void UpdateCharPos(int _charId, CharPos _charPos)
    {
        if (!charAttributeList.ContainsKey(_charId)) return;
        charAttributeList[_charId].SetCharPos(_charPos);
    }

    /// <summary>
    /// 添加角色buff
    /// </summary>
    public void AddBuff(int _charId, BuffInfo _buffInfo)
    {
        if (!charAttributeList.ContainsKey(_charId)) return;
        charAttributeList[_charId].AddBuff(_buffInfo);
    }
    /// <summary>
    /// 移除角色buff
    /// </summary>
    public void RemoveBuff(int _charId, int _buffId)
    {
        if (!charAttributeList.ContainsKey(_charId)) return;
        charAttributeList[_charId].RemoveBuff(_buffId, ScriptTimeSystem.Instance.Second);
    }

}
