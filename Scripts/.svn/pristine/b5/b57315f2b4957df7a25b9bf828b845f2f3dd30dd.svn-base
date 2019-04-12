using UnityEngine;
using UnityEngine.UI;

public class ItemCost: MonoBehaviour
{
    private Image m_icon;
    private Text m_num;

    public void InitComponent()
    {
        m_icon = transform.Find("Icon").GetComponent<Image>();
        m_num = transform.Find("Num").GetComponent<Text>();
    }

    public void Init(int id,int needNum)
    {
        ItemAttribute attr = ItemSystem.Instance.GetItemAttribute(id);
        if(attr != null)
        {
            Init(attr.GetItemData(),needNum);
        }
    }

    public void InitByTemplateId(int templateId,int needNum)
    {
        ItemAttribute attr = ItemSystem.Instance.GetItemAttributeByTemplateId(templateId);
        if (attr != null)
        {
            Init(attr.GetItemData(), needNum);
        }
        else
        {
            Item_instance item_Instance = Item_instanceConfig.GetItemInstance(templateId);
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                item_Instance.itemIcon.Count>0? item_Instance.itemIcon[0]:"");
            SetTextInfo(needNum,0);
        }
    }

    public void Init(ItemData itemData,int needNum)
    {
        Item_instance item_Instance = Item_instanceConfig.GetItemInstance(itemData.instanceID);
      //  m_quility.sprite = ResourceLoadUtil.LoadItemQuiltySprite(itemData.itemQuality);
        m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
            item_Instance.itemIcon.Count > 0 ? item_Instance.itemIcon[0] : "");
        SetTextInfo(needNum,itemData.sum);
    }


    private void SetTextInfo(int needNum,int haveNum)
    {
        if (haveNum >= 100)
        {
            m_num.text = needNum + "/" + "*";
        }
        else
        {
            m_num.text = needNum + "/" + haveNum;
        }
    }
}
