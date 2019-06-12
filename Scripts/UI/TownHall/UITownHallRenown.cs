﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITownHallRenown : MonoBehaviour
{
    /// <summary>
    /// 领取声望奖励
    /// </summary>
    public Action<RenownBoxReward> OnRenownAward;


    private void Start()
    {
        if (!isFirst)
        {
            GetObj();
            FirstShow();
            isFirst = true;
        }
        UpdateObjShow();
    }

    public void OpenUI(object param = null)
    {
        if (!isFirst)
        {
            GetObj();
            FirstShow();
            isFirst = true;
        }
        UpdateObjShow();
        gameObject.SetActive(true);
    }
    private void OnClickBack()
    {
        gameObject.SetActive(false);
    }

    private void OnClickItem(int type)
    {
        LogHelper_MC.Log("领取阵营" + type + "奖励");
        gameObject.SetActive(false);
        if (OnRenownAward != null)
        {
            OnRenownAward(BountySystem.Instance.ReceiveRenownAward(type));
        }
        //    EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.MainUpdate,
        //       (object) BountySystem.Instance.ReceiveRenownAward(type));
    }

    private void FirstShow()
    {
        for (int i = 1; i <= 8; i++)
        {
            factionName[i].text = BountySystem.Instance.GetFactionStr(i);
        }
    }

    private void UpdateObjShow()
    {
        for (int i = 1; i <= 8; i++)
        {
            int type = i;
            //  factionName[type].text = BountySystem.Instance.GetFactionStr(type);
            factionRenown[type].text = BountySystem.Instance.GetRenownValueStr(type);
            bool isRenownFull = BountySystem.Instance.IsRenownFull(type);
            factionMaxValue[type].SetActive(isRenownFull);
            AddGreyShow(factionRewards[type], type, isRenownFull);
            factionValue[type].gameObject.SetActive(!isRenownFull);
            if (isRenownFull)
            {
                continue;
            }

            factionValue[type].fillAmount = BountySystem.Instance.GetRenownRatio(type);
            factionValueHeight[type].anchoredPosition = GetHeightRatio(factionValue[type].fillAmount);
        }
    }

    /// <summary>
    /// 添加灰色显示
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isShow"></param>
    private void AddGreyShow(Button button, int type, bool isShow = true)
    {
        Image image = button.GetComponent<Image>();
        image.raycastTarget = isShow;
        button.onClick.RemoveAllListeners();
        if (isShow)
        {
            int index = type;
            button.onClick.AddListener(delegate { OnClickItem(index); });
            image.material = null;
            return;
        }
        //
        Material material = new Material(Shader.Find("UISprites/DefaultGray"));
        image.material = material;
    }

    private void GetObj()
    {
        //maskObj = transform.Find("Mask").gameObject;
        npcObj = transform.Find("Npc").gameObject;
        npc1 = npcObj.transform.Find("Npc");
        //
        itemIntroObj = transform.Find("Temp/ItemIntro").gameObject;
        //
        UpdateGetObj(1, transform.Find("Intro"));
    }

    private void UpdateGetObj(int index, Transform transform)
    {
        foreach (Transform item in transform)
        {
            if (index > 8)
            {
                return;
            }
            factionName.Add(index, item.Find("Name").GetComponent<Text>());
            factionRenown.Add(index, item.Find("Text").GetComponent<Text>());
            factionIcon.Add(index, item.Find("Npc/Icon").GetComponent<Image>());
            factionValue.Add(index, item.Find("Value").GetComponent<Image>());
            factionValueHeight.Add(index, factionValue[index].transform.Find("Bg/Height").GetComponent<RectTransform>());
            factionRewards.Add(index, item.Find("Item").GetComponent<Button>());
            factionMaxValue.Add(index, item.Find("Max").gameObject);
            index++;
        }
    }

    private Vector2 GetHeightRatio(float value)
    {
        if (value > 1)
        {
            value = 1;
        }

        if (value < 0)
        {
            value = 0;
        }

        return Vector2.left * 45.4f + Vector2.right * value * ratio;
    }

    //
    private Dictionary<int, Text> factionName = new Dictionary<int, Text>();
    private Dictionary<int, Text> factionRenown = new Dictionary<int, Text>();
    private Dictionary<int, Image> factionIcon = new Dictionary<int, Image>();
    private Dictionary<int, Image> factionValue = new Dictionary<int, Image>();
    private Dictionary<int, GameObject> factionMaxValue = new Dictionary<int, GameObject>();
    private Dictionary<int, Button> factionRewards = new Dictionary<int, Button>();
    private Dictionary<int, RectTransform> factionValueHeight = new Dictionary<int, RectTransform>();
    //
    //    private GameObject maskObj;
    private bool isFirst;
    //
    private GameObject npcObj;
    private Transform npc1;
    //
    private readonly ScrollRect intro1;
    private readonly Transform introList1;
    //
    private GameObject itemIntroObj;
    //
    private const float ratio = 198f;
}
