﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 探索背包
/// </summary>
public class UIExploreBagPopup : MonoBehaviour
{

    public void OpenUI()
    {
        GetObj();
        //
        LoadItem();
        _itemScroRect.verticalNormalizedPosition = 1;
        //
        gameObject.SetActive(true);
    }

    private void LoadItem()
    {
        ResourceLoadUtil.DeleteChildObj(_itemList);
        foreach (ItemAttribute item in ExploreSystem.Instance.BagItemAttributes)
        {
            LoadItemRes(item);
        }
    }

    private void OnClickItem(ItemAttribute item)
    {
        TipPanelShowUtil.ShowSimpleTip(item.GetItemData());
    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
    }



    private GameObject LoadItemRes(ItemAttribute itemAttribute)
    {
        GameObject obj = ResourceLoadUtil.InstantiateRes(_itemTemplate, _itemList);
        //
        Item_instance item_Instance = Item_instanceConfig.GetItemInstance(itemAttribute.instanceID);

        obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon, itemAttribute.instanceID);
        obj.transform.Find("Text").GetComponent<Text>().text = itemAttribute.sum > 1 ? itemAttribute.sum.ToString() : "";
        Button button = obj.transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickItem(itemAttribute); });
        //
        obj.name = itemAttribute.itemID.ToString();
        return obj;
    }

    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }

        _closeButton = transform.Find("Close").GetComponent<Button>();
        _closeButton.onClick.AddListener(OnClickClose);
        _itemScroRect = transform.Find("Item").GetComponent<ScrollRect>();
        _itemList = _itemScroRect.transform.Find("Viewport/Content");
        _itemTemplate = transform.Find("Temp/Template").gameObject;
        //
        _gridLayout = _itemList.GetComponent<GridLayoutGroup>();
        _contentSize = _itemList.GetComponent<ContentSizeFitter>();
        //
        _isFirst = true;
    }

    //
    private bool _isFirst;
    //
    private Button _closeButton;
    private Transform _itemList;
    private ScrollRect _itemScroRect;
    private GameObject _itemTemplate;
    private GridLayoutGroup _gridLayout;
    private ContentSizeFitter _contentSize;
}
