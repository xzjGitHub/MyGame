
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/9/2019
//Note:     
//--------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class DiaNpc: MonoBehaviour
{
    private GameObject m_tag;
    private Text m_info;

    private bool m_hasSetIcon;

    private MutiTextAnim m_mutilAnim;

    public void InitComponent()
    {
        m_tag = transform.Find("Tag").gameObject;
        m_tag.SetActive(false);
        m_info = transform.Find("Info").GetComponent<Text>();
        m_mutilAnim = m_info.GetComponent<MutiTextAnim>();
    }

    public void InitComponent(List<string> list,string npcIconName = "")
    {
        m_tag = transform.Find("Tag").gameObject;
        m_tag.SetActive(false);
        m_info = transform.Find("Info").GetComponent<Text>();
        m_mutilAnim = m_info.GetComponent<MutiTextAnim>();
        m_mutilAnim.InitAllText(list,null,null);

        Image icon = transform.Find("Icon").GetComponent<Image>();
        icon.gameObject.SetActive(!string.IsNullOrEmpty(npcIconName));
        if(!string.IsNullOrEmpty(npcIconName))
        {
            icon.sprite = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Char,npcIconName);
            icon.SetNativeSize();
        }
        m_hasSetIcon = true;
    }

    public void UpdateInfo()
    {
        m_mutilAnim.ShowNextByHand();
    }

    public void UpdateInfo(string npcIconName,string info)
    {
        if(!m_hasSetIcon)
        {
            Image icon = transform.Find("Icon").GetComponent<Image>();
            icon.gameObject.SetActive(!string.IsNullOrEmpty(info));
            if(!string.IsNullOrEmpty(info))
            {
                icon.sprite = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Char,npcIconName);
                icon.SetNativeSize();
            }
            m_hasSetIcon = true;
        }
        m_info.text = info;
    }
}
