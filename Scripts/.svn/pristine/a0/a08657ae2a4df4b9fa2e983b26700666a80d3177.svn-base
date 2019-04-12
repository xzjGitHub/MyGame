using System.Collections.Generic;
using UnityEngine;
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
        isDiscard = false;
        //
        UpdateButtonShow(isDiscard);
        LoadItem();
        itemScroRect.verticalNormalizedPosition = 1;
        //
        introText.text = itemObjs.Keys.Count + "/" + token_Cost.xieKuan;
        //
        gameObject.SetActive(true);
    }

    private void LoadItem()
    {
        contentSize.enabled = !isDiscard;
        gridLayout.enabled = !isDiscard;
        ResourceLoadUtil.DeleteChildObj(itemList);
        itemObjs.Clear();
        foreach (var item in ExploreSystem.Instance.BagItemAttributes)
        {
            itemObjs.Add(item.Key, LoadItemRes(item.Key, item.Value));
        }
    }

    private void OnClickItem(int index)
    {
        if (!isDiscard) return;
        if (!itemObjs.ContainsKey(index)) return;
        //
        ExploreSystem.Instance.DiscardItem(index);
        Destroy(itemObjs[index]);
        itemObjs.Remove(index);
    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击了丢弃
    /// </summary>
    private void OnClickDiscard()
    {
        isDiscard = !isDiscard;
        //
        contentSize.enabled = !isDiscard;
        gridLayout.enabled = !isDiscard;
        //
        UpdateButtonShow(isDiscard);
        UpdateItemShow(isDiscard);
    }

    private void UpdateButtonShow(bool isSelect)
    {
        discardNormal.SetActive(!isSelect);
        discardSelect.SetActive(isSelect);
    }

    private void UpdateItemShow(bool isSelect)
    {
        foreach (var item in itemObjs)
        {
            if (item.Value == null) return;
            item.Value.transform.Find("Select").gameObject.SetActive(isSelect);
        }
    }

    private GameObject LoadItemRes(int index, ItemAttribute itemAttribute)
    {
        GameObject obj = ResourceLoadUtil.InstantiateRes(itemTemplate, itemList);
        //
        var item_Instance = Item_instanceConfig.GetItemInstance(itemAttribute.instanceID);
        obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadItemIcon(itemAttribute);
        obj.transform.Find("Name").GetComponent<Text>().text = item_Instance.itemName;
        if (itemAttribute.sum > 1) obj.transform.Find("Sum").GetComponent<Text>().text = itemAttribute.sum.ToString();
        //
        int _index = index;
        obj.name = itemAttribute.itemID.ToString();
        obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { OnClickItem(_index); });
        return obj;
    }

    private void GetObj()
    {
        if (isFirst) return;
        introText = transform.Find("Intro").GetComponent<Text>();
        close = transform.GetComponent<Button>();
        close.onClick.AddListener(OnClickClose);
        discard = transform.Find("Discard/Btn").GetComponent<Button>();
        discard.onClick.AddListener(OnClickDiscard);
        itemScroRect = transform.Find("Item").GetComponent<ScrollRect>();
        itemList = itemScroRect.transform.Find("Viewport/Content");
        itemTemplate = transform.Find("Temp/Template").gameObject;
        //
        discardSelect = discard.transform.parent.Find("Select").gameObject;
        discardNormal = discard.transform.parent.Find("Normal").gameObject;
        gridLayout = itemList.GetComponent<GridLayoutGroup>();
        contentSize = itemList.GetComponent<ContentSizeFitter>();
        //
        token_Cost = Token_costConfig.GetToken_Cost();
        isFirst = true;
    }

    //
    private bool isFirst;
    private bool isDiscard;
    //
    private Dictionary<int, GameObject> itemObjs = new Dictionary<int, GameObject>();
    //
    private Text introText;
    private Button close;
    private Button discard;
    private Transform itemList;
    private ScrollRect itemScroRect;
    private GameObject itemTemplate;
    private GameObject discardSelect;
    private GameObject discardNormal;
    private GridLayoutGroup gridLayout;
    private ContentSizeFitter contentSize;

    private Token_cost token_Cost;
}
