using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemUtil
{
    /// <summary>
    /// 通用的格子
    /// </summary>
    private static GameObject m_equipPrefab;
    public static GameObject EquipPrefab
    {
        get
        {
            if(m_equipPrefab == null)
            {
                m_equipPrefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.Equip,false);
            }
            return m_equipPrefab;
        }
    }

    /// <summary>
    /// 通用的item
    /// </summary>
    private static GameObject m_item;
    public static GameObject Item
    {
        get
        {
            if(m_item == null)
            {
                m_item = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.Item,false);
            }
            return m_item;
        }
    }

    /// <summary>
    /// 设置信息
    /// </summary>
    /// <param name="info"></param>
    /// <param name="item"></param>
    public static void SetItemInfo(ItemAttribute info,GameObject item)
    {
      //  int qui = info.GetItemData().itemQuality;     
        // quility.SetNativeSize();

        //Image quilityFrame = item.transform.Find("Frame").GetComponent<Image>();
        //quilityFrame.sprite = ResourceLoadUtil.LoadItemQuiltyFrameSprite(qui);
        //   quilityFrame.SetNativeSize();

        Image icon = item.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = ResourceLoadUtil.LoadItemIcon(info);
        icon.SetNativeSize();

        Text num = item.transform.Find("Num").GetComponent<Text>();

        EquipAttribute equipAttribute = info as EquipAttribute;
        if(equipAttribute != null)
        {
            // Equip_instance equip = Equip_instanceConfig.GetEquip_instance(info.instanceID);
            Item_instance item_Instance = Item_instanceConfig.GetItemInstance(info.instanceID);
            num.text = "Lv." + item_Instance.charLevelReq;
            num.gameObject.SetActive(true);
        }
        else
        {
            num.text = info.sum.ToString();
            num.gameObject.SetActive(info.sum > 1);
        }
    }


    //白色/红色
    private static string m_format1 = "<color=#FAFAFAFF>{0}</color>/<color=#FF0000FF>{1}</color>";
    //白色/白色
    private static string m_format2 = "<color=#FAFAFAFF>{0}</color>/<color=#FAFAFAFF>{1}</color>";

    public static void SetTextInfo(int needNum,int haveNum,Text text)
    {
        if(haveNum >= 100)
        {
            text.text = needNum + "/" + "*";
        }
        else
        {
            if (needNum > haveNum)
            {
                text.text = string.Format(m_format1,needNum,haveNum);
            }
            else
            {
                text.text = string.Format(m_format2,needNum,haveNum);
            }
        }
    }
}

