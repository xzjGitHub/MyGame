using ProtoBuf;


/// <summary>
/// buff信息
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class BuffInfo
{
    public int buffId;
    /// <summary>
    /// 创建时间
    /// </summary>
    public float createTime;

    public BuffInfo(int _id, float _time)
    {
        buffId = _id;
        createTime = _time;
    }
}

/// <summary>
/// 角色升级信息
/// </summary>
public class CharUpgradeInfo
{
    /// <summary>
    /// 角色id
    /// </summary>
    public int charId;
    /// <summary>
    /// 伤害
    /// </summary>
    public float finalDamage;
    /// <summary>
    /// 生命
    /// </summary>
    public float finalHP;
    /// <summary>
    /// 治疗
    /// </summary>
    public float finalHealing;
    /// <summary>
    /// 防御
    /// </summary>
    public float finalDRBRating;
    /// <summary>
    /// 格挡
    /// </summary>
    public float finalBCBRating;
    /// <summary>
    /// 暴击
    /// </summary>
    public float finalCHCRating;
    /// <summary>
    /// 精通
    /// </summary>
    public float finalSBRating;
    /// <summary>
    /// 升级数
    /// </summary>
    public float upgradeNum;
    /// <summary>
    /// 角色现在等级
    /// </summary>
    public int charNowLevel;
    /// <summary>
    /// 角色现在经验
    /// </summary>
    public float charNowExp;
    /// <summary>
    /// 初始等级
    /// </summary>
    public int initLevel;
    /// <summary>
    /// 初始经验
    /// </summary>
    public float initExp;


    public CharUpgradeInfo() { }

    public CharUpgradeInfo(CharAttribute _attribute)
    {
        if (_attribute == null)
        {
            return;
        }

        charId = _attribute.charID;
        finalDamage = 0;
        finalHP = _attribute.finalHP;
        initExp = _attribute.charExp;
        initLevel = _attribute.charLevel;
    }

    /// <summary>
    /// 更新升级信息
    /// </summary>
    /// <param name="_attribute"></param>
    public void UpdateUpgradeInfo(CharAttribute _attribute)
    {
        if (_attribute == null)
        {
            return;
        }

        finalDamage = 0;
        finalHP = _attribute.finalHP - finalHP;
        upgradeNum = _attribute.charLevel - initLevel;
        charNowExp = _attribute.charExp;
        charNowLevel = _attribute.charLevel;
    }
}
