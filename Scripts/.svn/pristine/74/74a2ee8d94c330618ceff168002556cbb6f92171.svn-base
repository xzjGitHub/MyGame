using UnityEngine;
using System.Collections.Generic;

public class PlayerGameObjectInfo
{
    public int ID;
    public GameObject Obj;
}

public class PlayerPool: Singleton<PlayerPool>
{

    private PlayerPool() { }

    private List<PlayerGameObjectInfo> m_usingPlayer = new List<PlayerGameObjectInfo>();
    private List<PlayerGameObjectInfo> m_freePlayer = new List<PlayerGameObjectInfo>();

    public GameObject GetPlayer(CharAttribute attr)
    {
        GameObject temp = null;
        for(int i = 0; i < m_freePlayer.Count; i++)
        {
            if(m_freePlayer[i].ID == attr.charID)
            {
                temp = m_freePlayer[i].Obj;
                m_usingPlayer.Add(m_freePlayer[i]);
                m_freePlayer.RemoveAt(i);
                break;
            }
        }
        if(temp == null)
        {
            PlayerGameObjectInfo playerGameObjectInfo = new PlayerGameObjectInfo();
            playerGameObjectInfo.ID = attr.charID;
            CharRPack info = CharRPackConfig.GeCharShowTemplate(attr.char_template.templateID);
            playerGameObjectInfo.Obj = ResourceLoadUtil.LoadRole(info.charRP);
            m_usingPlayer.Add(playerGameObjectInfo);
            temp = playerGameObjectInfo.Obj;
        }
        if(temp != null)
            temp.SetActive(true);
        else
            Debug.LogError("加载角色模型资源出错，角色Id: " + attr.char_template.templateID);
        return temp;
    }

    public void LoadPlayer(int id)
    {
        PlayerGameObjectInfo playerGameObjectInfo = new PlayerGameObjectInfo();
        int templateID = CharSystem.Instance.GetCharAttribute(id).templateID;
        CharRPack info = CharRPackConfig.GeCharShowTemplate(templateID);
        playerGameObjectInfo.ID = id;
        playerGameObjectInfo.Obj = ResourceLoadUtil.LoadRole(info.charRP);
        playerGameObjectInfo.Obj.SetActive(false);
        m_freePlayer.Add(playerGameObjectInfo);
        GameObjectPool.Instance.AddChild(playerGameObjectInfo.Obj);
    }

    public void Free(int id)
    {
        for(int i = 0; i < m_usingPlayer.Count; i++)
        {
            if(m_usingPlayer[i].ID == id)
            {
                if(m_usingPlayer[i].Obj != null)
                {
                    m_usingPlayer[i].Obj.SetActive(false);
                    GameObjectPool.Instance.AddChild(m_usingPlayer[i].Obj);
                }
                m_freePlayer.Add(m_usingPlayer[i]);
                m_usingPlayer.RemoveAt(i);
                break;
            }
        }
    }

    public void Clear()
    {
        for(int i = 0; i < m_usingPlayer.Count; i++)
        {
            GameObject.DestroyImmediate(m_usingPlayer[i].Obj);
        }

        for(int i = 0; i < m_freePlayer.Count; i++)
        {
            GameObject.DestroyImmediate(m_freePlayer[i].Obj);
        }

        m_usingPlayer.Clear();
        m_freePlayer.Clear();
    }
}
