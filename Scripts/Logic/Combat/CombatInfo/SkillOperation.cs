using System.Collections.Generic;
using System.Linq;
using MCCombat;

public partial class CombatSystem
{
    public Dictionary<int, PlayerSkillInfo> PlayerSkills
    {
        get { return playerSkills; }
    }

    /// <summary>
    /// 创建职业技能_暂时只有玩家队伍有
    /// </summary>
    private void CreateClassSkill(bool isNewCreate = false, CombatTeamInfo combatTeamInfo = null)
    {
        //满卡
        if (playerSkills.Values.All(a => a != null)) return;
        //补卡
        List<int> indexs = new List<int>();
        foreach (var item in playerSkills)
        {
            if (item.Value == null) indexs.Add(item.Key);
        }
        foreach (var item in indexs)
        {
            AddSubstituteSkillId(item, combatTeamInfo);
        }
    }

    /// <summary>
    /// 获得替补技能ID
    /// </summary>
    private void AddSubstituteSkillId(int index, CombatTeamInfo combatTeamInfo = null)
    {
     //   int skillID;
        //bool isUse;
        ////a)如果玩家没有effectCategory = 1或2的combatSkill，则从cardSet1中，筛选1个手动技能； 
        //isUse = IsSkllEffectCategory(new List<int> { 1, 2 });
        //if (!isUse)
        //{
        //    skillID = CanUseSkillId(combat_Config.cardSet1, combatTeamInfo);
        //    AddPlayerSkillInfo(index, skillID);
        //    return;
        //}
        //// b)如果玩家没有effectCategory = 3或4的combatSkill，则从cardSet3中，筛选1个手动技能；
        //isUse = IsSkllEffectCategory(new List<int> { 3, 4 });
        //if (!isUse)
        //{
        //    skillID = CanUseSkillId(combat_Config.cardSet3, combatTeamInfo);
        //    AddPlayerSkillInfo(index, skillID);
        //    return;
        //}
        ////d)如果玩家只拥有1个effectCategory = 3或4的combatSkill（0<,<2），则从cardSet4中，筛选1个手动技能。
        //bool isUse1 = IsSkllEffectCategory(new List<int> { 1 });
        //bool isUse2 = IsSkllEffectCategory(new List<int> { 2 });
        ////2个都有
        //if (isUse1 && isUse2)
        //{
        //    skillID = CanUseSkillId(combat_Config.cardSet4, combatTeamInfo);
        //    AddPlayerSkillInfo(index, skillID);
        //    return;
        //}
        ////
        //skillID = CanUseSkillId(combat_Config.cardSet2, combatTeamInfo);
       // AddPlayerSkillInfo(index, skillID);
    }
    /// <summary>
    /// 添加卡牌
    /// </summary>
    private void AddPlayerSkillInfo(int index, int skillID, bool isPlayer = true)
    {
        if (!playerSkills.ContainsKey(index)) playerSkills.Add(index, null);
        if (skillID <= 0) return;
        playerSkills[index] = new PlayerSkillInfo(skillID);
    }

    /// <summary>
    /// 得到可用技能列表
    /// </summary>
    private int CanUseSkillId(List<int> skillSets, CombatTeamInfo combatTeamInfo = null)
    {
        if (combatTeamInfo == null) combatTeamInfo = playerTeamInfo;
        bool isSelect = true;
        int id;
        while (true)
        {
            if (skillSets.Count == 0) return -1;
            id = RandomBuilder.RandomList(1, skillSets)[0];
            combatskill_Template = Combatskill_templateConfig.GetCombatskill_template(id);
           // if (combatskill_Template.isUnique != 1) break;
            //检查是否有这个技能
            var isHave = false;
            foreach (var skill in playerSkills.Values)
            {
                if (skill == null || skill.skillId != id) continue;
                isHave = true;
                break;
            }
            if (isHave)
            {
                skillSets.Remove(id);
            }
            else
            {
                break;
            }
        }
        //
        //foreach (var item in combatskill_Template.skillRank)
        //{
        //    //解锁条件：1、指定角色存活
        //    var units = combatTeamInfo.combatUnits.FindAll(a => a.hp != 0).ToList();
        //    if (units.Count == 0) continue;
        //    if (units.All(b => b.charAttribute.char_template.templateID != combatskill_Template.charReq)) continue;
        //    id = item;
        //    break;
        //}
        return id;
    }

    /// <summary>
    /// 使用职业技能
    /// </summary>
    private bool UseClassSkill(PlayerSkillInfo playerSkillInfo)
    {
        if (playerSkillInfo == null) return false;
        playerSkills[playerSkillInfo.skillIndex] = null;
        return true;
    }

    /*
     
2手动技能机制
2.1抽卡机制
更新了手动技能的选择机制，新的选择机制如下：
在第1回合，从4个随机集合中，每个随机集合分别为玩家选择1个手动技能（共4个）；
列表中的手动技能会是重复的。
随机集合读自combat_config. cardSet1~cardSet4。

玩家可以对手动技能使用3种操作：
1.使用技能：当玩家使用1个手动技能时，该手动技能生效，消耗能量，并从列表中移除（不影响重复的其他手动技能）。
2.保留技能：玩家未使用的手动技能会自动保留，直到被“使用”或被“放弃”；
3.放弃技能：玩家可以主动放弃手动技能。被放弃的手动技能被移除，不生效，也不消耗能量。

在每回合开始时，检查玩家保留的手动技能的数量是否 < 4。
如果数量 < 4，则使用补卡机制，将技能数量补足到4；
如果数量 = 4，则无视。

补卡机制：
第2回合开始，使用补卡机制。补卡机制与首回合的抽卡机制不同。
补卡机制的流程如下：
首先检查玩家保留的手动技能的Combatskill_template. effectCategory（以下检查顺序敏感）；
a)如果玩家没有effectCategory = 1或2的combatSkill，则从cardSet1中，筛选1个手动技能； 
b)如果玩家没有effectCategory = 3或4的combatSkill，则从cardSet3中，筛选1个手动技能；
c)如果玩家只拥有1个effectCategory = 1或2的combatSkill（0<,<2），则从cardSet2中，筛选1个手动技能；
d)如果玩家只拥有1个effectCategory = 3或4的combatSkill（0<,<2），则从cardSet4中，筛选1个手动技能。

     
     
     */

    private Dictionary<int, PlayerSkillInfo> playerSkills = new Dictionary<int, PlayerSkillInfo>();
    private Dictionary<int, PlayerSkillInfo> monsterSkills = new Dictionary<int, PlayerSkillInfo>();
    private Combatskill_template combatskill_Template;


}