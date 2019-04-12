using ProtoBuf;

/// <summary>
/// 军衔类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum CharRank
{
    /// <summary>
    /// 新手=0
    /// </summary>
    XinShou = 0,
    /// <summary>
    /// E
    /// </summary>
    E = 1,
    /// <summary>
    /// D
    /// </summary>
    D = 2,
    /// <summary>
    /// C
    /// </summary>
    C = 3,
    /// <summary>
    /// B
    /// </summary>
    B = 4,
    /// <summary>
    /// A
    /// </summary>
    A = 5,
    /// <summary>
    /// S
    /// </summary>
    S = 6,
}

/// <summary>
/// 技能类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum SkillType
{
    /// <summary>
    /// 普通
    /// </summary>
    Normal = 1,
    /// <summary>
    /// 治疗
    /// </summary>
    Heal = 2,
    /// <summary>
    /// 战术
    /// </summary>
    Tactical = 3,
    /// <summary>
    /// 被动
    /// </summary>
    Passive = 4,
    /// <summary>
    /// 手动
    /// </summary>
    Manual = 5,
    /// <summary>
    /// 通用
    /// </summary>
    Common = 6,
}

/// <summary>
/// 角色状态
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum CharStatus
{
    /// <summary>
    /// 空闲
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 战斗队伍
    /// </summary>
    InCombat = 2,
    /// <summary>
    /// 战斗占用
    /// </summary>
    CbtOccupied = 4,
    /// <summary>
    /// 战斗休息
    /// </summary>
    CombatRest = 8,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 16,
    /// <summary>
    /// 训练中
    /// </summary>
    Train = 32,
    /// <summary>
    /// 金币制造
    /// </summary>
    GoldProduce = 64,
    /// <summary>
    /// 附魔研究
    /// </summary>
    EnchantResearch = 128,
    /// <summary>
    /// 装备研究
    /// </summary>
    EquipResearch = 526
}
/// <summary>
/// 角色位置
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum CharPos
{
    /// <summary>
    /// 空闲
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 作坊
    /// </summary>
    Workshop = 2,
    /// <summary>
    /// 远征
    /// </summary>
    Expedition = 3,
}

/// <summary>
/// 事件访问类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum WPEventVisitType
{
    /// <summary>
    /// 放弃访问
    /// </summary>
    Abandon = 0,
    /// <summary>
    /// 普通访问
    /// </summary>
    Normal = 1,
    /// <summary>
    /// 额外普通访问
    /// </summary>
    NormalExt = 2,
    /// <summary>
    /// 高级访问
    /// </summary>
    Advanced = 3,
    /// <summary>
    /// 付费访问
    /// </summary>
    Pay = 4,
}


/// <summary>
/// 路点事件类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum WPEventType
{
    /// <summary>
    /// 草丛1
    /// </summary>
    Grass = 1,
    /// <summary>
    /// 残骸2
    /// </summary>
    Remains = 2,
    /// <summary>
    /// 战斗3
    /// </summary>
    Combat = 3,
    /// <summary>
    /// Boss4
    /// </summary>
    Boss = 4,
    /// <summary>
    /// 宝藏5
    /// </summary>
    Treasure = 5,
    /// <summary>
    /// 囊泡6
    /// </summary>
    NangPao = 6,
    /// <summary>
    /// 召唤7
    /// </summary>
    Summon = 7,
    /// <summary>
    /// 陷阱8
    /// </summary>
    Trap = 8,
    /// <summary>
    /// 抉择9
    /// </summary>
    Choice = 9,
    /// <summary>
    /// 草药10
    /// </summary>
    Herb = 10,
    /// <summary>
    /// 异常
    /// </summary>
    Abnormal = 11,
}

/// <summary>
/// 事件访问结果
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum WPEventVisitResult
{
    /// <summary>
    /// 高级 
    /// </summary>
    Jckpot = 1,
    /// <summary>
    /// 成功
    /// </summary>
    Success = 2,
    /// <summary>
    /// 失败
    /// </summary>
    Failure = 3,
    /// <summary>
    /// 埋伏
    /// </summary>
    Ambush = 4,
    /// <summary>
    /// 陷阱
    /// </summary>
    Trap = 5,
}


/// <summary>
/// 技能动画位置类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum SkillAnimationPosType
{
    /// <summary>
    /// 靠近敌方
    /// </summary>
    NearEnemy = 1,
    /// <summary>
    /// 敌方中间
    /// </summary>
    EnemyMiddle = 2,
    /// <summary>
    /// 靠近自己
    /// </summary>
    NearOneself = 5,
    /// <summary>
    /// 自己中间
    /// </summary>
    OneselfMiddle = 4,
    /// <summary>
    /// 原地
    /// </summary>
    Inplace = 3,
    /// <summary>
    /// 屏幕中间
    /// </summary>
    Middle = 6,
}


/// <summary>
/// 队伍类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum TeamType
{
    Player = 0, //自己
    Enemy = 1, //敌人
    Thirdparty = 2,
}

/// <summary>
/// 角色品质
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum CharQuality
{
    /// <summary>
    /// 普通
    /// </summary>
    Common = 1,
    /// <summary>
    /// 非凡
    /// </summary>
    Uncommon = 2,
    /// <summary>
    /// 稀有
    /// </summary>
    Rare = 3,
    /// <summary>
    ///  史诗
    /// </summary>
    Epic = 4,
    /// <summary>
    /// 传说
    /// </summary>
    Legendary = 5,
}
/// <summary>
/// 物品类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum ItemType
{
    WuQi = 1,
    KuiJia = 2,
    ShiPing = 3,
    MoJing = 4,
    ZhuCai = 5,
    FuCai = 6,
    YiWu = 7,
    XiSu = 8,
    QiYue = 9,
    LingHai = 10,
    ShengWu = 11,
    Task = 12,
    CaiBao = 13,
}

/// <summary>
/// 威胁类型
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public enum ThreatType
{
    /// <summary>
    /// 狗头人
    /// </summary>
    GouTouRen = 1,
    /// <summary>
    /// 伪军
    /// </summary>
    WeiJun = 4,
    /// <summary>
    /// 古神教
    /// </summary>
    GuShenJiao = 6,
    /// <summary>
    /// 狼人
    /// </summary>
    LangRen = 5,
    /// <summary>
    /// 亡灵
    /// </summary>
    WangLing = 2,
    /// <summary>
    /// 蜥蜴人
    /// </summary>
    XiYiRen = 3,
    /// <summary>
    /// 蛇人
    /// </summary>
    SheRen = 7,
    /// <summary>
    /// 古神
    /// </summary>
    GuShen = 8,
}

/// <summary>
/// 战斗
/// </summary>

namespace MCCombat
{
    /// <summary>
    /// 目标类型
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum TargetType
    {
        /// <summary>
        /// 首要敌方目标
        /// </summary>
        OtherPrimary = 1,
        /// <summary>
        /// 偷袭敌方目标
        /// </summary>
        OtherSneak = 2,
        /// <summary>
        /// 随机敌方目标
        /// </summary>
        OtherRandom = 3,
        /// <summary>
        /// 上一名行动的敌人
        /// </summary>
        OtherPreviousAction = 4,
        /// <summary>
        /// 下一名行动的敌人
        /// </summary>
        OtherNextAction = 5,
        /// <summary>
        ///  生命比例最低的敌人
        /// </summary>
        OtherHpMin = 6,
        /// <summary>
        /// 生命比例最高的敌人
        /// </summary>
        OtherHpMax = 7,
        /// <summary>
        /// 从敌第2位开始向后计
        /// </summary>
        OtherSecondStart = 8,
        /// <summary>
        /// 从敌第1位以后随机取一个
        /// </summary>
        OtherFirstAfterRandom = 9,
        /// <summary>
        /// 攻击与施法者index相同的敌方队伍目标
        /// </summary>
        OtherSameIndex = 10,
        /// <summary>
        /// 自身
        /// </summary>
        Oneself = 11,
        /// <summary>
        /// 首要己方
        /// </summary>
        SamePrimary = 12,
        /// <summary>
        /// 偷袭己方
        /// </summary>
        SameSneak = 13,
        /// <summary>
        /// 随机友方
        /// </summary>
        SameRandom = 14,
        /// <summary>
        /// 上一名行动的友方
        /// </summary>
        SamePreviousAction = 15,
        /// <summary>
        /// 下一名行动的友方
        /// </summary>
        SameNextAction = 16,
        /// <summary>
        /// 生命比例最低的友方
        /// </summary>
        SameHpMin = 17,
        /// <summary>
        /// 死亡己方角色
        /// </summary>
        SameDie = 18,
        /// <summary>
        /// 相同队伍
        /// </summary>
        SameTeam = 19,
        /// <summary>
        /// 治疗标签
        /// </summary>
        HealingTag = 20,
        /// <summary>
        /// 高危险
        /// </summary>
        HighThreat = 21,
        /// <summary>
        /// 吸血
        /// </summary>
        XiXue = 22,
        /// <summary>
        /// 相同状态
        /// </summary>
        SameState = 23,
        /// <summary>
        /// beingHit最低的后排
        /// </summary>
        BeingHitMin = 90,
        /// <summary>
        /// beingHit最高的目标
        /// </summary>
        BeingHitAllMax = 91,
        /// <summary>
        /// 2、3、4号位置中beingHit最高的目标
        /// </summary>
        BeingHitMax = 92,
    }

    /// <summary>
    /// 攻击类型
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum HitResult
    {

        Miss = 0,
        /// <summary>
        /// 暴击
        /// </summary>
        Critical = 1,
        /// <summary>
        /// 格挡
        /// </summary>
        Block = 2,
        /// <summary>
        /// 命中
        /// </summary>
        Hit = 3,
        /// <summary>
        /// 躲避
        /// </summary>
        Dodge = 4,
        /// <summary>
        /// 吸收
        /// </summary>
        Absorb = 5,
    }

    /// <summary>
    /// 技能效果
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum SkillEffect
    {
        /// <summary>
        /// 物理伤害
        /// </summary>
        WuShang = 1,
        /// <summary>
        /// 法伤
        /// </summary>
        FaShang = 2,
        /// <summary>
        /// 真伤
        /// </summary>
        ZhenShang = 3,
        /// <summary>
        /// 甲伤
        /// </summary>
        JiaShang = 4,
        /// <summary>
        /// 盾伤
        /// </summary>
        DunShang = 5,
        /// <summary>
        /// 血伤
        /// </summary>
        XueShang = 6,
        /// <summary>
        /// 回甲
        /// </summary>
        HuiJia = 7,
        /// <summary>
        /// 回盾
        /// </summary>
        HuiDun = 8,
        /// <summary>
        /// 回血
        /// </summary>
        HuiXue = 9,
        /// <summary>
        /// 真伤盾
        /// </summary>
        ZhenShangDun = 10,
        /// <summary>
        /// 临时护甲
        /// </summary>
        LinShiHuJia = 11,
        /// <summary>
        /// 临时护盾
        /// </summary>
        LinShiHuDun = 12,
        /// <summary>
        /// 临时生命
        /// </summary>
        LinShiShengMing = 13,
        /// <summary>
        /// 伤害增幅
        /// </summary>
        ShangHaiZengFu = 14,
        /// <summary>
        /// 物伤增幅
        /// </summary>
        WuShangZengFu = 15,
        /// <summary>
        /// 法伤增幅
        /// </summary>
        FuShangZengFu = 16,
        /// <summary>
        /// 易伤
        /// </summary>
        YiShang = 17,
        /// <summary>
        /// 物伤易伤
        /// </summary>
        WuShangYiShang = 18,
        /// <summary>
        /// 法伤易伤
        /// </summary>
        FaShangYiShang = 19,
        /// <summary>
        /// 物伤降低
        /// </summary>
        WuShangJianDi = 20,
        /// <summary>
        /// 法伤降低
        /// </summary>
        FaShangJianDi = 21,
        /// <summary>
        /// 击晕
        /// </summary>
        JiYun = 22,
        /// <summary>
        /// 护盾反伤
        /// </summary>
        HuDunFanShang = 23,
        /// <summary>
        /// 护甲反伤
        /// </summary>
        HuJiaFanShang = 24,
        /// <summary>
        /// 被动复活
        /// </summary>
        FuHuo_BeiDong = 25,
        /// <summary>
        /// 复活
        /// </summary>
        FuHuo_ZhuDong = 26,
        /// <summary>
        /// 临时复活
        /// </summary>
        FuHuo_LinShi = 27,
        /// <summary>
        /// 召唤
        /// </summary>
        ZhaoHuan = 28,
        /// <summary>
        /// 消耗能量
        /// </summary>
        Energy = 29,
        /// <summary>
        /// 负生命
        /// </summary>
        NegativeHP = 30,
        /// <summary>
        /// 切换治疗标签
        /// </summary>
        SwitchHealingTag = 31,
        /// <summary>
        /// 激励个人
        /// </summary>
        IncentiveChar = 32,
        /// <summary>
        /// 激励队伍
        /// </summary>
        IncentiveTeam = 33,
        /// <summary>
        /// 保护
        /// </summary>
        Defended = 34,
        /// <summary>
        /// 消失
        /// </summary>
        Hidden = 35,
        /// <summary>
        /// 指定复活
        /// </summary>
        FuHuo_ZhiDing = 36,
    }

    /// <summary>
    /// 状态加载类型
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum StateLoadType
    {
        /// <summary>
        /// 必定加载
        /// </summary>
        Any = 1,
        /// <summary>
        /// 周期加载
        /// </summary>
        Frequency = 2,
    }

    /// <summary>
    /// 状态类型
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum StateType
    {
        /// <summary>
        /// 常效型
        /// </summary>
        Constant = 1,
        /// <summary>
        /// 脉冲型
        /// </summary>
        Impulse = 2,
        /// <summary>
        /// 瞬间型
        /// </summary>
        Instant = 3,
        /// <summary>
        /// 光环
        /// </summary>
        Aura = 4,
        /// <summary>
        /// 激励
        /// </summary>
        Incentive = 5,
        /// <summary>
        /// 生命触发
        /// </summary>
        HpTrigger = 6,
        /// <summary>
        /// 受击触发
        /// </summary>
        HitTrigger = 7,
        /// <summary>
        /// 超级脉冲型
        /// </summary>
        SuperImpulse = 8,
    }
}