using System.Collections.Generic;
using UnityEngine;


public class SingleSpriteDataController
{

    private SpriteData m_spriteData;

    private Dictionary<string,Sprite> m_iconDic_ = new Dictionary<string,Sprite>();

    public SingleSpriteDataController(SpriteData data)
    {
        m_spriteData = data;
    }

    public void Init()
    {
        for(int i = 0; i < m_spriteData.List.Count; i++)
        {
            Sprite icon = m_spriteData.List[i];
            try
            {
                m_iconDic_.Add(icon.name,icon);
            }
            catch
            {
                LogHelperLSK.LogError("Icon 名称冲突，存在同名" + icon.name);
            }
        }
    }


    public Sprite GetSprite(string name)
    {
        if(m_iconDic_.Count == 0)
        {
            Init();
        }
        Sprite image = null;
        m_iconDic_.TryGetValue(name,out image);
        return image;
    }

    public void Free()
    {
        m_iconDic_.Clear();
    }
}

