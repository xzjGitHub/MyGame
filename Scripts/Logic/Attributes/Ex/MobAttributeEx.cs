﻿using System.Collections.Generic;

/// <summary>
/// 怪物属性
/// </summary>
public partial class MobAttribute : CharAttribute
{
    /// <summary>
    /// 击晕难度
    /// </summary>
    public List<float> stunValue;
    /// <summary>
    /// 角色等级
    /// </summary>
    public int charLeve;
    /// <summary>
    /// 怪物强化
    /// </summary>
    public float mobBonus;
    /// <summary>
    /// 消耗生命
    /// </summary>
    public float HPConsume;
    /// <summary>
    /// 角色模板
    /// </summary>
    public Mob_template mob_template;
    /// <summary>
    /// 技能信息
    /// </summary>
    public List<int> skillInfos2 = new List<int>();
    /// <summary>
    /// 技能信息
    /// </summary>
    public List<int> skillInfos3 = new List<int>();
    /// <summary>
    /// 技能信息
    /// </summary>
    public List<int> skillInfos4 = new List<int>();
    /// <summary>
    /// 创建角色
    /// </summary>
    public MobAttribute(CharCreate _create) : base(_create)
    {
        InitInfo();
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    public MobAttribute(int mobTeam, CharAttribute charAttribute) : base(charAttribute)
    {
        Mob_mobteam mobteam = Mob_mobteamConfig.GetMobMobteam(mobTeam);
        if (mobteam != null)
        {
            charLeve = mobteam.charLevel;
            mobBonus = mobteam.mobBonus;
            HPConsume = mobteam.HPConsume;
        }

        InitInfo();
    }


    private void InitInfo()
    {
        mob_template = Mob_templateConfig.GetTemplate(templateID);
        if (mob_template == null)
        {
            LogHelper_MC.LogError("Mob_template中没有找到=" + templateID);
        }
        charHP += baseCharHP;
        charAttack += baseCharAttack;
        charType = CombatCharType.Mob;
        equipAttribute = new List<EquipAttribute>();
        upgradeDef = GetRandom_Normal(mob_template.upgrade[0], mob_template.upgrade[1]);
        upgradeOff = GetRandom_Normal(mob_template.upgrade[0], mob_template.upgrade[1]);
        //战术技能
        tacticalSkills.AddRange(mob_template.tacticalSkillList);
        //添加战斗技能
        combatSkills.Clear();
        foreach (int item in mob_template.combatSkillList)
        {
            combatSkills.Add(item);
        }
        //添加战斗技能
        foreach (int item in mob_template.combatSkillList2)
        {
            skillInfos2.Add(item);
        }
        //添加战斗技能
        foreach (int item in mob_template.combatSkillList3)
        {
            skillInfos3.Add(item);
        }
        //添加战斗技能
        foreach (int item in mob_template.combatSkillList4)
        {
            skillInfos4.Add(item);
        }
    }

    //
    //  private float upgradeDef;
    // private float upgradeOff;
    //private int charRank;
}
