using GameEventDispose;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 大厅悬赏信息
/// </summary>
public class UITownHallRandomBounty : MonoBehaviour
{

    public Action OnClickCall;

    /// <summary>
    /// 关闭界面
    /// </summary>
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        if (!isFirst)
        {
            GetObj();
            //
            EventDispatcher.Instance.BountyEvent.AddEventListener<BountyEventType, object>(EventId.BountyEvent, OnBountyEvent);
            isFirst = true;
        }
        ResetUIShow();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭弹窗显示
    /// </summary>
    public void ClosePopoupShow()
    {
        introPopupObj.SetActive(false);
        maskObj.SetActive(false);
        selectIndex = -1;
    }


    private void Start()
    {
        return;
        if (!isFirst)
        {
            GetObj();
            //
            EventDispatcher.Instance.BountyEvent.AddEventListener<BountyEventType, object>(EventId.BountyEvent, OnBountyEvent);
            isFirst = true;
        }
        ResetUIShow();
    }
    /// <summary>
    /// 更新Npc
    /// </summary>
    private void UpdateNpc()
    {
        npcButton1.enabled = false;
        npcButton2.enabled = false;
        if (BountySystem.Instance.UsableRandomBounty.Count <= 0)
        {
            return;
        }

        npcTop.SetActive(!BountySystem.Instance.IsAcceptedRandomBounty);
        switch (BountySystem.Instance.SelectRandomBountyIndex)
        {
            case 0:
                ResourceLoadUtil.LoadNpcModel("1001", npc1);
                npcButton1.enabled = true;
                break;
            case 1:
                ResourceLoadUtil.LoadNpcModel("1001", npc2);
                npcButton2.enabled = true;
                break;
            default:
                ResourceLoadUtil.LoadNpcModel("1001", npc1);
                ResourceLoadUtil.LoadNpcModel("1001", npc2);
                npcButton1.enabled = true;
                npcButton2.enabled = true;
                break;
        }
        npcObj.SetActive(true);
    }

    /// <summary>
    /// 重置UI
    /// </summary>
    private void ResetUIShow()
    {
        selectIndex = -1;
        introPopupObj.SetActive(false);
        npcObj.SetActive(false);
        maskObj.SetActive(false);
        ResourceLoadUtil.DeleteChildObj(npc1);
        ResourceLoadUtil.DeleteChildObj(npc2);
        if (maskObj.activeInHierarchy)
        {
            maskObj.SetActive(false);
        }

        UpdateNpc();
    }

    /// <summary>
    /// 点击了NPC
    /// </summary>
    /// <param name="index"></param>
    private void OnClickNpc(int index)
    {
        if (OnClickCall != null)
        {
            OnClickCall();
        }
        if (selectIndex == index)
        {
            return;
        }

        selectIndex = index;
        //
        // introPopupObj.SetActive(false);
        //
        if (IEUpdateBountyIntro != null)
        {
            IEUpdateBountyIntro.Stop();
        }

        IEUpdateBountyIntro = new CoroutineUtil(UpdateBountyIntro(index));
    }
    /// <summary>
    /// 点击了选择
    /// </summary>
    private void OnClickSelect()
    {
        if (OnClickCall != null)
        {
            OnClickCall();
        }
        maskObj.SetActive(false);
        introPopupObj.SetActive(false);
        BountySystem.Instance.SelectRandomBounty(selectIndex);
    }

    private void OnClickBack()
    {
        maskObj.SetActive(false);
        selectIndex = -1;
        introPopupObj.SetActive(false);
    }
    /// <summary>
    /// 更新悬赏简介
    /// </summary>
    /// <param name="index"></param>
    private IEnumerator UpdateBountyIntro(int index)
    {
        foreach (GameObject item in introLoadObjs)
        {
            ResourceLoadUtil.DeleteObj(item);
        }
        introSize1.enabled = false;
        introSize2.enabled = false;
        popupCanvas.alpha = 0;
        introPopupObj.SetActive(true);
        intro1.verticalNormalizedPosition = 1;
        intro2.verticalNormalizedPosition = 1;
        //更新位置
        Vector2 pos = taskIntroRT.anchoredPosition;
        pos.x = (index == 0 ? -1 : 1) * Mathf.Abs(pos.x);
        taskIntroRT.anchoredPosition = pos;
        //更新显示
        BountyAttribute bountyAttribute = BountySystem.Instance.UsableRandomBounty[index];
        //更新对话
        acceptedObj.SetActive(bountyAttribute.BountyState == BountyState.Accepted);
        selectButton.gameObject.SetActive(bountyAttribute.BountyState == BountyState.Acceptable && !BountySystem.Instance.IsAcceptedRandomBounty);
        dialog1.gameObject.SetActive(index == 0);
        dialog2.gameObject.SetActive(index == 1);
        //dialogText1.text = string.Empty;
        //dialogText2.text = string.Empty;
        if (!BountySystem.Instance.IsAcceptedRandomBounty)
        {
            (index != 0 ? dialogText1 : dialogText2).text = "一段嘲讽的话！！！！！！！！！";
        }
        //更新目标
        foreach (List<int> item in bountyAttribute.BountyTemplate.bountyTarget)
        {
            GameObject obj = ResourceLoadUtil.InstantiateRes(numIntroObj, introList1);
            introLoadObjs.Add(obj);
            //
            List<string> list = BountySystem.Instance.GetTargetStr(item);
            //
            obj.transform.Find("Text").GetComponent<Text>().text = list[0] + list[1];
            obj.transform.Find("Text/Bg/Text").GetComponent<Text>().text = "0/" + list[2];
        }
        //
        bountyName.text = bountyAttribute.BountyTemplate.bountyName;
        bountyIntro.text = bountyAttribute.BountyTemplate.bountyDescription;
        Map_template template = Map_templateConfig.GetMap_templat(bountyAttribute.BountyTemplate.bountyLocation);
        bountyMap.text = mapIntroStr + (template == null ? string.Empty : template.mapName);
        //金币
        goldText.text = "+" + bountyAttribute.BountyReward.gold;
        //声望
        foreach (KeyValuePair<int, int> item in bountyAttribute.BountyReward.renowRewards)
        {
            GameObject obj = ResourceLoadUtil.InstantiateRes(renowObj, introList2);
            introLoadObjs.Add(obj);
            obj.transform.Find("Text").GetComponent<Text>().text = BountySystem.Instance.GetRenowRewardStrs(item.Key, item.Value);
        }
        //间隔线
        GameObject obj1 = ResourceLoadUtil.InstantiateRes(lineObj, introList2);
        introLoadObjs.Add(obj1);
        //物品
        foreach (ItemData item in bountyAttribute.BountyReward.itemRewards)
        {
            introLoadObjs.Add(BountySystem.Instance.GetItem(item, introList2, itemIntroObj));
        }
        yield return null;
        introSize1.enabled = true;
        introSize2.enabled = true;
        yield return null;
        popupCanvas.alpha = 1;
        if (!maskObj.activeInHierarchy)
        {
            maskObj.SetActive(true);
        }
    }

    private void OnBountyEvent(BountyEventType arg1, object arg2)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        switch (arg1)
        {
            case BountyEventType.TargetUpdate:
                break;
            case BountyEventType.AcceptBounty:
                ResetUIShow();
                break;
            case BountyEventType.RandomUpdate:
                ResetUIShow();
                break;
            case BountyEventType.FinishBounty:
                break;
            case BountyEventType.MainUpdate:
                break;
        }
    }


    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        npcObj = transform.Find("Npc").gameObject;
        maskObj = npcObj.transform.Find("Mask").gameObject;
        npcTop = npcObj.transform.Find("Top").gameObject;
        npc1 = npcObj.transform.Find("Npc1/Npc");
        npc2 = npcObj.transform.Find("Npc2/Npc");
        npcButton1 = npcObj.transform.Find("Npc1/Button").GetComponent<Button>();
        npcButton2 = npcObj.transform.Find("Npc2/Button").GetComponent<Button>();
        //
        introPopupObj = transform.Find("Popup").gameObject;
        popupCanvas = introPopupObj.GetComponent<CanvasGroup>();
        dialog1 = introPopupObj.transform.Find("Dialog1").gameObject;
        dialog2 = introPopupObj.transform.Find("Dialog2").gameObject;
        dialogText1 = dialog1.transform.Find("Text").GetComponent<Text>();
        dialogText2 = dialog2.transform.Find("Text").GetComponent<Text>();
        Transform taskIntro = introPopupObj.transform.Find("TaskIntro");
        taskIntroRT = taskIntro.GetComponent<RectTransform>();
        intro1 = taskIntro.Find("Intro1/List").GetComponent<ScrollRect>();
        intro2 = taskIntro.Find("Intro2/List").GetComponent<ScrollRect>();
        introList1 = taskIntro.Find("Intro1/List/Viewport/Content");
        introList2 = taskIntro.Find("Intro2/List/Viewport/Content");
        introSize1 = introList1.GetComponent<ContentSizeFitter>();
        introSize2 = introList2.GetComponent<ContentSizeFitter>();
        selectButton = taskIntro.Find("Select/Image").GetComponent<Button>();
        backButton = introPopupObj.transform.Find("Back/Image").GetComponent<Button>();
        acceptedObj = taskIntro.Find("Accepted").gameObject;
        //
        bountyName = introList1.Find("Name/Text").GetComponent<Text>();
        bountyIntro = introList1.Find("Intro/Text").GetComponent<Text>();
        bountyMap = introList1.Find("MapIntro/Text").GetComponent<Text>();
        numIntroObj = taskIntro.Find("Intro1/Temp/NumIntro").gameObject;
        //
        goldText = introList2.Find("Gold/Text").GetComponent<Text>();
        renowObj = taskIntro.Find("Intro2/Temp/Renow").gameObject;
        lineObj = taskIntro.Find("Intro2/Temp/Line").gameObject;
        itemIntroObj = taskIntro.Find("Intro2/Temp/ItemIntro").gameObject;
        //
        npcButton1.onClick.AddListener(delegate { OnClickNpc(0); });
        npcButton2.onClick.AddListener(delegate { OnClickNpc(1); });
        selectButton.onClick.AddListener(OnClickSelect);
        backButton.onClick.AddListener(OnClickBack);
    }
    /// <summary>
    /// 销毁
    /// </summary>
    private void OnDestroy()
    {
        EventDispatcher.Instance.BountyEvent.RemoveEventListener<BountyEventType, object>(EventId.BountyEvent, OnBountyEvent);
    }

    //
    private CoroutineUtil IEUpdateBountyIntro;
    private RectTransform taskIntroRT;
    private GameObject maskObj;
    private bool isFirst;
    private int selectIndex = -1;
    private const string mapIntroStr = "前往";
    private List<GameObject> introLoadObjs = new List<GameObject>();
    //
    private GameObject npcObj;
    private Transform npc1;
    private Transform npc2;
    private GameObject npcTop;
    private Button npcButton1;
    private Button npcButton2;
    private GameObject dialog1;
    private GameObject dialog2;
    //
    private GameObject introPopupObj;
    private CanvasGroup popupCanvas;
    private Text dialogText1;
    private Text dialogText2;
    private ScrollRect intro1;
    private ScrollRect intro2;
    private Transform introList1;
    private Transform introList2;
    private ContentSizeFitter introSize1;
    private ContentSizeFitter introSize2;
    private Button selectButton;
    private GameObject acceptedObj;
    //
    private Text bountyName;
    private Text bountyIntro;
    private Text bountyMap;
    private GameObject numIntroObj;
    //
    private Text goldText;
    private GameObject renowObj;
    private GameObject lineObj;
    private GameObject itemIntroObj;
    private Button backButton;
}
