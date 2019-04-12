using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UITownHallRenownReward : MonoBehaviour
{
    public Action OnBack;

    public void OpenUI(RenownBoxReward renownBoxReward)
    {
        if (!isFirst)
        {
            GetObj();
            isFirst = true;
        }
        //
        UpdateItemShow(renownBoxReward.itemAttributes);
        UpdateCharShow(renownBoxReward.charAttribute);
        //
        gameObject.SetActive(true);
    }

    private void OnClickBack()
    {
        gameObject.SetActive(false);
        if (OnBack != null) OnBack();
    }

    /// <summary>
    /// 更新物品显示
    /// </summary>
    /// <param name="itemAttribute"></param>
    private void UpdateItemShow(List<ItemAttribute> itemAttribute)
    {
        ResourceLoadUtil.DeleteChildObj(itemList);
        itemScrollRect.verticalNormalizedPosition = 1;
        if (itemAttribute == null) return;
        int index = 0;
        foreach (var item in itemAttribute)
        {
            if (index > 10) break;
            BountySystem.Instance.GetItem(item, itemList, itemIntroObj);
            index++;
        }
    }

    /// <summary>
    /// 更新角色显示
    /// </summary>
    /// <param name="charAttribute"></param>
    private void UpdateCharShow(CharAttribute charAttribute)
    {
        charObj.SetActive(false);
        if (charAttribute == null) return;
        charIncon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon, charAttribute.templateID);
        charKuangBG.sprite = ResourceLoadUtil.LoadItemQuiltyFrameSprite((int)charAttribute.charQuality);
        charQuality.sprite = ResourceLoadUtil.LoadItemQuiltySprite((int)charAttribute.charQuality);
        charName.text = charAttribute.char_template.charName;
        charObj.SetActive(true);
    }

    private void GetObj()
    {
        charObj = transform.Find("Char").gameObject;
        var charInfo = charObj.transform.Find("Info");
        charName = charInfo.Find("Text").GetComponent<Text>();
        charIncon = charInfo.Find("Item/KuangBG").GetComponent<Image>();
        charKuangBG = charInfo.Find("Item/Icon").GetComponent<Image>();
        charQuality = charInfo.Find("Item").GetComponent<Image>();
        itemIntroObj = transform.Find("Item/Temp/ItemIntro").gameObject;
        itemList = transform.Find("Item/List/Viewport/Content");
        itemScrollRect = transform.Find("Item/List").GetComponent<ScrollRect>();
        //
       // backButton = transform.Find("Back").GetComponent<Button>();
        //
      //  backButton.onClick.AddListener(OnClickBack);
    }
    //
    private bool isFirst;
    //
    private GameObject charObj;
    private Text charName;
    private Image charIncon;
    private Image charKuangBG;
    private Image charQuality;
    private GameObject itemIntroObj;
    private Transform itemList;
    private ScrollRect itemScrollRect;
  //  private Button backButton;
}
