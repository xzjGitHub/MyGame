using GameEventDispose;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITownHallMainBounty : MonoBehaviour
{

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
    /// 更新Npc
    /// </summary>
    private void UpdateShow()
    {
        //
        // if (!maskObj.activeInHierarchy) maskObj.SetActive(true);
        // introPopupObj.SetActive(false);
        //
        if (IEUpdateBountyIntro != null)
        {
            IEUpdateBountyIntro.Stop();
        }

        IEUpdateBountyIntro = new CoroutineUtil(UpdateBountyIntro());
    }

    /// <summary>
    /// 重置UI
    /// </summary>
    private void ResetUIShow()
    {
        bountyID = -1;
        introPopupObj.SetActive(false);
        // if (!maskObj.activeInHierarchy) maskObj.SetActive(true);
        UpdateShow();
    }

    /// <summary>
    /// 点击了选择
    /// </summary>
    private void OnClickSelect()
    {
        //  maskObj.SetActive(false);
        introPopupObj.SetActive(false);
        BountySystem.Instance.SelectMainBounty(bountyID);
        selectButton.gameObject.SetActive(false);
        //  gameObject.SetActive(false);
    }



    /// <summary>
    /// 更新悬赏简介
    /// </summary>
    /// <param name="index"></param>
    private IEnumerator UpdateBountyIntro()
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
        //更新显示
        BountyAttribute bountyAttribute = BountySystem.Instance.MainBountyAttributes.Find(a => a.BountyState == BountyState.Acceptable || a.BountyState == BountyState.Accepted);
        if (bountyAttribute == null)
        {
            bountyAttribute = new BountyAttribute(1001);
            LogHelperLSK.Log("没有主线新建1001测试");
        }
        selectButton.gameObject.SetActive(bountyAttribute.BountyState == BountyState.Acceptable);
        completeObj.SetActive(bountyAttribute.BountyState == BountyState.Accepted);
        bountyID = bountyAttribute.BountyId;
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
                break;
            case BountyEventType.RandomUpdate:
                break;
            case BountyEventType.FinishBounty:
                break;
            case BountyEventType.MainUpdate:
                ResetUIShow();
                break;
        }
    }


    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        //   maskObj = transform.Find("Mask").gameObject;
        //
        introPopupObj = transform.Find("Popup").gameObject;
        popupCanvas = introPopupObj.GetComponent<CanvasGroup>();
        Transform right = introPopupObj.transform.Find("Right");
        intro1 = right.Find("Intro1/List").GetComponent<ScrollRect>();
        intro2 = right.Find("Intro2/List").GetComponent<ScrollRect>();
        //
        introList1 = right.Find("Intro1/List/Viewport/Content");
        introList2 = right.Find("Intro2/List/Viewport/Content");
        introSize1 = introList1.GetComponent<ContentSizeFitter>();
        introSize2 = introList2.GetComponent<ContentSizeFitter>();
        selectButton = introPopupObj.transform.Find("Select/Button").GetComponent<Button>();
        completeObj = introPopupObj.transform.Find("Complete").gameObject;
        //
        Transform left = introPopupObj.transform.Find("Left");
        bountyName = left.Find("Name").GetComponent<Text>();
        bountyIntro = left.Find("Intro").GetComponent<Text>();
        bountyMap = introList1.Find("MapIntro/MapName/Text").GetComponent<Text>();
        numIntroObj = right.Find("Intro1/Temp/NumIntro").gameObject;
        //
        goldText = introList2.Find("Gold/Text").GetComponent<Text>();
        renowObj = right.Find("Intro2/Temp/Renow").gameObject;
        lineObj = right.Find("Intro2/Temp/Line").gameObject;
        itemIntroObj = right.Find("Intro2/Temp/ItemIntro").gameObject;
        //
        selectButton.onClick.AddListener(OnClickSelect);
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
    private readonly GameObject maskObj;
    private bool isFirst;
    private int bountyID = -1;
    private const string mapIntroStr = "前往";
    private List<GameObject> introLoadObjs = new List<GameObject>();
    //
    private GameObject introPopupObj;
    private CanvasGroup popupCanvas;
    private ScrollRect intro1;
    private ScrollRect intro2;
    private Transform introList1;
    private Transform introList2;
    private ContentSizeFitter introSize1;
    private ContentSizeFitter introSize2;
    private Button selectButton;
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
    private GameObject completeObj;
}
