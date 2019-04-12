using MCCombat;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 战斗治疗标签
/// </summary>
public class CombatHealingTag
{
    public int NowIndex
    {
        get { return nowIndex; }
    }

    public CombatHealingTag(int index, int nowRound)
    {
        SetIndex(index, nowRound);
    }

    public void SetIndex(int index, int nowRound)
    {
        nowIndex = index;
        addRound = nowRound;
    }

    public void Reset(int nowRound, int index)
    {
        if (nowRound - 1 != addRound)
        {
            return;
        }
        SetIndex(index, nowRound);
    }

    //
    private int nowIndex;
    private int addRound;
}

/// <summary>
/// 战斗角色信息
/// </summary>
public class CombatCharInfo
{
    public int teamID;
    public int charID;
    public int index;
    public int templateID;
    public int hp;
    public int maxHp;
    public int exp;
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<UnitSkillInfo> combatSkills = new List<UnitSkillInfo>();
    /// <summary>
    /// 手动技能
    /// </summary>
    public List<UnitSkillInfo> manualSkills = new List<UnitSkillInfo>();
    /// <summary>
    /// 通用技能
    /// </summary>
    public List<UnitSkillInfo> commonSkills = new List<UnitSkillInfo>();
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<UnitSkillInfo> combatSkills2 = new List<UnitSkillInfo>();
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<UnitSkillInfo> combatSkills3 = new List<UnitSkillInfo>();
    /// <summary>
    /// 战斗技能
    /// </summary>
    public List<UnitSkillInfo> combatSkills4 = new List<UnitSkillInfo>();
    /// <summary>
    /// 状态信息
    /// </summary>
    public List<UnitStateInfo> stateInfos = new List<UnitStateInfo>();
}

/// <summary>
/// 战斗回合
/// </summary>
public class CombatRound
{
    /// <summary>
    /// 回合ID
    /// </summary>
    public int roundId;
    /// <summary>
    /// 战斗回合结果
    /// </summary>
    public List<CombatRoundResult> combatRoundResults = new List<CombatRoundResult>();
    /// <summary>
    /// 回合所有单元信息列表
    /// </summary>
    public List<CombatUnit> combatRoundUnits = new List<CombatUnit>();
    /// <summary>
    /// 当前回合所有角色信息
    /// </summary>
    public List<CombatCharInfo> combatCharInfos = new List<CombatCharInfo>();
    /// <summary>
    /// 战斗结果
    /// </summary>
    public CombatResult combatResult = new CombatResult();
}

/// <summary>
/// 战斗回合结果_每个人的行动结果
/// </summary>
public class CombatRoundResult
{
    /// <summary>
    /// 当前行动的队伍
    /// </summary>
    public int teamID;//
    /// <summary>
    /// 当前行动的角色
    /// </summary>
    public int charID;//
    /// <summary>
    /// 当前行动角色位置
    /// </summary>
    public int index;//
    /// <summary>
    /// 使用的技能ID
    /// </summary>
    public int skillID;//
    /// <summary>
    /// 战斗结果类型
    /// </summary>
    public CommandResultType resultType; //
    /// <summary>
    /// 使用技能
    /// </summary>
    public CRCastSkill castSkill;//
    /// <summary>
    /// 脉冲效果
    /// </summary>
    public CRImpulseEffect impulseEffect;//
    /// <summary>
    /// 回合结束
    /// </summary>
    public CRRoundEnd roundEnd;
    /// <summary>
    /// 移除的状态
    /// </summary>
    public List<CRRemoveState> removeStates;
    /// <summary>
    /// 目标位置集合
    /// </summary>
    public List<int> targetIndexs;//

    public CombatRoundResult(CommandResultType type)
    {
        resultType = type;
    }
}

/// <summary>
/// 战斗结果
/// </summary>
public class CombatResult
{
    /// <summary>
    /// 是否结束
    /// </summary>
    public bool isEnd;
    /// <summary>
    /// 胜利队伍
    /// </summary>
    public int victoryTeam;
}


/// <summary>
/// 结果类型
/// </summary>
public enum CommandResultType
{
    /// <summary>
    /// 使用技能
    /// </summary>
    CastSkill = 1,       //
    /// <summary>
    /// 执行状态
    /// </summary>
    ExecState = 2,       //
    /// <summary>
    /// 检查Buff生存期
    /// </summary>
    CheckBuff = 3,       //
    /// <summary>
    /// 脉冲生效
    /// </summary>
    ImpulseEffect = 4,    //
}
/// <summary>
/// 回合结束
/// </summary>
public class CRRoundEnd
{
    public int nowRound;
    /// <summary>
    /// 移除的状态
    /// </summary>
    public List<CRRemoveState> removeStates;
}

/// <summary>
/// 移除状态
/// </summary>
public class CRRemoveState
{
    /// <summary>
    /// 队伍id
    /// </summary>
    public int teamID;
    /// <summary>
    /// 角色id
    /// </summary>
    public int charID;
    /// <summary>
    /// 索引id
    /// </summary>
    public int index;
    /// <summary>
    /// 状态id
    /// </summary>
    public List<int> stateID = new List<int>();
}

/// <summary>
/// 技能效果类型
/// </summary>
public enum CRSkillEffectResultType
{
    /// <summary>
    /// 增加状态
    /// </summary>
    AddState = 1,     //
    /// <summary>
    ///删除状态 
    /// </summary>
    RemoveState = 2,  //
    /// <summary>
    /// 执行效果
    /// </summary>
    ExecEffect = 3,//
    /// <summary>
    /// 未命中
    /// </summary>
    Miss = 4,            //
    /// <summary>
    /// 额外
    /// </summary>
    Extra = 5,
}

/// <summary>
/// 技能效果类型
/// </summary>
public enum SkillEffectType
{
    /// <summary>
    /// 击退
    /// </summary>
    Knockback = 1011,
    /// <summary>
    /// 击晕
    /// </summary>
    Stun = 1012,
    /// <summary>
    /// 加速
    /// </summary>
    Haste = 1013,
    /// <summary>
    /// 冲锋
    /// </summary>
    Charge = 1014,
}


/// <summary>
/// 技能信息
/// </summary>
public class UnitSkillInfo
{
    /// <summary>
    /// 技能编号
    /// </summary>
    public int skillID;    //
    /// <summary>
    /// 冷却回合数
    /// </summary>
    public int cooldown; //
}

/// <summary>
/// 状态信息
/// </summary>
public class UnitStateInfo
{
    /// <summary>
    ///  状态编号
    /// </summary>
    public int stateID;
    /// <summary>
    /// 状态类型
    /// </summary>
    public StateType stateType;
    /// <summary>
    /// 剩余回合数
    /// </summary>
    public int leftRound;   //
    /// <summary>
    /// 施放团队
    /// </summary>
    public int castTeam;    //
    /// <summary>
    /// 施放者编号
    /// </summary>
    public int castId; //
    /// <summary>
    /// 施放者索引
    /// </summary>
    public int castIndex;
    /// <summary>
    /// 伤害类型
    /// </summary>
    public HitResult hitResult;//
    //
    public UnitStateInfo(State _state)
    {
        stateID = _state.stateID;
        castId = _state.charID;
        castIndex = _state.charIndex;
        castTeam = _state.teamID;
        leftRound = _state.duration;
        hitResult = _state.hitResult;
    }
    public UnitStateInfo(StateAttribute stateAttribute)
    {
        stateID = stateAttribute.stateID;
        castId = stateAttribute.charID;
        castIndex = stateAttribute.charIndex;
        castTeam = stateAttribute.teamID;
        leftRound = stateAttribute.duration;
        hitResult = HitResult.Hit;
    }
}

/// <summary>
/// 使用技能
/// </summary>
public class CRCastSkill
{
    /// <summary>
    /// 使用技能的团队
    /// </summary>
    public int castTeamId;              //
    /// <summary>
    /// 使用技能的角色索引
    /// </summary>
    public int castIndex;
    /// <summary>
    /// 使用技能的角色
    /// </summary>
    public int castCharId;               //
    /// <summary>
    /// 使用的技能
    /// </summary>
    public int castSkillId;               //
    /// <summary>
    /// 是预备还是生效阶段
    /// </summary>
    public bool isFire;             //
    /// <summary>
    /// 目标集和信息列表
    /// </summary>
    public List<CRTargetInfo> targetInfos;//
}

/// <summary>
/// 脉冲效果
/// </summary>
public class CRImpulseEffect
{
    /// <summary>
    /// 当前行动的队伍
    /// </summary>
    public int teamID;
    /// <summary>
    /// 当前行动的角色
    /// </summary>
    public int charID;
    /// <summary>
    /// 当前行动角色位置
    /// </summary>
    public int index;
    /// <summary>
    /// 战斗结果类型
    /// </summary>
    public CommandResultType resultType;
    /// <summary>
    /// 技能效果结果
    /// </summary>
    public List<CRSkillEffectResult> skillEffectResults;
}

/// <summary>
/// 目标集合信息
/// </summary>
public class CRTargetInfo
{
    /// <summary>
    /// 效果ID
    /// </summary>
    public int targetId;
    /// <summary>
    /// 动作索引
    /// </summary>
    public int actionIndex;
    /// <summary>
    /// 效果列表
    /// </summary>
   // public List<CRSkillEffect> skillEffects;  
    /// <summary>
    /// 目标单元效果列表
    /// </summary>
    public List<CRTargetUnitInfo> targetUnitInfos;
}

/// <summary>
/// 目标单元信息
/// </summary>
public class CRTargetUnitInfo
{
    /// <summary>
    /// 命中者队伍id
    /// </summary>
    public int hitTeamId;
    /// <summary>
    /// 命中者角色id
    /// </summary>
    public int hitCharId;
    /// <summary>
    /// 命中者位置
    /// </summary>
    public int hitIndex;
    /// <summary>
    /// 伤害类型
    /// </summary>
    public HitResult hitResult;
    /// <summary>
    /// 效果
    /// </summary>
    public CRSkillEffect skillEffect;
    /// <summary>
    /// 来源位置----吸血显示
    /// </summary>
    public int sourceIndex = -1;
}

/// <summary>
/// 技能效果
/// </summary>
public class CRSkillEffect
{
    /// <summary>
    /// 命中者队伍id
    /// </summary>
    public int hitTeamId;       //
    /// <summary>
    /// 命中者角色id
    /// </summary>
    public int hitCharId;       //
    /// <summary>
    /// 命中者位置
    /// </summary>
    public int hitIndex;       //
    /// <summary>
    /// 伤害类型
    /// </summary>
    public HitResult hitResult;
    /// <summary>
    /// 技能效果类型
    /// </summary>
    public SkillEffectType skillEffectType;
    /// <summary>
    /// 技能效果结果
    /// </summary>
    public List<CRSkillEffectResult> skillEffectResults;
}
/// <summary>
/// 技能效果结果
/// </summary>
public class CRSkillEffectResult
{
    /// <summary>
    /// 技能效果类型
    /// </summary>
    public CRSkillEffectResultType CrSkillEffectResultType;
    /// <summary>
    /// 执行状态
    /// </summary>
    public CRExecState execState;
}
/// <summary>
/// 状态效果类型
/// </summary>
public enum CRStateEffectType
{
    /// <summary>
    /// 只修改生命
    /// </summary>
    HP = 1,
    /// <summary>
    /// 只修改护盾
    /// </summary>
    Shield = 2,
    /// <summary>
    /// 添加状态
    /// </summary>
    AddState = 3,
    /// <summary>
    /// 移除状态
    /// </summary>
    RemoveState = 4,
    /// <summary>
    /// 修改护盾和生命
    /// </summary>
    All = 5,
    /// <summary>
    /// 吸收
    /// </summary>
    Absorb = 6,
}

/// <summary>
/// 效果结果
/// </summary>
public class CREffectResult
{
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool isShow = true;
    /// <summary>
    /// 是否复活
    /// </summary>
    public bool isRevive;
    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool isDie;
    /// <summary>
    /// 是否伤害
    /// </summary>
    public bool isHurt;
    /// <summary>
    /// 命中团队
    /// </summary>
    public int hitTeamId;       //团队编号0,1
    /// <summary>
    /// 命中索引
    /// </summary>
    public int hitIndex;       //当前角色位置
    /// <summary>
    /// 命中角色id
    /// </summary>
    public int hitCharId;       //当前角色id
    /// <summary>
    /// 角色最大生命值
    /// </summary>
    public int maxHp;
    /// <summary>
    /// 伤害，治疗
    /// </summary>
    public int hp;            //
    /// <summary>
    /// 护盾（+、-）
    /// </summary>
    public int shield;      //
    /// <summary>
    /// 最大护盾
    /// </summary>
    public int maxShield;
    /// <summary>
    /// 当前生命
    /// </summary>
    public int currentHp;
    /// <summary>
    /// 当前护盾
    /// </summary>
    public int currentShield;
    /// 当前临时护盾
    /// </summary>
    public int currentTempShield;
    /// <summary>
    /// 当前护甲
    /// </summary>
    public int currentArmor;
    /// <summary>
    /// 当前临时护甲
    /// </summary>
    public int currentTempArmor;
    /// <summary>
    /// 状态ID
    /// </summary>
    public int stateID;
    /// <summary>
    /// 状态效果类型
    /// </summary>
    public CRStateEffectType CrStateEffectType;
    /// <summary>
    /// 最大护甲
    /// </summary>
    public int maxArmor;
    /// <summary>
    /// 护甲
    /// </summary>
    public int armor;
    /// <summary>
    /// 伤害吸收
    /// </summary>
    public float damageAbsorb;
    /// <summary>
    /// 当前伤害吸收
    /// </summary>
    public float currentDamageAbsorb;
    /// <summary>
    /// 临时护盾
    /// </summary>
    public float periodShield;
    /// <summary>
    /// 临时护甲
    /// </summary>
    public float periodArmor;
    /// <summary>
    /// 负生命
    /// </summary>
    public float negativeHP;
    /// <summary>
    /// 消耗能量
    /// </summary>
    public float consumeEnergy;
    /// <summary>
    /// 状态的持续周期计算参数
    /// </summary>
    public int duration;
    /// <summary>
    /// 是否击晕
    /// </summary>
    public bool isStunned;
    /// <summary>
    /// 是否盾破
    /// </summary>
    public bool isShieldMar;
    /// <summary>
    /// 是否盾防
    /// </summary>
    public bool isShieldDefend;
    /// <summary>
    /// 是否甲防
    /// </summary>
    public bool isArmorDefend;
    /// <summary>
    /// 是否甲破
    /// </summary>
    public bool isArmorMar;
    /// <summary>
    /// 是否生命值变化
    /// </summary>
    public bool isHPChange;

    public CREffectResult() { }

    public CREffectResult(CombatUnit unit)
    {
        maxHp = unit.maxHp;
        maxArmor = unit.maxArmor;
        maxShield = unit.maxShield;
        currentHp = unit.hp;
        currentArmor = unit.armor;
        currentShield = unit.shield;
        hitIndex = unit.initIndex;
        hitCharId = unit.CharID;
        hitTeamId = unit.teamId;
    }
}
/// <summary>
/// 执行状态
/// </summary>
public class CRExecState
{
    /// <summary>
    /// 状态id
    /// </summary>
    public int stateID;
    /// <summary>
    /// 使用者队伍id
    /// </summary>
    public int castTeamId;
    /// <summary>
    /// 使用者角色id
    /// </summary>
    public int castCharId;
    /// <summary>
    /// 使用者位置
    /// </summary>
    public int castIndex;
    /// <summary>
    /// 使用的技能
    /// </summary>
    public int castSkillId;
    /// <summary>
    /// 最终的效果
    /// </summary>
    public CREffectResult effectResult;
    /// <summary>
    /// 状态信息
    /// </summary>
    public UnitStateInfo stateInfo;
    /// <summary>
    /// 状态属性
    /// </summary>
    public StateAttribute stateAttribute;
    //
    public CRExecState(CombatUnit combatUnit, int castSkillId, int stateID, CREffectResult effectResult, StateAttribute stateAttribute)
    {
        castTeamId = combatUnit.teamId;
        castCharId = combatUnit.charAttribute.charID;
        castIndex = combatUnit.initIndex;
        this.castSkillId = castSkillId;
        this.stateID = stateID;
        this.effectResult = effectResult;
        this.stateAttribute = stateAttribute;
        stateInfo = stateAttribute.GetStateInfo();
    }
    public CRExecState(int castTeamId, int castCharId, int castIndex, int castSkillId, int stateID, CREffectResult effectResult = null)
    {
        this.castTeamId = castTeamId;
        this.castCharId = castCharId;
        this.castIndex = castIndex;
        this.castSkillId = castSkillId;
        this.stateID = stateID;
        this.effectResult = effectResult;
    }
    public CRExecState(StateAttribute state, int castIndex, bool isAutoExecute = true)
    {
        castTeamId = state.teamID;
        castCharId = state.charID;
        this.castIndex = castIndex;
        castSkillId = state.fromSkillId;
        stateAttribute = state;
        if (isAutoExecute)
        {
            effectResult = state.ExecuteEffect();
        }
        stateInfo = state.GetStateInfo();
    }
}

/// <summary>
/// 立即显示效果
/// </summary>
public class CRImmediateShowEffect
{
    /// <summary>
    /// 队伍id
    /// </summary>
    public int teamId;
    /// <summary>
    /// 角色id
    /// </summary>
    public int charId;
    /// <summary>
    /// 位置
    /// </summary>
    public int index;
    /// <summary>
    /// 效果类型
    /// </summary>
    public ImmediateShowEffectType effectType;
    /// <summary>
    /// 效果结果
    /// </summary>
    public CREffectResult effectResult;

    public CRImmediateShowEffect(CombatUnit unit)
    {
        teamId = unit.teamId;
        charId = unit.charAttribute.charID;
        index = unit.initIndex;
    }
}


/// <summary>
/// 额外使用技能
/// </summary>
public class CRExtraUseSkill
{
    /// <summary>
    /// 使用者队伍id
    /// </summary>
    public int castTeamId;
    /// <summary>
    /// 使用者角色id
    /// </summary>
    public int castCharId;
    /// <summary>
    /// 使用者位置
    /// </summary>
    public int castIndex;
    /// <summary>
    /// 使用的技能
    /// </summary>
    public int castSkillId;

    public CSkillInfo skillInfo;
}

/// <summary>
/// 立即显示效果类型
/// </summary>
public enum ImmediateShowEffectType
{
    /// <summary>
    /// 生命恢复
    /// </summary>
    HPRecover = 1,
    /// <summary>
    /// 护甲恢复
    /// </summary>
    ArmorRecover = 2,
    /// <summary>
    /// 护盾恢复
    /// </summary>
    ShieldRecover = 3,
}

/// <summary>
/// 职业技能施放阶段
/// </summary>
public enum PlayerSkillFireStage
{
    /// <summary>
    /// 选中
    /// </summary>
    Selected = 1,
    /// <summary>
    /// 选择目标
    /// </summary>
    SelectTarget = 2,
    /// <summary>
    /// 选择取消
    /// </summary>
    SelectCancel = 3,
    /// <summary>
    /// 目标选择完成
    /// </summary>
    TargetSelectOk = 4,
}

/// <summary>
/// 战斗阶段
/// </summary>
public enum CombatStage
{
    /// <summary>
    /// 创建战斗
    /// </summary>
    CreateCombat = 1,
    /// <summary>
    /// 播放开场动画
    /// </summary>
    Opening = 2,
    /// <summary>
    /// 战斗准备
    /// </summary>
    CombatPrepare = 3,
    /// <summary>
    /// 创建回合
    /// </summary>
    CreateRound = 4,
    /// <summary>
    /// 选择技能
    /// </summary>
    ChooseSkill = 5,
    /// <summary>
    /// 开始回合
    /// </summary>
    StartRound = 6,
    /// <summary>
    /// 战斗结束
    /// </summary>
    CombatEnd = 7,
    /// <summary>
    /// 放弃技能
    /// </summary>
    AbandonSkill = 8,
    /// <summary>
    /// 使用手动技能
    /// </summary>
    UseManualSkill = 9,
    /// <summary>
    /// 使用通用技能
    /// </summary>
    UseCommonSkill = 10,
    /// <summary>
    /// 回合结束
    /// </summary>
    RoundEnd = 11,
}

/// <summary>
/// 播放战斗阶段
/// </summary>
public enum PlayCombatStage
{
    /// <summary>
    /// 创建战斗
    /// </summary>
    CreateCombat = 1,
    /// <summary>
    /// 播放开场动画
    /// </summary>
    Opening = 2,
    /// <summary>
    /// 战斗准备
    /// </summary>
    CombatPrepare = 3,
    /// <summary>
    /// 创建回合
    /// </summary>
    CreateRound = 4,
    /// <summary>
    /// 播放选择技能
    /// </summary>
    ChooseSkill = 5,
    /// <summary>
    /// 播放立即技能效果
    /// </summary>
    ImmediateSkillEffect = 6,
    /// <summary>
    /// 回合信息
    /// </summary>
    RoundInfo = 7,
    /// <summary>
    /// 战斗结束
    /// </summary>
    CombatEnd = 8,
    /// <summary>
    /// 初始化左边
    /// </summary>
    InitLeft = 9,
    /// <summary>
    /// 初始化右边
    /// </summary>
    InitRight = 10,
    /// <summary>
    /// 初始化资源
    /// </summary>
    InitRes = 11,
    /// <summary>
    /// 播放回合信息结束
    /// </summary>
    PlayRoundInfoEnd = 12,
    /// <summary>
    /// 回合结束
    /// </summary>
    RoundEnd = 13,
    /// <summary>
    /// 立即显示
    /// </summary>
    ImmediateShow = 14,
}

/// <summary>
/// 回合行动信息
/// </summary>
public class RoundActionInfo
{
    /// <summary>
    /// 战斗单元
    /// </summary>
    public CombatUnit combatUnit;
    /// <summary>
    /// 技能id
    /// </summary>
    public int skillId;
    /// <summary>
    /// 施放类型
    /// </summary>
    public int castType;
    /// <summary>
    /// 技能索引
    /// </summary>
    public int skillIndex;
    /// <summary>
    /// 速度
    /// </summary>
    public int finalSpeed;
    /// <summary>
    /// 技能类型
    /// </summary>
    public SkillType skillType;
    /// <summary>
    /// 选择索引——默认是100(自动技能)
    /// </summary>
    public int selectIndex = 100;

    public CSkillInfo skillInfo;

    /// <summary>
    /// 更新技能信息
    /// </summary>
    public void UpdateSkillInfo(CSkillInfo skillInfo)
    {
        this.skillInfo = skillInfo;
        castType = skillInfo.Combatskill.castType;
        skillType = (SkillType)skillInfo.Combatskill.skillType;
    }

    public RoundActionInfo(CombatUnit combatUnit, CSkillInfo skillInfo)
    {
        this.combatUnit = combatUnit;
        this.skillInfo = skillInfo;
    }
}

#region 测试
public class TestTeam
{
    public List<TestChar> testChars = new List<TestChar>();

    public int FinalEncourageSum { get { return testChars.Sum(a => a.finalEncourage); } }

    public int GetMoreThenValueSum(int value)
    {
        int sum = 0;
        foreach (TestChar item in testChars)
        {
            if (item.finalEncourage > value)
            {
                sum++;
            }
        }
        return sum;
    }

    /// <summary>
    /// 重置性格效果
    /// </summary>
    public void RestPersonalityTake()
    {
        foreach (TestChar item in testChars)
        {
            item.ResetPersonalitEffect();
        }
        PersonalityTake();
    }
    /// <summary>
    /// 性格生效
    /// </summary>
    public void PersonalityTake()
    {
        //先按类型排序
        testChars = testChars.OrderBy(a => a.personalityType).ToList();
        //性格生效
        foreach (TestChar item in testChars)
        {
            PersonalityTakeEffect(item, testChars);
        }
        //按索引排序
        testChars = testChars.OrderBy(a => a.initIndex).ToList();
        //最后添加激励
        List<int> relational1 = new List<int> { 1, 2, 3 };
        List<int> relational2 = new List<int> { 4, 5, 6 };
        //先禁用产出   //如果1名角色上，有2个相关联的disableTag，则所有获得或产出会为0；
        foreach (TestChar unit in testChars)
        {
            //产出
            int sum = GetRelationalSum(unit, relational2);
            switch (sum)
            {
                case 0:
                    break;
                case 1:
                    foreach (TestChar item in testChars)
                    {
                        foreach (PersonalityAddEncourage info in item.personalityAddEncourages)
                        {
                            if (info.formeCharIndex == unit.initIndex && info.disableTag != 4)
                            {
                                info.addEncourage = 0;
                            }
                        }
                    }
                    break;
                default:
                    foreach (TestChar item in testChars)
                    {
                        foreach (PersonalityAddEncourage info in item.personalityAddEncourages)
                        {
                            if (info.formeCharIndex == unit.initIndex)
                            {
                                info.addEncourage = 0;
                            }
                        }
                    }
                    break;
            }
        }
        //再加获得
        foreach (TestChar unit in testChars)
        {
            //获得
            int sum = GetRelationalSum(unit, relational1);
            switch (sum)
            {
                case 0:
                    foreach (PersonalityAddEncourage info in unit.personalityAddEncourages)
                    {
                        unit.finalEncourage = info.addEncourage;
                    }
                    break;
                case 1:
                    foreach (PersonalityAddEncourage info in unit.personalityAddEncourages)
                    {
                        if (info.disableTag == 1 || info.disableTag == 2 || info.disableTag == 3)
                        {
                            unit.finalEncourage = info.addEncourage;
                        }
                    }
                    break;
                default:
                    unit.finalEncourage = 0;
                    break;
            }
        }
    }

    /// <summary>
    /// 性格生效
    /// </summary>
    private void PersonalityTakeEffect(TestChar oneself, List<TestChar> combatUnits)
    {
        PassiveSkill_template passiveSkill = PassiveSkill_templateConfig.GetPassiveSkill_Template(oneself.passiveSkill);
        TestChar targetUnit;
        int personalityID;
        //性格需求表
        List<int> personalityReqIDs = new List<int>();
        for (int i = 0; i < passiveSkill.personalityList.Count; i++)
        {
            personalityID = passiveSkill.personalityList[i];
            if (personalityID == 0)
            {
                continue;
            }
            personalityReqIDs.Add(personalityID);
        }
        //检查需求
        switch (passiveSkill.reqType)
        {
            case 0: //无条件，必定生效
                AddTagAndReward(passiveSkill, oneself, combatUnits);
                break;
            case 1: //与指定性格（列表）的位置关系——相邻
                //先检查右边
                targetUnit = GetCharRightPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.personality))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                        break;
                    }
                }
                //检查左边
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.personality))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 2: //与指定性格（列表）的位置关系——相邻且在其后
                //检查右边
                targetUnit = GetCharRightPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.personality))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 3: //与指定性格（列表）的位置关系——相邻且在其前
                //检查左边
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.personality))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 4: //队友不包含指定性格（列表）
                List<int> temp = new List<int>();
                foreach (TestChar item in combatUnits)
                {
                    if (item.initIndex == oneself.initIndex)
                    {
                        continue;
                    }
                    temp.Add(item.personality);
                }
                bool isHaveAttitudeID = false;
                foreach (int item in personalityReqIDs)
                {
                    if (temp.Contains(item))
                    {
                        isHaveAttitudeID = true;
                        break;
                    }
                }
                //没有满足的性格
                if (!isHaveAttitudeID)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 5: //位于首位
                if (oneself.initIndex == 0)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 6: //不位于首位
                if (oneself.initIndex != 0)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 7: //位于最后一位
                if (oneself.initIndex == 3)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 8: //不位于最后一位
                if (oneself.initIndex != 3)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 9: //受到激励
                bool isHaveEncourage = false;
                foreach (PersonalityAddEncourage item in oneself.personalityAddEncourages)
                {
                    if (item.addEncourage > 0)
                    {
                        isHaveEncourage = true;
                        break;
                    }
                }
                if (isHaveEncourage)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 10:  //未受到激励  
                bool isNoHaveEncourage = false;
                foreach (PersonalityAddEncourage item in oneself.personalityAddEncourages)
                {
                    if (item.addEncourage > 0)
                    {
                        isNoHaveEncourage = true;
                        break;
                    }
                }
                if (!isNoHaveEncourage)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
        }
    }

    /// <summary>
    /// 添加标签和奖励
    /// </summary>
    private void AddTagAndReward(PassiveSkill_template passiveSkill, TestChar oneself, List<TestChar> combatUnits, TestChar targetUnit = null)
    {
        bool isAddReward = false;
        PersonalityAddEncourage addEncourageInfo;
        //添加标签
        List<int> tags = new List<int>();
        foreach (int item in passiveSkill.addTag)
        {
            if (item != 0)
            {
                tags.Add(item);
            }
        }
        //有标签和奖励
        if (tags.Count > 0)
        {
            foreach (int tag in tags)
            {
                switch (tag)
                {
                    case 1:
                        //添加目标奖励
                        if (!isAddReward && passiveSkill.rewardType == 2)
                        {
                            if (targetUnit == null)
                            {
                                targetUnit = GetCharRightPos(oneself, combatUnits);
                            }
                            if (targetUnit != null)
                            {
                                targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                                {
                                    addEncourage = passiveSkill.addEncourage,
                                });
                            }
                        }
                        //添加自身标签和奖励
                        addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                        {
                            disableTag = tag,
                        };
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                        }
                        oneself.personalityAddEncourages.Add(addEncourageInfo);
                        break;
                    case 4://自己加
                           //添加目标奖励
                        if (!isAddReward && passiveSkill.rewardType == 2)
                        {
                            if (targetUnit == null)
                            {
                                targetUnit = GetCharRightPos(oneself, combatUnits);
                            }
                            if (targetUnit != null)
                            {
                                targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                                {
                                    disableTag = tag,
                                    addEncourage = passiveSkill.addEncourage,
                                });
                            }
                        }
                        //添加自身标签和奖励
                        addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                        {
                            disableTag = tag,
                        };
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                        }
                        oneself.personalityAddEncourages.Add(addEncourageInfo);
                        break;
                    case 2://目标加
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        if (targetUnit == null)
                        {
                            targetUnit = GetCharRightPos(oneself, combatUnits);
                        }
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 5://目标加
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        if (targetUnit == null)
                        {
                            targetUnit = GetCharRightPos(oneself, combatUnits);
                        }
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 3://右侧和左侧的角色
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        targetUnit = GetCharRightPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                                isAddReward = true;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        targetUnit = GetCharLeftPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                                isAddReward = true;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 6: //右侧和左侧的角色
                            //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        targetUnit = GetCharRightPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        targetUnit = GetCharLeftPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                }
                isAddReward = true;
            }
            return;
        }
        //没有标签只有奖励
        switch (passiveSkill.rewardType)
        {
            case 1:
                oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                {
                    addEncourage = passiveSkill.addEncourage,
                });
                break;
            case 2:
                //如果没有就优先加给右边的角色——有角色就直接加指定人
                if (targetUnit == null)
                {
                    targetUnit = GetCharRightPos(oneself, combatUnits);
                }
                if (targetUnit != null)
                {
                    targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                    {
                        addEncourage = passiveSkill.addEncourage,
                    });
                    break;
                }
                //右边没有角色加给左边的角色
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                    {
                        addEncourage = passiveSkill.addEncourage,
                    });
                }
                break;
        }
    }

    private TestChar GetCharLeftPos(TestChar onself, List<TestChar> combatUnits)
    {
        return onself.initIndex == 3 ? null : combatUnits.Find(a => a.initIndex == onself.initIndex + 1);
    }
    private TestChar GetCharRightPos(TestChar onself, List<TestChar> combatUnits)
    {
        return onself.initIndex == 0 ? null : combatUnits.Find(a => a.initIndex == onself.initIndex - 1);
    }

    /// <summary>
    /// 获得关系数量
    /// </summary>
    /// <param name="combatUnit"></param>
    /// <param name="relationals"></param>
    /// <returns></returns>
    private int GetRelationalSum(TestChar combatUnit, List<int> relationals)
    {
        int sum = 0;
        foreach (PersonalityAddEncourage info in combatUnit.personalityAddEncourages)
        {
            if (info.disableTag == 4 && info.formeCharIndex != combatUnit.initIndex && relationals.Contains(4))
            {
                continue;
            }
            if (relationals.Contains(info.disableTag))
            {
                sum++;
            }
        }

        return sum;
    }
}

public class TestChar
{
    public int FinalEncourage { get { return finalEncourage; } }
    public int PersonalityAddPassiveSkill { get { return passiveSkill; } }

    public int initIndex;
    public int personality;
    public int passiveSkill;
    public int finalEncourage;
    public int personalityType;

    public List<PersonalityAddEncourage> personalityAddEncourages = new List<PersonalityAddEncourage>();

    public TestChar(int index, int value)
    {
        initIndex = index;
        SetPersonality(value);
    }
    public TestChar()
    {
    }

    public void ResetPersonalitEffect()
    {
        finalEncourage = 0;
        personalityAddEncourages.Clear();
    }
    private void SetPersonality(int value)
    {
        passiveSkill = value;
        PassiveSkill_template template = PassiveSkill_templateConfig.GetPassiveSkill_Template(value);
        personality = template.personalityID;
        personalityType = template.reqType;
    }
}

#endregion
