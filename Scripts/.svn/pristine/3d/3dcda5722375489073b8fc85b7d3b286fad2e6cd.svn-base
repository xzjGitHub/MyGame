using Boo.Lang;
using Newtonsoft.Json.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRwardPopup : MonoBehaviour
{
    public object Param;
    public Action<object> OnClose;


    public void OpenUI(RwardInfo rwardInfo)
    {
        GetObj();
        //
        LoadItemShow(rwardInfo);
        if (_isHaveRward)
        {
            gameObject.SetActive(true);
            return;
        }

        OnClickMask();
    }


    private void LoadItemShow(RwardInfo rwardInfo)
    {
        _isHaveRward = false;
        ResourceLoadUtil.DeleteChildObj(_itemList);
        LoadItemRes(_goldSprite, rwardInfo.gold);
      //  LoadItemRes(_expSprite, rwardInfo.exp);
        if (rwardInfo.items != null)
        {
            foreach (ItemAttribute item in rwardInfo.items)
            {
                ExploreSystem.Instance.AddItem(item);
                LoadItemRes(item);
            }
        }

    }

    private void LoadItemRes(ItemAttribute itemAttribute)
    {
        int instanceID = itemAttribute.item_instance.instanceID;
        GameObject obj = ResourceLoadUtil.InstantiateRes(_itemObj, _itemList);
        obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon, instanceID);
        obj.transform.Find("Text").GetComponent<Text>().text = itemAttribute.sum.ToString();
        Button button = obj.transform.Find("Button").GetComponent<Button>();

        button.onClick.AddListener(delegate { OnClickItem(itemAttribute); });
        _isHaveRward = true;
    }
    private void LoadItemRes(Sprite iconSprite, int sum)
    {
        if (sum <= 0)
        {
            return;
        }
        GameObject obj = ResourceLoadUtil.InstantiateRes(_itemObj, _itemList);
        obj.transform.Find("Icon").GetComponent<Image>().sprite = iconSprite;
        obj.transform.Find("Text").GetComponent<Text>().text = sum.ToString();
        obj.transform.Find("Button").GetComponent<Button>().enabled = false;
        _isHaveRward = true;
    }
    private void OnClickItem(ItemAttribute item)
    {
        //todo 点击物品没有做
        TipPanelShowUtil.ShowSimpleTip(item.GetItemData());
    }

    private void OnClickMask()
    {
        gameObject.SetActive(false);
        if (OnClose != null)
        {
            OnClose(Param);
        }

        OnClose = null;
        Param = null;
    }

    //
    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }
        //
        _itemList = transform.Find("Bg/List");
        _maskButton = transform.GetComponent<Button>();
        _itemObj = transform.Find("Temp/Item").gameObject;
        _goldSprite = transform.Find("Temp/Gold").GetComponent<Image>().sprite;
        _expSprite = transform.Find("Temp/Exp").GetComponent<Image>().sprite;
        //
        _maskButton.onClick.AddListener(OnClickMask);
        //
        _isFirst = true;
    }

    //
    private bool _isFirst;
    private bool _isHaveRward;
    //
    private Transform _itemList;
    private Sprite _goldSprite;
    private Sprite _expSprite;
    private Button _maskButton;
    private GameObject _itemObj;
}
