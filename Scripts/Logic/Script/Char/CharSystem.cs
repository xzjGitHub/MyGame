using System.Collections.Generic;
using System.Linq;


/// <summary>
/// 角色系统
/// </summary>
public partial class CharSystem : ScriptBase
{
    private static CharSystem instance;

    public static CharSystem Instance { get { return instance; } }
    //
    private const string CharPath = "CharData";
    //
    private CharSystemData charSystemData;
    //
    private string parentPath;
    //
    private Dictionary<int, CharData> charDataList = new Dictionary<int, CharData>();
    private Dictionary<int, CharAttribute> charAttributeList = new Dictionary<int, CharAttribute>();
    //
    public Dictionary<int, CharAttribute> CharAttributeList { get { return charAttributeList; } }

    //
    public CharSystem()
    {
        instance = this;
    }

    public override void Init()
    {
        charAttributeList.Clear();
        //
        if (charSystemData == null) charSystemData = new CharSystemData();
        charDataList = charSystemData.charDataList;
        //已添加装备
        foreach (var item in charDataList)
        {
            CreateChar(item.Value);
        }
    }

    /// <summary>
    /// 读档
    /// </summary>
    /// <param name="parentPath"></param>
    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        charSystemData = GameDataManager.ReadData<CharSystemData>(parentPath + CharPath) as CharSystemData;
    }

    /// <summary>
    /// 存档
    /// </summary>
    public override void SaveData(string parentPath)
    {
        this.parentPath = parentPath;
        SaveCharDate();
        GameDataManager.SaveData(parentPath, CharPath, charSystemData);
    }


    /// <summary>
    /// 得到所有有buff的角色列表
    /// </summary>
    /// <returns></returns>
    public List<CharAttribute> GetAllHaveBuffCharList()
    {
        return (from item in CharAttributeList where item.Value.Buffs.Count > 0 select item.Value).ToList();
    }
    /// <summary>
    /// 得到角色属性
    /// </summary>
    /// <param name="charID"></param>
    /// <returns></returns>
    public CharAttribute GetAttribute(int charID)
    {
        return charAttributeList.ContainsKey(charID) ? charAttributeList[charID] : null;
    }
    /// <summary>
    /// 根据配置表ID获取唯一ID
    /// </summary>
    /// <param name="templateID"></param>
    /// <returns></returns>
    public int GetInstanceIDByTemplateID(int templateID)
    {
        foreach (var item in CharAttributeList.Values)
        {
            if (item.templateID == templateID)
            {
                return item.charID;
            }
        }
        return 0;
    }
    /// <summary>
    /// 获取角色的装备列表
    /// </summary>
    /// <param name="playerID">配置表ID</param>
    /// <returns></returns>
    public List<EquipAttribute> GetEquip(int playerID)
    {
        return CharAttributeList[playerID].equipAttribute;
    }
    /// <summary>
    /// 获取角色属性
    /// </summary>
    /// <param name="playerID"> 配置表ID</param>
    /// <returns></returns>
    public CharAttribute GetCharAttribute(int playerID)
    {
        if (!charAttributeList.ContainsKey(playerID))
        {
            LogHelperLSK.LogError(playerID);
        }
        return charAttributeList.ContainsKey(playerID) ? charAttributeList[playerID] : null;
    }


    /// <summary>
    /// 创建角色_存档
    /// </summary>
    private CharAttribute CreateChar(CharData charData)
    {
        charData.charID = charAttributeList.Count == 0 ? 1 : charAttributeList.Last().Key + 1;
        charAttributeList.Add(charData.charID, new CharAttribute(charData, charData.equipID));
        return charAttributeList[charData.charID];
    }
    /// <summary>
    /// 保存角色信息
    /// </summary>
    private void SaveCharDate()
    {
        charDataList.Clear();
        foreach (var item in charAttributeList)
        {
            charDataList.Add(item.Key, item.Value.GetCharData());
        }

        charSystemData.charDataList = charDataList;
    }

}