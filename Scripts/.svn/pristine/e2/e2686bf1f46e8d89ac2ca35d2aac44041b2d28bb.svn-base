using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    ItemIcon,
    SkillIcon,
    EquipTypeIcon,
    CharHeadIcon,
    CharType,
    CharRank,
    CharAttrIcon,
    RuneQuelity,
    CharStatus,
    Other
}

public partial class ResourceLoadUtil
{
    private static string m_playerPath = "UI/Character/";
    //private static string m_playerPath = "char/model/Anima_";
    private static string m_skillPath = "Skill/skill_";
    private static string m_spritePath = "UI/Sprite/";
    private static string m_prefabPath = "UI/Prefab/";
    private static string m_panelPath = "UI/Panel/";
    private static string m_audioPath = "Audio/";
    private static string m_uiEffectPath = "UI_Effect/";
    private static string m_npcPath = "UI/Npc/";


    /// <summary>
    /// 加载角色
    /// </summary>
    /// <param name="playerId">角色id</param>
    /// <returns></returns>
    public static GameObject LoadRole(int roleId)
    {
        GameObject gameObject = Resources.Load<GameObject>(m_playerPath + roleId);
        if(gameObject != null)
        {
            return GameObject.Instantiate(gameObject) as GameObject;
        }
        else
        {

            LogHelperLSK.Log("加载Role出错,roleId: " + roleId);
            return null;
        }
    }


    public static GameObject LoadRole(string name)
    {
        GameObject gameObject = Resources.Load<GameObject>(m_playerPath + name);
        if(gameObject != null)
        {
            return GameObject.Instantiate(gameObject) as GameObject;
        }
        else
        {

            LogHelperLSK.Log("加载Role出错,name: " + name);
            return null;
        }
    }

    public static GameObject LoadUIEffect(string effectName)
    {
        GameObject gameObject = Resources.Load<GameObject>(m_uiEffectPath + effectName);
        if(gameObject != null)
        {
            return GameObject.Instantiate(gameObject) as GameObject;
        }
        else
        {
            LogHelperLSK.Log("加载Role出错,effectName: " + effectName);
            return null;
        }
    }

    public static GameObject LoadNpc(string npcName)
    {
        GameObject npc = Resources.Load<GameObject>(m_npcPath + npcName);
        return npc;
    }



    /// <summary>
    /// 加载prefab
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(string path,bool needInstaniate = true)
    {
        string ph = m_prefabPath + path;
        GameObject gameObject = Resources.Load<GameObject>(ph);
        if(gameObject != null)
        {
            if(needInstaniate)
            {
                return GameObject.Instantiate(gameObject) as GameObject;
            }
            else
            {
                return gameObject;
            }
        }
        else
        {
            LogHelperLSK.Log("加载prefab出错,path: " + path);
            return null;
        }
    }

    public static GameObject LoadPanelByPath(string path)
    {
        GameObject gameObject = Resources.Load<GameObject>(m_panelPath + path);
        if(gameObject != null)
        {
            return GameObject.Instantiate(gameObject) as GameObject;
        }
        else
        {
            LogHelperLSK.Log("加载prefab出错,path: " + path);
            return null;
        }
    }


    /// <summary>
    /// 加载sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string path)
    {
        Sprite spri = Resources.Load<Sprite>(m_spritePath + path);
        if(spri != null)
        {
            return spri;
        }
        else
        {
            LogHelperLSK.Log("加载spite出错，path: " + path);
            return null;
        }
    }

    public static Sprite LoadItemIcon(ItemAttribute itemAttribute)
    {
        Sprite spri = null;

        if (itemAttribute == null)
            return spri;

        string iconName = itemAttribute.GetItemData().itemIconName;
        
        if (string.IsNullOrEmpty(iconName) || iconName=="")
        {
            List<string> list = Item_instanceConfig.GetItemInstance(itemAttribute.instanceID).itemIcon;
            if (list.Count > 0)
            {
                iconName = list[0];
            }
        }

        spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemIcon,iconName);
        return spri;
    }

    public static Sprite LoadItemIcon(EquipmentData data)
    {
        Sprite spri = null;

        if(data == null)
            return spri;

        string iconName = data.itemIconName;

        if(string.IsNullOrEmpty(iconName) || iconName == "")
        {
            List<string> list = Item_instanceConfig.GetItemInstance(data.instanceID).itemIcon;
            if(list.Count > 0)
            {
                iconName = list[0];
            }
        }

        spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemIcon,iconName);
        return spri;
    }

    public static Sprite LoadItemIcon(int itemId)
    {
        var itemAttribute = ItemSystem.Instance.GetItemAttribute(itemId);
        return LoadItemIcon(itemAttribute);
    }


    public static Sprite LoadItemQuiltySprite(int quilty)
    {
       return SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemQuilty, quilty.ToString());
    }

    public static Sprite LoadItemQuiltyFrameSprite(int quilty)
    {
        return SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemQuilty,quilty.ToString());
    }

    public static Sprite LoadSprite(ResourceType type, int id)
    {
        var iconName = string.Empty;
        switch (type)
        {
            case ResourceType.ItemIcon:
                var template = Item_instanceConfig.GetItemInstance(id);
                if (template == null) return null;
                if (template.itemIcon.Count == 0) return null;
                iconName = template.itemIcon[0];
                break;
            case ResourceType.SkillIcon:
                var template1 = Combatskill_templateConfig.GetCombatskill_template(id);
                if (template1 == null) return null;
                iconName = template1.skillIcon;
                break;
            case ResourceType.EquipTypeIcon:
                break;
            case ResourceType.CharHeadIcon:
                var template2 = Char_templateConfig.GetTemplate(id);
                if (template2 == null) return null;
                iconName = template2.HeadIcon;
                break;
            case ResourceType.CharType:
                var template3 = Char_templateConfig.GetTemplate(id);
                if (template3 == null) return null;
                iconName = template3.charType.ToString();
                break;
            case ResourceType.RuneQuelity:
                break;
            case ResourceType.CharStatus:
                break;
        }


        return LoadSprite(type, iconName);
    }

    public static Sprite LoadSprite(ResourceType type,string spriteName)
    {
        Sprite spri = null;
        switch(type)
        {
            case ResourceType.ItemIcon:
                //spri = AssetBundleMgr.Instance.LoadAsset("scene_comon","scene_comon/needloadsprite.ab",iconName,false) as Sprite;
                spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemIcon,spriteName);
                break;
            //case ResourceType.ItemQuility:
            //     spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.ItemQuilty,spriteName);
            //    break;
            case ResourceType.SkillIcon:
                spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.SkillIcon,spriteName);
                break;
            case ResourceType.EquipTypeIcon:
                spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.EquipType,spriteName);
                break;
            case ResourceType.CharHeadIcon:
            case ResourceType.CharType:
            case ResourceType.CharRank:
            case ResourceType.CharAttrIcon:
                spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Char,spriteName);
                break;
            case ResourceType.Other:
                spri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Other,spriteName);
                break;
        }
        if(spri == null)
            LogHelperLSK.LogError("ResourceType: " + type + "  spriteName: " + spriteName);
        return spri;
    }

    public static AudioClip LoadAudioClip(string name)
    {
        return Resources.Load<AudioClip>(m_audioPath + name);
    }
}
