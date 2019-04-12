using System.Collections.Generic;
using UnityEngine;


public class SpriteManager: Singleton<SpriteManager>
{
    private SpriteManager() { }

    private Dictionary<string,SingleSpriteDataController> m_dict = new Dictionary<string,SingleSpriteDataController>();

    public Sprite GetSprite(string spriteTypeName,string spriteName)
    {
        if(spriteName == null)
            spriteName = "";
        if(!m_dict.ContainsKey(spriteTypeName))
        {
            SpriteData data = Resources.Load<SpriteData>("ScriptableData/SpriteData/" + spriteTypeName);

            SingleSpriteDataController singleSpriteDataInfo = new SingleSpriteDataController(data);
            singleSpriteDataInfo.Init();
            m_dict[spriteTypeName] = singleSpriteDataInfo;
        }
        Sprite sp = m_dict[spriteTypeName].GetSprite(spriteName);
        if(sp == null)
        {
            if(string.IsNullOrEmpty(spriteName))
                LogHelperLSK.LogError("加载图片出错；图片类型: " + spriteTypeName + " 图片名称为空，请检查配置表 ");
            else
                LogHelperLSK.LogError("加载图片出错；图片类型: " + spriteTypeName + " 图片名称： " + spriteName);
        }
        return sp;
    }

    public void Free(string spriteTypeName)
    {
        SingleSpriteDataController singleSpriteDataController = m_dict[spriteTypeName];
        singleSpriteDataController.Free();
        m_dict.Remove(spriteTypeName);
    }

    public void FreeAll()
    {
        List<SingleSpriteDataController> list = new List<SingleSpriteDataController>();
        list.AddRange(m_dict.Values);

        for(int i = 0; i < list.Count; i++)
        {
            list[i].Free();
        }

        m_dict.Clear();
    }
}

