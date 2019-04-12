using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;

public partial class CharAttribute
{
    /// <summary>
    /// 角色升级
    /// </summary>
    public Char_lvup char_lvup { get { return Char_lvupConfig.GetChar_Lvup(charLevel); } }
    /// <summary>
    /// 角色状态
    /// </summary>
    public CharStatus Status { get { return charStatus; } }
    /// <summary>
    /// 角色位置
    /// </summary>
    public CharPos Pos { get { return charPos; } }

    /// <summary>
    /// buff列表
    /// </summary>
    public List<BuffInfo> Buffs { get { return buffs; } }

    /// <summary>
    /// 清除战斗休息状态
    /// </summary>
    public void ClearCombatRest()
    {
        if (charStatus != CharStatus.CombatRest)
        {
            return;
        }

        charStatus = CharStatus.Idle;
    }
    /// <summary>
    /// 设置角色状态类型
    /// </summary>
    /// <param name="_type"></param>
    public void SetCharSate(CharStatus _type)
    {
        charStatus = _type;
    }
    /// <summary>
    /// 设置角色位置
    /// </summary>
    /// <param name="_type"></param>
    public void SetCharPos(CharPos _type)
    {
        charPos = _type;
    }
    /// <summary>
    /// 设置最终能量
    /// </summary>
    /// <param name="value"></param>
    public void SetFinalEncourage(int value,int addIndex=-1)
    {
        if (!addFinalEncourageIndexs.Contains(addIndex))
        {
            addFinalEncourageIndexs.Add(addIndex);
        }
        finalEncourage += value;
        finalEncourage = Math.Max(0, finalEncourage);
    }

    /// <summary>
    /// 设置经验
    /// </summary>
    public CharUpgradeInfo SetCharExp(float _exp)
    {
        charExp += _exp;
        CharUpgradeInfo _info = new CharUpgradeInfo(this);
        charLevel = Char_lvupConfig.GetCharNowLevel(charExp);
        //UnityEngine.Debug.LogError("当前经验： " + charExp + "  当前等级: " + charLevel);
        //foreach (Char_lvup item in Char_lvupConfig.GetChar_Lvups())
        //{
        //    if (charExp == item.levelupExp)
        //    {
        //        charLevel = item.charLevel + 1;
        //        break;
        //    }
        //    if (charExp < item.levelupExp && item.charLevel != 1)
        //    {
        //        charLevel = item.charLevel;
        //        break;
        //    }
        //}
        _info.UpdateUpgradeInfo(this);
        //
        if (_info.charNowLevel - _info.initLevel <= 0)
        {
            return _info;
        }
        //
        CharUpgrades(_info.charNowLevel - _info.initLevel);
        return _info;
    }


    public void SetPersonality(int passiveSkillID)
    {
        passiveSkills.Remove(personalityAddPassiveSkill);
        //添加被动
        personalityAddPassiveSkill = passiveSkillID;
        PassiveSkill_template template = PassiveSkill_templateConfig.GetPassiveSkill_Template(personalityAddPassiveSkill);
        personalityType = template.reqType;
        passiveSkills.Add(personalityAddPassiveSkill);
        attitudeID = template.personalityID;
    }



    /// <summary>
    /// 角色穿戴装备
    /// </summary>
    /// <param name="_equipAttribute"></param>
    public void CharWearEquipment(EquipAttribute _equipAttribute)
    {
        if (equipAttribute == null || _equipAttribute == null)
        {
            return;
        }

        equipAttribute.Add(_equipAttribute);
    }

    /// <summary>
    /// 角色卸载装备
    /// </summary>
    /// <param name="itemId"></param>
    public void CharStripEquipment(int itemId)
    {
        if (equipAttribute == null)
        {
            return;
        }

        for (int i = 0; i < equipAttribute.Count; i++)
        {
            if (equipAttribute[i].itemID != itemId)
            {
                continue;
            }

            equipAttribute.RemoveAt(i);
            //
            break;
        }
    }

    /// <summary>
    /// 角色卸载所有装备
    /// </summary>
    public void CharStripAllEquipment()
    {
        if (equipAttribute == null)
        {
            return;
        }

        equipAttribute.Clear();
    }
    /// <summary>
    /// 添加角色buff
    /// </summary>
    public void AddBuff(BuffInfo buffInfo)
    {
        buffs.Add(buffInfo);
    }
    /// <summary>
    /// 移除角色buff
    /// </summary>
    public void RemoveBuff(int buffId, float time)
    {
        buffs.Remove(buffs.Find(a => a.buffId == buffId));
        if (buffs.Count < 1)
        {
            return;
        }

        buffs.Last().createTime = time;
    }

    #region 创建角色
    /// <summary>
    /// 创建角色
    /// </summary>
    public CharAttribute(CharCreate create)
    {
        char_config = Char_configConfig.GetConfig();
        cbt_config = Combat_configConfig.GetCombat_config();
        charID = create.charID;
        charExp = create.charExp;
        charLevel = create.charLevel;
        templateID = create.templateID;
        char_template = Char_templateConfig.GetTemplate(templateID);
        if (char_template == null)
        {
          //  LogHelperLSK.LogError("角色不存在，Id: " + templateID);

            return;
        }
        charName = char_template.charName;
        charStatus = CharStatus.Idle;
        charPos = CharPos.Idle;
        charType = CombatCharType.Char;
        //
        finalUpgradeDefSum = finalUpgradeDef;
        finalUpgradeOffSum = finalUpgradeOff;
        charQuality = (float)Math.Log((finalUpgradeDefSum + finalUpgradeOffSum) / 2f, char_config.upgradeRate);
        //军衔
        isCommander = RandomBuilder.RandomIndex_Chances(char_template.cmderChance) == 0;
        //得到性格
        if (create.initialPersonality==0)
        {
            List<int> temp = RandomBuilder.RandomList(1, char_template.personalityList);
            if (temp!=null)
            {
                attitudeID = temp[0];
            }
        }
        //初始化性格
        InitialPersonality(attitudeID);
        //添加额外强化
        if (isCommander)
        {
            powerUpIDs.AddRange(cbt_config.commandPU);
        }
        //从小到大
        powerUpIDs = powerUpIDs.OrderBy(a => a).ToList();
        //
        //计算最小、大升级
        switch (create.createType)
        {
            case CharCreateType.Initialize:
                minUpgrade = char_template.upgrade[0];
                maxUpgrade = char_template.upgrade[1];
                break;
            case CharCreateType.Award:
                minUpgrade = RandomBuilder.RandomNum(CoreSystem.Instance.GetCoreLvup().minUpgrade);
                maxUpgrade = CoreSystem.Instance.GetCoreLvup().maxUpgrade;
                break;
            case CharCreateType.Summon:
                minUpgrade = RandomBuilder.RandomNum(CoreSystem.Instance.GetCoreLvup().minUpgrade);
                maxUpgrade = CoreSystem.Instance.GetCoreLvup().maxUpgrade;
                break;
            case CharCreateType.Quest:
                minUpgrade = char_template.upgrade[0];
                maxUpgrade = char_template.upgrade[1];
                break;
        }
        //初始化属性
        GetUpgrade();
        charHP += baseCharHP;
        charAttack += baseCharAttack;
        LogHelperLSK.Log("charID=" + templateID + "    0级" + "charHP=" + charHP + "   charAttack=" + charAttack);
        //计算升级
        CalculateUpgrade(1);
        //
        LogHelperLSK.Log("charID=" + templateID + "    1级" + "charHP=" + charHP + "   charAttack=" + charAttack);
        //初始化强化属性
        rndATTs.AddRange(GetRandomNums());
        rndATTs.AddRange(GetRandomNums());
        //

        charRank1 = (CharRank)RandomBuilder.RandomIndex_Chances(char_template.charRankChance);
        //
        //添加配置技能
        manualSkills.AddRange(char_template.manualSkillList);
        combatSkills.AddRange(char_template.combatSkillList);
        tacticalSkills.AddRange(char_template.tacticalSkillList);
        commonSkills.Add(char_template.commonSkillList);
        if (isCommander)
        {
            commonSkills.Add(cbt_config.commandSkill);
        }
        //随机技能
        List<int> skills = RandomBuilder.RandomValues(char_template.randomSkillSetList, 1);
        if (skills != null && skills.Count > 0)
        {
            int skillsetID = skills[0];
            Char_skillset skillset = Char_skillsetConfig.GetSkillset(skillsetID);
            if (skillset != null)
            {
                manualSkills.Add(skillset.manualSkill);
                tacticalSkills = SkillsAddOperation(skillset.tacticalSkill, tacticalSkills);
            }
        }
        UpdateEquipAddSkill();
        //
        InitPassiveAttribute();
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    public CharAttribute(CharData data)
    {
        CreateBaseInfo(data);
        //
        CerateEquipAttribute(data.equipID);

    }
    /// <summary>
    /// 创建角色_装备列表(物品系统中取装备)
    /// </summary>  
    public CharAttribute(CharData data, List<int> equips)
    {
        CreateBaseInfo(data);
        //
        CerateEquipAttribute(equips);
        //添加技能属性
        CreateSkillAttribute();
    }
    /// <summary>
    /// 创建角色_装备属性
    /// </summary>  
    public CharAttribute(CharData _data, List<EquipAttribute> _equipAttributes)
    {
        CreateBaseInfo(_data);
        //
        foreach (EquipAttribute item in _equipAttributes)
        {
            equipAttribute.Add(item);
        }

        //添加技能属性
        CreateSkillAttribute();
    }

    /// <summary>
    /// 创建角色_装备属性
    /// </summary>  
    public CharAttribute(CharAttribute charAttribute)
    {
        CreateBaseInfo(charAttribute.GetCharData());
        //
        foreach (EquipAttribute item in charAttribute.equipAttribute)
        {
            equipAttribute.Add(item);
        }

        //添加技能属性
        CreateSkillAttribute();
    }

    #endregion

    /// <summary>
    /// 得到角色数据
    /// </summary>
    /// <returns></returns>
    public CharData GetCharData()
    {
        CharData date = new CharData
        {
            templateID = templateID,
            charID = charID,
            charEXP = charExp,
            charLevel = charLevel,
            charQuality = charQuality,
            charType = charType,
            upgradeDef = upgradeDef,
            upgradeOff = upgradeOff,
            minUpgrade = minUpgrade,
            maxUpgrade = maxUpgrade,
            charAttack = charAttack,
            attitudeID = attitudeID,
            charHP = charHP,
            personalityAddPassiveSkill = personalityAddPassiveSkill,
            personalityType = personalityType,
            powerUpIDs = powerUpIDs,
            finalUpgradeDefSum = finalUpgradeDefSum,
            finalUpgradeOffSum = finalUpgradeOffSum,
            charName = charName,
            isRename = isRename,
            isCommander= isCommander,
            //
            equipID = new List<int>(),
            charStateType = (int)charStatus,
            charPos = (int)charPos,
            buffs = buffs,
            rndATTs = rndATTs,
            manualSkills = manualSkills,
            combatSkills = combatSkills,
            commonSkills = commonSkills,
            passiveSkills = passiveSkills,
            tacticalSkills = tacticalSkills,
        };
        foreach (EquipAttribute item in equipAttribute)
        {
            date.equipID.Add(item.itemID);
        }
        //
        return date;
    }

    /// <summary>
    /// 计算升级
    /// </summary>
    private void CalculateUpgrade(int level)
    {
        GetUpgrade();
        expAddCharHP = (ExpCharHP(level) - ExpCharHP(level - 1)) * (1 + charRankBonus);
        expAddDamage = (ExpDamage(level) - ExpDamage(level - 1)) * (1 + charRankBonus);
        LogHelperLSK.Log("charID=" + templateID + "    " + level + "级" + "expAddCharHP=" + expAddCharHP +
                         "   expAddDamage=" + expAddDamage + "  finalUpgradeDef=" + finalUpgradeDef +
                         "   finalUpgradeOff=" + finalUpgradeOff);
        charHP += expAddCharHP * finalUpgradeDef;
        charAttack += expAddDamage * finalUpgradeOff;
    }
    /// <summary>
    /// 初始化性格
    /// </summary>
    /// <param name="personalityID"></param>
    private void InitialPersonality(int personalityID)
    {
        Personality_template personality_Template = Personality_templateConfig.GetTemplate(personalityID);
        if (personality_Template != null)
        {
            //添加被动
            personalityAddPassiveSkill = RandomBuilder.RandomList(1, personality_Template.passiveSkillList)[0];
            PassiveSkill_template template = PassiveSkill_templateConfig.GetPassiveSkill_Template(personalityAddPassiveSkill);
            personalityType = template.reqType;
            passiveSkills.Add(personalityAddPassiveSkill);
            //随机添加强化
            powerUpIDs.Add(RandomBuilder.RandomList(1, template.powerUp)[0]);
        }
    }

    //获得升级
    private void GetUpgrade()
    {
        upgradeDef = GetRandom_Normal(minUpgrade, maxUpgrade);
        upgradeOff = GetRandom_Normal(minUpgrade, maxUpgrade);
        finalUpgradeDefSum += finalUpgradeDef;
        finalUpgradeOffSum += finalUpgradeOff;
        LogHelperLSK.Log("charID=" + templateID + "    " + "upgradeDef=" + upgradeDef + "    " + "upgradeOff=" + upgradeOff);
    }

    /// <summary>
    /// 创建基本信息
    /// </summary>
    /// <param name="data"></param>
    private void CreateBaseInfo(CharData data)
    {
        charStatus = (CharStatus)data.charStateType;
        charPos = (CharPos)data.charPos;
        charID = data.charID;
        charExp = data.charEXP;
        charLevel = data.charLevel;
        charQuality = data.charQuality;
        templateID = data.templateID;
        charType = data.charType;
        upgradeDef = data.upgradeDef;
        upgradeOff = data.upgradeOff;
        minUpgrade = data.minUpgrade;
        maxUpgrade = data.maxUpgrade;
        charAttack = data.charAttack;
        attitudeID = data.attitudeID;
        charHP = data.charHP;
        personalityAddPassiveSkill = data.personalityAddPassiveSkill;
        personalityType = data.personalityType;
        powerUpIDs = data.powerUpIDs;
        finalUpgradeDefSum = data.finalUpgradeDefSum;
        finalUpgradeOffSum = data.finalUpgradeOffSum;
        charName = data.charName;
        isRename = data.isRename;
        isCommander = data.isCommander;
        //
        char_template = Char_templateConfig.GetTemplate(templateID);
        cbt_config = Combat_configConfig.GetCombat_config();
        char_config = Char_configConfig.GetConfig();
        buffs = data.buffs;
        manualSkills = data.manualSkills;
        combatSkills = data.combatSkills;
        passiveSkills = data.passiveSkills;
        tacticalSkills = data.tacticalSkills;
        commonSkills = data.commonSkills;
        //
        rndATTs = data.rndATTs;
        //
        InitPassiveAttribute();
    }


    private void InitPassiveAttribute()
    {
        passiveSkills.Clear();
        foreach (int item in passiveSkills)
        {
            passiveAttribute.Add(new PassiveAttribute(item));
        }
    }

    /// <summary>
    /// 创建装备属性
    /// </summary>
    /// <param name="equips"></param>
    private void CerateEquipAttribute(List<int> equips)
    {
        if (equips != null)
        {
            foreach (int item in equips)
            {
                equipAttribute.Add(ItemSystem.Instance.GetEquipAttribute(item));
            }
        }
    }

    /// <summary>
    /// 更新装备添加技能
    /// </summary>
    private void UpdateEquipAddSkill()
    {

    }
    /// <summary>
    /// 添加技能操作
    /// </summary>
    /// <param name="skillID"></param>
    private void AddSkillOperation(int skillID)
    {
        Combatskill_template combatskill = Combatskill_templateConfig.GetCombatskill_template(skillID);
        if (combatskill == null)
        {
            return;
        }
        switch ((SkillType)combatskill.skillType)
        {
            case SkillType.Tactical:
                SkillsAddOperation(skillID, tacticalSkills);
                break;
            case SkillType.Manual:
                SkillsAddOperation(skillID, manualSkills);
                break;
            case SkillType.Common:
                SkillsAddOperation(skillID, commonSkills);
                break;
        }
    }

    /// <summary>
    /// 创建技能属性
    /// </summary>
    private void CreateSkillAttribute()
    {

    }

    /// <summary>
    /// 获得正态分布
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    protected float GetRandom_Normal(float max, float min)
    {
        return (int)(RandomBuilder.Random_Normal(max, min) * 100) / 100f;
    }

    private List<int> GetRandomNums()
    {
        List<int> list = new List<int>();
        int[] query = Enumerable.Range(0, 3).Select(x => RandomBuilder.RandomNum(600)).ToArray();
        float[] temp = query.Select(x => x / (float)query.Sum() * 6).ToArray();
        foreach (float item in temp)
        {
            list.Add((((int)(item * 10)) % 10 >= 5 ? 1 : 0) + (int)item);
        }
        return list;
    }

    private List<int> SkillsAddOperation(List<int> newSkills, List<int> oldSkills)
    {
        List<int> lists = newSkills.Where(item => !oldSkills.Contains(item)).ToList();
        lists.AddRange(oldSkills);
        return lists;
    }
    /// <summary>
    /// 技能添加
    /// </summary>
    /// <param name="newSkill"></param>
    /// <param name="oldSkills"></param>
    private void SkillsAddOperation(int newSkill, List<int> oldSkills)
    {
        if (oldSkills.Contains(newSkill))
        {
            return;
        }

        oldSkills.Add(newSkill);
    }

    private int ComputeNowIndex(int nowValue, List<int> values, int startIndex, int endIndex)
    {
        return ComputeNowIndex(nowValue, values, startIndex, endIndex);
    }
    /// <summary>
    /// 计算现在索引
    /// </summary>
    /// <param name="nowValue"></param>
    /// <param name="values"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    private int ComputeNowIndex(float nowValue, List<int> values, int startIndex, int endIndex)
    {
        try
        {
            //检查最后一个
            if (values.First() == nowValue)
            {
                return 0;
            }
            //检查最后一个
            if (values.Last() == nowValue)
            {
                return values.Count - 1;
            }
            int middleIndex = (startIndex + endIndex + 1) / 2;
            if (nowValue == values[middleIndex])
            {
                return middleIndex;
            }
            //先查当前表的前端或后端
            //在左边
            if (nowValue < values[middleIndex])
            {
                endIndex = middleIndex;
            }
            else //在右边
            {
                startIndex = middleIndex;
            }
            if (endIndex - startIndex == 1)
            {
                if (nowValue == values[startIndex])
                {
                    return startIndex;
                }
                if (nowValue == values[endIndex])
                {
                    return endIndex;
                }
                return nowValue > values[startIndex] ? startIndex : startIndex - 1;
            }
            return ComputeNowIndex(nowValue, values, startIndex, endIndex);
        }
        catch (Exception)
        {
            LogHelperLSK.LogWarning("计算错误");
            return -2;
        }
    }

    /// <summary>
    /// 角色升级
    /// </summary>
    /// <param name="value"></param>
    private void CharUpgrades(int value)
    {
        CheckRankLevel();
    }

    /// <summary>
    /// 检查军衔等级
    /// </summary>
    private void CheckRankLevel()
    {
        if (charRank >= char_lvup.maxCharRank)
        {
            return;
        }
        if (RandomBuilder.RandomIndex_Chances(char_lvup.rkupChance) != 0)
        {
            return;
        }
        charRank1 = (CharRank)char_lvup.maxCharRank;
    }

    private int GetCharPos(CombatUnit onself, CombatUnit target)
    {
        if (onself.initIndex - target.initIndex == -1)
        {
            return 1;
        }
        if (onself.initIndex - target.initIndex == 1)
        {
            return 2;
        }
        return -1;
    }

}

/// <summary>
/// 性格添加激励
/// </summary>
public class PersonalityAddEncourage
{
    /// <summary>
    /// 添加激励
    /// </summary>
    public int addEncourage;
    /// <summary>
    /// 禁用标签
    /// </summary>
    public int disableTag;
    /// <summary>
    /// 来源角色索引
    /// </summary>
    public int formeCharIndex;

    /// <summary>
    /// 性格类型
    /// </summary>
    public int personalityType;
    /// <summary>
    /// 添加信息：key=角色index,value=添加数量
    /// </summary>
    public Dictionary<int, int> addInfo = new Dictionary<int, int>();
    /// <summary>
    /// 目标
    /// </summary>
    public int target = -1;

    public PersonalityAddEncourage(int charIndex)
    {
        formeCharIndex = charIndex;
    }
    public PersonalityAddEncourage()
    {
    }
}