using UnityEngine;
using UnityEngine.UI;

public class UIExploreItem : MonoBehaviour
{


    public void Init(ItemData data)
    {
        sceneX = GetPos(0).x;
        //
        itemData = data;
        //
        var _obj = gameObject;
        _obj.name = data.itemID.ToString();
        _obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnClickItem);
        //
        _obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
            data.instanceID);
        //
        if (data.itemQuality == 2 || data.itemQuality == 3 || data.itemQuality == 4)
        {
            _obj.transform.Find("Effect").Find(2.ToString()).gameObject.SetActive(true);
            _obj.transform.Find("Effect").Find(data.itemQuality.ToString()).gameObject.SetActive(true);
        }
        if (GameTools.IsEquip(data.itemType))
        {
            if (data.resultItemLevel > TeamSystem.Instance.GetTeamEquipMaxLevel()) _obj.transform.Find("Icon/Up").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 点击了物品
    /// </summary>
    private void OnClickItem()
    {
        ExploreSystem.Instance.AddItem(itemData);
        DestroyImmediate(gameObject);
    }



    void Update()
    {
        if (transform.position.x >= sceneX) return;
        DestroyImmediate(gameObject);
    }

    private Vector3 GetPos(float x)
    {
        return GameTools.ScreenToWorldPoint(new Vector3(x, 0, 0), transform);
    }

    //
    private ItemData itemData;
    private float sceneX;
}
