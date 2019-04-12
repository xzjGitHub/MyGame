using UnityEngine;
using UnityEngine.UI;

public class ItemTipInfo: MonoBehaviour
{
    private Text m_name;
    private Text m_des;
    private Image m_icon;

    private bool m_hasInit;
    private void InitComponent()
    {
        m_name = transform.Find("Title/Name").GetComponent<Text>();
        m_des = transform.Find("Des").GetComponent<Text>();
        m_icon = transform.Find("Title/Icon").GetComponent<Image>();
    }

    public void UpdateInfo(ItemData itemData)
    {
        if (!m_hasInit)
        {
            InitComponent();
            m_hasInit = true;
        }

        Item_instance item = Item_instanceConfig.GetItemInstance(itemData.instanceID);

        m_name.text = item.itemName;
        m_des.text = item.itemDescription;
        m_icon.sprite = ResourceLoadUtil.LoadSprite( ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
    }

    public void UpdateInfo(int instanceId)
    {
        if(!m_hasInit)
        {
            InitComponent();
            m_hasInit = true;
        }

        Item_instance item = Item_instanceConfig.GetItemInstance(instanceId);

        m_name.text = item.itemName;
        m_des.text = item.itemDescription;
        m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
    }

}

