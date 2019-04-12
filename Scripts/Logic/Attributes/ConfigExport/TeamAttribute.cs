using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// TeamAttribute
/// </summary>
public partial class TeamAttribute
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public List<CharAttribute> charAttribute;
    /// <summary>
    /// 宝箱几率奖励
    /// </summary>
    public float treasureSuccessChance;
    /// <summary>
    /// 宝箱物品奖励
    /// </summary>
    public float treasureAddRewardLevel;
    /// <summary>
    /// 挑战额外生命球加值
    /// </summary>
    public int addChallengeGlobChance;
    /// <summary>
    /// 战斗额外生命球加值
    /// </summary>
    public int addCombatGlobChance;
    /// <summary>
    /// 草药事件加值
    /// </summary>
    public int addHerbChance;
    /// <summary>
    /// 金属奖励加值
    /// </summary>
    public float addMetalReward;
    /// <summary>
    /// 宝石奖励加值
    /// </summary>
    public float addGemReward;
    /// <summary>
    /// 贵金属奖励加值
    /// </summary>
    public float addNobleMReward;
    /// <summary>
    /// 木材奖励加值
    /// </summary>
    public float addWoodReward;
    /// <summary>
    /// 纤维奖励加值
    /// </summary>
    public float addFibreReward;
    /// <summary>
    /// 皮革奖励加值
    /// </summary>
    public float addLeatherReward;
    /// <summary>
    /// 力量挑战加值
    /// </summary>
    public float addSTRReward;
    /// <summary>
    /// 耐力挑战加值
    /// </summary>
    public float addENDReward;
    /// <summary>
    /// 敏捷挑战加值
    /// </summary>
    public float addAGIReward;
    /// <summary>
    /// 智力挑战加值
    /// </summary>
    public float addINTReward;
    /// <summary>
    /// 感知挑战加值
    /// </summary>
    public float addPERReward;
    /// <summary>
    /// 战斗奖励加值
    /// </summary>
    public float addCombatReward;
    /// <summary>
    /// 金币献祭加值
    /// </summary>
    public float addGoldSacriReward;
    /// <summary>
    /// 物品献祭加值
    /// </summary>
    public float addItemSacriReward;
    /// <summary>
    /// 素材献祭加值
    /// </summary>
    public float addMaterialSacriReward;

}
