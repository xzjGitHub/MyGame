using System.Collections.Generic;
using System.Linq;

namespace MCCombat
{

    /// <summary>
    /// 技能信息
    /// </summary>
    public class CSkillInfo
    {
        public int castFrequency;
        public int isRandom;

        public int lastUseRound;

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get { return useCount; } }

        public bool IsMar { get { return isMar; } }

        public SkillType SkillType { get { return skillType; } }

        public int CourageCost { get { return courageCost; } }

        public int AlternativeSkill { get { return alternativeSkill; } }

        public int UseRound { get { return useRound; } }

        public int CurrentSkillCharge { get { return currentSkillCharge; } }

        public float ManaCost { get { return energyCost; } }

        public int ID { get { return id; } }

        public int CastType { get { return castType; } }

        public int Cooldown { get { return cooldown; } }

        public Combatskill_template Combatskill
        {
            get { return isIncentive ? alternativeTemplate : combatskill; }
        }

        /// <summary>
        /// 是否激励
        /// </summary>
        public bool isIncentive;
        public int incentiveFromIndex;

        /// <summary>
        /// 是否能使用
        /// </summary>
        /// <returns></returns>
        public bool IsUsable(int nowRound)
        {
            if (useRound != 0)
            {
                return false;
            }
            if (!CombatSystemTool.IsSkillCastFrequency(castFrequency, nowRound, lastUseRound))
            {
                return false;
            }
            if (!CombatSystemTool.IsSkillRandom(isRandom, castFrequency, nowRound))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新使用回合
        /// </summary>
        public void UpdateUseRound()
        {
            if (useRound == 0)
            {
                if (!isUse)
                {
                    return;
                }

                useRound = cooldown;
                isUse = false;
                return;
            }
            if (isUse && UpdateSkillCharge())
            {
                return;
            }
            useRound--;
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public void UseSkill()
        {
            isUse = true;
            useCount++;
            //重置计数
            if (useCount > configResetCount)
            {
                useCount = 1;
            }
            //更新Cd
            cooldown = GetInitSkillCD(skillCDReduction, configCooldown);
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        public void UseSkill(int nowRound)
        {
            lastUseRound = nowRound;
            UseSkill();
        }

        /// <summary>
        /// 新建技能
        /// </summary>
        /// <param name="id">技能id</param>
        /// <param name="skillCDReduction">技能CD修正</param>
        protected CSkillInfo(int id, float skillCDReduction, int cooldownDelay = 0)
        {
            this.skillCDReduction = skillCDReduction;
            Combatskill_template combatskillTemplate = Combatskill_templateConfig.GetCombatskill_template(id);
            if (combatskillTemplate == null)
            {
                isMar = true;
                return;
            }
            combatskill = combatskillTemplate;
            skillType = (SkillType)combatskillTemplate.skillType;
            this.id = id;
            configCooldown = combatskillTemplate.Cooldown;
            //使怪物的所有initialCooldown或 Cooldown>0的技能，的initialCooldown  = initialCooldown + value
            if (configCooldown > 0 || combatskillTemplate.initialCooldown > 0)
            {
                this.cooldownDelay = cooldownDelay;
            }
            cooldown = combatskillTemplate.Cooldown;
            useRound = GetInitSkillCD(skillCDReduction, combatskillTemplate.initialCooldown);
            castType = combatskillTemplate.castType;
            energyCost = combatskillTemplate.energyCost;
            configeSkillCharge = combatskillTemplate.skillCharge;
            currentSkillCharge = configeSkillCharge;
            configResetCount = combatskillTemplate.castCount;
            courageCost = 0;
            alternativeSkill = combatskillTemplate.alternativeSkill;
            alternativeTemplate = Combatskill_templateConfig.GetCombatskill_template(alternativeSkill);
        }

        /// <summary>
        /// 更新充能
        /// </summary>
        private bool UpdateSkillCharge()
        {
            if (!isUse)
            {
                return false;
            }
            if (currentSkillCharge == 0)
            {
                currentSkillCharge = configeSkillCharge;
                return false;
            }
            //
            currentSkillCharge--;
            return true;
        }
        /// <summary>
        /// 获得技能CD
        /// </summary>
        /// <returns></returns>
        private int GetInitSkillCD(float skillCDReduction, int cooldown)
        {
            cooldown += cooldownDelay;
            float avgInitialCoolDown = cooldown / (1 + skillCDReduction);
            //
            if (avgInitialCoolDown % 1 != 0)
            {
                avgInitialCoolDown = (int)avgInitialCoolDown + (RandomBuilder.RandomIndex_Chances(
                                  new List<float> { (avgInitialCoolDown - (int)avgInitialCoolDown) * 10000 }) == 0 ? 1 : 0);
            }
            //
            return (int)avgInitialCoolDown;
        }

        /// <summary>
        /// 技能ID
        /// </summary>
        private readonly int id;
        /// <summary>
        /// 施放类型
        /// </summary>
        private readonly int castType;
        /// <summary>
        /// 能量消耗
        /// </summary>
        private readonly float energyCost;
        /// <summary>
        /// 当前技能充能
        /// </summary>
        private int currentSkillCharge;
        /// <summary>
        /// 冷却
        /// </summary>
        private int cooldown;
        /// <summary>
        /// 使用回合
        /// </summary>
        private int useRound;
        /// <summary>
        /// 技能CD减少比例
        /// </summary>
        private readonly float skillCDReduction;
        /// <summary>
        /// 配置冷却
        /// </summary>
        private readonly int configCooldown;
        /// <summary>
        /// 使用次数
        /// </summary>
        private int useCount;
        /// <summary>
        /// 是否使用过
        /// </summary>
        private bool isUse;
        /// <summary>
        /// 配置重置次数
        /// </summary>
        private readonly int configResetCount;
        /// <summary>
        /// 冷却前多用次数
        /// </summary>
        private readonly int configeSkillCharge;
        /// <summary>
        /// 是否损毁
        /// </summary>
        private readonly bool isMar;
        /// <summary>
        /// 技能类型
        /// </summary>
        private readonly SkillType skillType;
        /// <summary>
        /// 激励消耗
        /// </summary>
        private readonly int courageCost;

        private readonly int cooldownDelay;
        /// <summary>
        /// 替换技能
        /// </summary>
        private readonly int alternativeSkill;
        private readonly Combatskill_template combatskill;
        private readonly Combatskill_template alternativeTemplate;
    }


    /// <summary>
    /// 手动技能
    /// </summary>
    public class ManualSkillInfo : CSkillInfo
    {
        public ManualSkillInfo(int id, float skillCDReduction, int cooldownDelay = 0) : base(id, skillCDReduction, cooldownDelay) { }

        /// <summary>
        /// 是否能被使用
        /// </summary>
        /// <param name="powerValue"></param>
        /// <returns></returns>
        public bool IsUsable(int powerValue, int nowRound)
        {
            return base.IsUsable(nowRound) && powerValue >= ManaCost;
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="incentiveValue">激励值</param>
        /// <param name="newSkillID">新技能id</param>
        /// <returns></returns>
        public void UseSkill()
        {
            base.UseSkill();
        }

    }
    /// <summary>
    /// 战斗技能信息
    /// </summary>
    public class CombatSkillInfo : CSkillInfo
    {

        /// <summary>
        /// 获得使用技能信息
        /// </summary>
        /// <param name="activeHealing"></param>
        /// <param name="combatSkills"></param>
        /// <param name="combatUnits"></param>
        /// <param name="newSkill"></param>
        public CombatSkillInfo GetUseSkillInfo(float activeHealing, List<CSkillInfo> combatSkills, List<CombatUnit> combatUnits, int nowRound)
        {
            if (activeHealing <= 0)
            {
                return this;
            }

            foreach (CSkillInfo item in combatSkills)
            {
                // skillType = 2的技能并未处于冷却中
                if (item.SkillType != SkillType.Heal || !item.IsUsable(nowRound))
                {
                    continue;
                }
                //优先治疗
                if (combatUnits.Any(combatUnit => combatUnit.LoseHp >= activeHealing))
                {
                    return item as CombatSkillInfo;
                }
            }
            return this;
        }


        public CombatSkillInfo(int id, float skillCDReduction, int cooldownDelay = 0) : base(id, skillCDReduction, cooldownDelay) { }

    }
    /// <summary>
    /// 通用技能
    /// </summary>
    public class CommonSkillInfo : CSkillInfo
    {
        /// <summary>
        /// 类型:1=自己，2=指挥
        /// </summary>
        public CommonSkillType CommonType { get { return (CommonSkillType)commonType; } }




        /// <summary>
        /// 是否能被使用
        /// </summary>
        /// <returns></returns>
        public bool IsUsable(List<State> states, int nowRound)
        {
            if (!base.IsUsable(nowRound))
            {
                return false;
            }
            foreach (State item in states)
            {
                if ((item.stateType == 1 || item.stateType == 2) && item.canBeDispelled == 1)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="powerValue"></param>
        /// <param name="combatUnit"></param>
        /// <returns></returns>
        public bool UseSkill(int powerValue, int nowRound, CombatUnit combatUnit)
        {
            if (!base.IsUsable(nowRound))
            {
                return false;
            }
            if (powerValue < ManaCost)
            {
                return false;
            }
            base.UseSkill();
            combatUnit.RemoveState(1);
            combatUnit.RemoveState(2);
            return true;
        }


        public CommonSkillInfo(int id, float skillCDReduction, int cooldownDelay = 0) : base(id, skillCDReduction, cooldownDelay)
        {
            if (Combatskill == null)
            {
                return;
            }
            commonType = Combatskill.commonType;
            if (commonType == 0)
            {
                commonType = 1;
            }
        }

        /// <summary>
        /// 类型:1=自己，2=指挥
        /// </summary>
        private readonly int commonType;
    }

    /// <summary>
    /// Boss技能
    /// </summary>
    public class BossSkillInfo : CSkillInfo
    {
        public Bossskill_template bossSkill;

        public BossSkillInfo(Bossskill_template bossSkill, float skillCDReduction, int cooldownDelay) : base(bossSkill.skillID, skillCDReduction, cooldownDelay)
        {
            this.bossSkill = bossSkill;
            base.castFrequency = bossSkill.castFrequency;
            base.isRandom = bossSkill.isRandom;
        }

        /// <summary>
        /// 是否能被使用
        /// </summary>
        public bool IsUsable(int nowRound)
        {
            return base.IsUsable(nowRound);
        }
        /// <summary>
        /// 使用技能
        /// </summary>
        public void UseSkill(int nowRound)
        {
            base.UseSkill(nowRound);
        }

    }


    /// <summary>
    /// 玩家技能
    /// </summary>
    public class PlayerSkillInfo
    {
        public Combatskill_template CombatskillTemplate { get { return combatskill_Template; } }

        public int SelectIndex { get { return selectIndex; } }

        public int skillIndex;
        /// <summary>
        /// 技能id
        /// </summary>
        public int skillId;
        /// <summary>
        /// 能量消耗
        /// </summary>
        public float energyCost;
        /// <summary>
        /// 技能类型
        /// </summary>
        public int skillType;
        public int castType;
        //
        public int charID;
        public int charIndex;
        public int teamID;
        //是否激励
        public bool isIncentive;

        /// <summary>
        /// 设置选择索引
        /// </summary>
        /// <param name="index"></param>
        public void SetSelectIndex(int index)
        {
            selectIndex = index;
        }



        public PlayerSkillInfo(int id)
        {
            Init(id);
        }
        public PlayerSkillInfo(int id, CombatUnit combatUnit)
        {
            Init(id);
            teamID = combatUnit.teamId;
            charID = combatUnit.charAttribute.charID;
            charIndex = combatUnit.initIndex;
        }

        private void Init(int id)
        {
            skillId = id;
            combatskill_Template = Combatskill_templateConfig.GetCombatskill_template(id);
            if (combatskill_Template == null)
            {
                return;
            }
            castType = combatskill_Template.castType;
            energyCost = combatskill_Template.energyCost;
            skillType = combatskill_Template.skillType;
        }

        /// <summary>
        /// 选择索引
        /// </summary>
        private int selectIndex;
        private Combatskill_template combatskill_Template;
    }

    public enum CommonSkillType
    {
        Default = 0,
        Oneself = 1,
        All = 2,
    }
}



