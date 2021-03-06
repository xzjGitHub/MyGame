﻿using System.Collections.Generic;


public class CharCreate
{
    /// <summary>
    /// 创建类型
    /// </summary>
    public CharCreateType createType;
    /// <summary>
    /// 角色id
    /// </summary>
    public int charID;
    /// <summary>
    /// 角色经验
    /// </summary>
    public int charExp;//0
    /// <summary>
    /// 角色等级
    /// </summary>
    public int charLevel;
    /// <summary>
    /// 角色模板
    /// </summary>
    public int templateID;
    /// <summary>
    /// 初始性格
    /// </summary>
    public int initialPersonality;
    /// <summary>
    /// 装备id列表
    /// </summary>
    public List<int> equipIdList;
    /// <summary>
    /// 技能id列表
    /// </summary>
    public List<int> skillIdList;


    public CharCreate(int templateId, CharCreateType type = CharCreateType.Initialize)
    {
        templateID = templateId;
        createType = type;
    }

    public CharCreate(int templateId, int charLevel, CharCreateType type = CharCreateType.Initialize)
    {
        if (charLevel != 0)
        {
            this.charLevel = charLevel;
        }
        templateID = templateId;
        createType = type;
    }
    public CharCreate(int templateId, int charLevel, int charID, CharCreateType type = CharCreateType.Initialize)
    {
        this.charLevel = charLevel;
        templateID = templateId;
        createType = type;
        //
        this.charID = charID;
    }

}

/// <summary>
/// 角色创建模式
/// </summary>
public enum CharCreateType
{
    /// <summary>
    /// 初始化
    /// </summary>
    Initialize = 0,
    /// <summary>
    /// 奖励
    /// </summary>
    Award = 1,
    /// <summary>
    /// 召唤
    /// </summary>
    Summon = 2,
    /// <summary>
    /// 任务
    /// </summary>
    Quest = 3,
}