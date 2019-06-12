﻿using System;
using System.Collections.Generic;
using System.Linq;


public partial class TeamAttribute
{

    /// <summary>
    /// 队伍id
    /// </summary>
    public int teamId;
    /// <summary>
    /// 冷却
    /// </summary>
    public float finalCharCooldown;
    //todo 队伍先攻值
    /// <summary>
    /// 队伍先攻值
    /// </summary>
    public int FinalTeamInitiative { get { return _finalTeamInitiative; } }

    public List<CombatUnit> combatUnits = new List<CombatUnit>();

    /// <summary>
    /// 队伍等级
    /// </summary>
    public float teamLevel
    {
        get
        {
            return charAttribute.Sum(a => a.charLevel) / (float)charAttribute.Count;
        }
    }

    /// <summary>
    /// 设置先攻值
    /// </summary>
    /// <param name="value"></param>
    public void SetTeamInitiative(int value)
    {
        _finalTeamInitiative = value;
    }

    /// <summary>
    /// 包含角色职业
    /// </summary>
    /// <param name="classs"></param>
    /// <returns></returns>
    public bool IsContainCharClass(List<int> classs)
    {
        if (classs == null || classs.Count == 0)
        {
            return true;
        }
        foreach (CombatUnit item in combatUnits)
        {
            if (classs.Contains(item.charAttribute.char_template.charClass))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 包含角色种族
    /// </summary>
    /// <param name="races"></param>
    /// <returns></returns>
    public bool IsContainCharRace(List<int> races)
    {
        if (races == null || races.Count == 0)
        {
            return true;
        }
        foreach (CombatUnit item in combatUnits)
        {
            if (races.Contains(item.charAttribute.char_template.charRace))
            {
                return true;
            }
        }
        return false;
    }


    public TeamAttribute(List<CharAttribute> _charAttributes)
    {
        charAttribute = new List<CharAttribute>();
        charAttribute.Clear();
        charAttribute.AddRange(_charAttributes);

        for (int i = 0; i < charAttribute.Count; i++)
        {
            combatUnits.Add(new CombatUnit(charAttribute[i], i));
        }
    }

    public TeamAttribute(List<CombatUnit> combatUnits,int teamInitiative)
    {
        charAttribute = new List<CharAttribute>();
        combatUnits = new List<CombatUnit>();
        for (int i = 0; i < combatUnits.Count; i++)
        {
            combatUnits.Add(combatUnits[i]);
            charAttribute.Add(combatUnits[i].charAttribute);
        }

        _finalTeamInitiative = teamInitiative;
    }

    public TeamAttribute()
    {
        charAttribute = new List<CharAttribute>();
        combatUnits = new List<CombatUnit>();
    }

    /// <summary>
    /// 设置角色列表
    /// </summary>
    /// <param name="_charIds"></param>
    public void SetCharList(List<int> _charIds)
    {
        combatUnits.Clear();
        charAttribute.Clear();
        foreach (int item in _charIds)
        {
            charAttribute.Add(new CharAttribute(CharSystem.Instance.GetCharAttribute(item).GetCharData()));
        }
        //
        for (int i = 0; i < charAttribute.Count; i++)
        {
            combatUnits.Add(new CombatUnit(charAttribute[i], i));
        }
    }

    /// <summary>
    /// 设置角色列表
    /// </summary>
    public void SetCharList(List<CharAttribute> _charAttributes)
    {
        combatUnits.Clear();
        charAttribute.Clear();
        foreach (CharAttribute item in _charAttributes)
        {
            charAttribute.Add(new CharAttribute(item.GetCharData()));
        }
        for (int i = 0; i < charAttribute.Count; i++)
        {
            combatUnits.Add(new CombatUnit(charAttribute[i], i));
        }
    }

    /// <summary>
    /// 设置角色列表
    /// </summary>
    public void SetCharList(List<CombatUnit> _combatUnits)
    {
        charAttribute.Clear();
        combatUnits.Clear();
        //
        foreach (CombatUnit item in _combatUnits)
        {
            charAttribute.Add(item.charAttribute);
        }
        //
        combatUnits.AddRange(_combatUnits);
        combatUnits = combatUnits.OrderBy(a => a.initIndex).ToList();
    }

    /// <summary>
    /// 移除队伍
    /// </summary>
    public void RemoveTeam(CharAttribute _charAttribute)
    {
        for (int i = 0; i < charAttribute.Count; i++)
        {
            if (charAttribute[i].charID != _charAttribute.charID)
            {
                continue;
            }

            _charAttribute.SetCharSate(CharStatus.Idle);
            charAttribute.Remove(charAttribute[i]);
            combatUnits.Remove(combatUnits[i]);
            break;
        }
    }

    /// <summary>
    /// 更新角色血量
    /// </summary>
    /// <param name="_list"></param>
    public void UpdateCharHp(List<CombatUnit> _list)
    {
        foreach (CombatUnit item in _list)
        {
            foreach (CombatUnit _unit in combatUnits)
            {
                if (_unit.teamId != item.teamId || _unit.charAttribute.charID != item.charAttribute.charID || _unit.initIndex != item.initIndex)
                {
                    continue;
                }

                _unit.hp = item.hp;
                break;
            }
        }
    }

    /// <summary>
    /// 更新角色休息状态
    /// </summary>
    public void UpdateCharRestState()
    {
        foreach (CombatUnit _unit in combatUnits)
        {
            _unit.charAttribute.SetCharSate(_unit.hp == 0 ? CharStatus.Die : CharStatus.CombatRest);
        }
    }

    /// <summary>
    /// 战斗角色复活
    /// </summary>
    /// <param name="resurrectProp"></param>
    public void CombatRevivableChar(float resurrectProp)
    {
        foreach (CombatUnit item in combatUnits)
        {
            if (item.hp > 0)
            {
                continue;
            }
            item.hp = (int)Math.Max(item.charAttribute.finalHP * resurrectProp, 1);
        }
    }

    /// <summary>
    /// 使用生命球
    /// </summary>
    public void UseGlobHealing(float _value)
    {
        foreach (CombatUnit _unit in combatUnits)
        {
            _unit.hp += (int)(_value * _unit.charAttribute.finalHP);
            _unit.hp = Math.Min(_unit.maxHp, _unit.hp);
            break;
        }
    }
    /// <summary>
    /// 损失生命
    /// </summary>
    public void CharLossHP(float _value)
    {
        foreach (CombatUnit _unit in combatUnits)
        {
            if (_unit.hp <= 0)
            {
                continue;
            }

            _unit.hp -= (int)(_value * _unit.charAttribute.finalHP);
            _unit.hp = Math.Max(0, _unit.hp);
            break;
        }
    }

    public bool IsEventVisitHPCost(float visitHPCost)
    {
        return visitHPCost < combatUnits.Sum(a => a.hp);
    }

    /// <summary>
    /// 事件访问生命消耗
    /// </summary>
    /// <param name="visitHPCost"></param>
    public bool EventVisitHPCost(float visitHPCost, out List<int> list)
    {
        list = new List<int>();
        //全部死完
        if (visitHPCost >= combatUnits.Sum(a => a.hp))
        {
            return false;
            foreach (CombatUnit item in combatUnits)
            {
                if (list != null)
                {
                    list.Add(item.hp);
                }

                item.hp = 0;
                item.charAttribute.SetCharSate(CharStatus.Die);
            }
        }
        //
        Dictionary<int, int> chars = new Dictionary<int, int>();
        foreach (CombatUnit item in combatUnits)
        {
            chars.Add(item.charAttribute.charID, item.hp);
        }
        //排序
        chars = (from entry in chars
                 orderby entry.Value
                 ascending
                 select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
        List<int> keys = new List<int>();
        keys.AddRange(chars.Keys);
        Dictionary<int, int> hps = new Dictionary<int, int>();
        float sumMaxHP = combatUnits.Sum(a => a.hp);
        foreach (int item in keys)
        {
            CombatUnit combatUnit = combatUnits.Find(a => a.charAttribute.charID == item);
            //
            int _temp = Math.Min((combatUnit.hp - 1), (int)(combatUnit.hp / sumMaxHP * visitHPCost));
            hps.Add(combatUnit.initIndex, _temp);
            combatUnit.hp -= _temp;
            //更新数据
            visitHPCost -= _temp;
            sumMaxHP = combatUnits.Sum(a => a.hp);
        }
        //排序
        hps = (from entry in hps
               orderby entry.Key
                 ascending
               select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
        if (list != null)
        {
            list.AddRange(hps.Values);
        }

        return true;
    }

    private CharAttribute GetCobatCharAttribute(int _id)
    {
        CharAttribute _attribute = CharSystem.Instance.GetCharAttribute(_id);
        _attribute.SetCharSate(CharStatus.InCombat);
        return _attribute;
    }


    //
    private int _finalTeamInitiative;
}


