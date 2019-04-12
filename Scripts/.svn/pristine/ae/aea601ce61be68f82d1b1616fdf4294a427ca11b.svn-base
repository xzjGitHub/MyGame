using MCCombat;
using System;
using System.Collections.Generic;

/// <summary>
/// 角色属性
/// </summary>
public partial class CharAttribute
{
    /// <summary>
    /// 角色伤害
    /// </summary>
    public float charAttack;
    /// <summary>
    /// 负生命
    /// </summary>
    public float negativeHP;
    /// <summary>
    /// 角色生命
    /// </summary>
    public float charHP;
    /// <summary>
    /// 角色随机强化_生命
    /// </summary>
    public float upgradeDef;
    /// <summary>
    /// 角色随机强化_伤害
    /// </summary>
    public float upgradeOff;
    /// <summary>
    /// 模板ID
    /// </summary>
    public int templateID;
    /// <summary>
    /// 角色id
    /// </summary>
    public int charID;
    /// <summary>
    /// 角色等级
    /// </summary>
    public int charLevel;
    /// <summary>
    /// 等级加值
    /// </summary>
    public float addLevel;
    /// <summary>
    /// 角色随机属性列表
    /// </summary>
    public List<int> rndATTs = new List<int>();
    /// <summary>
    /// 角色品质
    /// </summary>
    public float charQuality;
    /// <summary>
    /// 角色经验
    /// </summary>
    public float charExp;
    /// <summary>
    /// 指挥官
    /// </summary>
    private bool isCommander;
    /// <summary>
    /// 战斗角色类型
    /// </summary>
    public CombatCharType charType;
    /// <summary>
    /// 角色模板
    /// </summary>
    public Char_template char_template;
    /// <summary>
    /// 角色设置
    /// </summary>
    public Char_config char_config;
    /// <summary>
    /// 战斗配置
    /// </summary>
    public Combat_config cbt_config;
    /// <summary>
    /// 装备属性
    /// </summary>
    public List<EquipAttribute> equipAttribute = new List<EquipAttribute>();
    /// <summary>
    /// 技能属性
    /// </summary>
    public List<SkillAttribute> skillAttribute = new List<SkillAttribute>();

    /// <summary>
    /// 被动属性
    /// </summary>
    public List<PassiveAttribute> passiveAttribute = new List<PassiveAttribute>();
    /// <summary>
    /// 状态属性
    /// </summary>
    public List<StateAttribute> stateAttribute = new List<StateAttribute>();

    /// <summary>
    /// 命中类型
    /// </summary>
    public HitResult hitResult = HitResult.Critical;
    /// <summary>
    /// 角色的最终资质
    /// </summary>
    public float AvgPotential { get { return 0; } }

    public float skillCDReduction
    {
        get { return 0; }
    }

    public int finalCharInitiative
    {
        get { return 0; }
    }

    /// <summary>
    /// 角色状态
    /// </summary>
    protected CharStatus charStatus = CharStatus.Idle;
    /// <summary>
    /// 角色位置
    /// </summary>
    protected CharPos charPos = CharPos.Idle;
    /// <summary>
    /// buff列表
    /// </summary>
    protected List<BuffInfo> buffs = new List<BuffInfo>();
    /// <summary>
    /// 手动技能
    /// </summary>
    public List<int> ManualSkills { get { return manualSkills; } }
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<int> CombatSkills { get { return combatSkills; } }
    /// <summary>
    /// 被动技能
    /// </summary>
    public List<int> PassiveSkills { get { return passiveSkills; } }
    /// <summary>
    /// 战术技能
    /// </summary>
    public List<int> TacticalSkills { get { return tacticalSkills; } }
    /// <summary>
    /// 通用技能
    /// </summary>
    public List<int> CommonSkills { get { return commonSkills; } }
    /// <summary>
    /// 军衔位阶
    /// </summary>
    public CharRank CharRank { get { return charRank1; } }
    /// <summary>
    /// 性格id
    /// </summary>
    public int AttitudeID { get { return attitudeID; } }
    /// <summary>
    /// 最终激励数
    /// </summary>
    public int FinalEncourage { get { return finalEncourage; } }
    /// <summary>
    /// 性格类型
    /// </summary>
    public int PersonalityType { get { return personalityType; } }
    /// <summary>
    /// 指挥官
    /// </summary>
    public bool IsCommander { get { return isCommander; } }
    /// <summary>
    /// 性格添加被动技能
    /// </summary>
    public int PersonalityAddPassiveSkill { get { return personalityAddPassiveSkill; } }

    /// <summary>
    /// 角色生命预期__给tempCharLevel赋值
    /// </summary>
    public float ExpCharHP(int level)
    {
        return char_template.baseHP * FinalLvRate(level) + level * char_template.addLvHP;
    }

    /// <summary>
    /// 角色伤害预期__给tempCharLevel赋值
    /// </summary>
    public float ExpDamage(int level)
    {
        return char_template.baseAttack * Char_lvupConfig.GetChar_Lvup(level).DMGRate;
    }

    /// 角色等级成长
    /// </summary>
    public float FinalLvRate(int level)
    {
        return (float)(Math.Pow(char_config.lvRate, level));
    }

    /// <summary>
    /// 手动技能
    /// </summary>
    private List<int> manualSkills = new List<int>();
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<int> combatSkills = new List<int>();
    /// <summary>
    /// 被动技能
    /// </summary>
    private List<int> passiveSkills = new List<int>();
    /// <summary>
    /// 战术技能
    /// </summary>
    protected List<int> tacticalSkills = new List<int>();
    /// <summary>
    /// 通用技能
    /// </summary>
    private List<int> commonSkills = new List<int>();

    /// <summary>
    /// 军衔类型
    /// </summary>
    private int charRank
    {
        get { return (int)charRank1; }
    }

    public List<int> PowerUpIDs
    {
        get
        {
            return powerUpIDs;
        }
    }

    public bool IsRename
    {
        get
        {
            return isRename;
        }
        set
        {
            isRename = value;
        }
    }

    public string CharName
    {
        get
        {
            return charName;
        }
        set
        {
            charName = value;
        }
    }

    public List<int> AddFinalEncourageIndexs { get { return addFinalEncourageIndexs; } }

    /// <summary>
    /// 军衔类型
    /// </summary>
    private CharRank charRank1;
    /// <summary>
    /// 最小升级
    /// </summary>
    private float minUpgrade;
    /// <summary>
    /// 最大升级
    /// </summary>
    private float maxUpgrade;
    /// <summary>
    /// 强化ID
    /// </summary>
    private List<int> powerUpIDs = new List<int>();

    private string charName;
    private bool isRename;

    private float expAddCharHP;
    private float expAddDamage;
    private float finalUpgradeDefSum;
    private float finalUpgradeOffSum;

    private List<int> addFinalEncourageIndexs = new List<int>();

    /// <summary>
    /// 性格
    /// </summary>
    private int attitudeID;
    /// <summary>
    /// 最终激励数
    /// </summary>
    private int finalEncourage;
    /// <summary>
    /// 性格添加被动技能
    /// </summary>
    private int personalityAddPassiveSkill;
    /// <summary>
    /// 性格类型
    /// </summary>
    private int personalityType;
}

public enum CombatCharType
{
    /// <summary>
    /// 角色
    /// </summary>
    Char = 1,
    /// <summary>
    /// 怪物
    /// </summary>
    Mob = 2,
}
