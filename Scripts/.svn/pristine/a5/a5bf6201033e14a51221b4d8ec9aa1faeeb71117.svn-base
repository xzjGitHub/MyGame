using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBountyInfo : MonoBehaviour
{

    /// <summary>
    /// 打开界面
    /// </summary>
    public void OpenUI(BountyAttribute bountyAttribute, BuountyUIState state = BuountyUIState.Confirm)
    {
        GetObj();
        _canvasGroup.alpha = 0;
        if (_IUpdateBountyIntro != null)
        {
            _IUpdateBountyIntro.Stop();
        }
        if (bountyAttribute==null)
        {
            return;
        }
        _bountyId = bountyAttribute.BountyId;
        gameObject.SetActive(true);
        _IUpdateBountyIntro = new CoroutineUtil(IEUpdateBountyIntro(bountyAttribute, state));
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    public void OpenUI(int bountyId, BuountyUIState state = BuountyUIState.Confirm)
    {
        GetObj();
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _bountyId = bountyId;
        if (_IUpdateBountyIntro != null)
        {
            _IUpdateBountyIntro.Stop();
        }
        BountyAttribute bountyAttribute = BountySystem.Instance.GetBountyAttribute(_bountyId);
        _IUpdateBountyIntro = new CoroutineUtil(IEUpdateBountyIntro(bountyAttribute, state));
    }

    /// <summary>
    /// 点击了选择
    /// </summary>
    private void OnClickSelect()
    {
        BountySystem.Instance.SelectMainBounty(_bountyId);
        CloseUI();
    }

    /// <summary>
    /// 点击了确定
    /// </summary>
    private void OnClickConfirm()
    {
        BountySystem.Instance.SelectMainBounty(_bountyId);
        CloseUI();
    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新悬赏简介
    /// </summary>
    private IEnumerator IEUpdateBountyIntro(BountyAttribute bountyAttribute,BuountyUIState state)
    {
        Bounty_template bountyTemplate = Bounty_templateConfig.GetBounty_template(_bountyId);
        if (bountyTemplate == null)
        {
            CloseUI();
            yield break;
        }
        _bountyName.text = bountyTemplate.bountyName;
        _bountyIntro.text = bountyTemplate.bountyDescription;
        foreach (GameObject item in _introLoadObjs)
        {
            ResourceLoadUtil.DeleteObj(item);
        }
        _introSize1.enabled = false;
        _introSize2.enabled = false;
        _canvasGroup.alpha = 0;
        _introScrollRect1.verticalNormalizedPosition = 1;
        _introScrollRect2.verticalNormalizedPosition = 1;
        //更新按钮显示
        _acceptedObj.SetActive(state == BuountyUIState.Accepted);
        _confirmButton.gameObject.SetActive(state == BuountyUIState.Confirm);
        _selectButton.gameObject.SetActive(state == BuountyUIState.Select);
        _selectObj.gameObject.SetActive(state != BuountyUIState.Hidden);
        //
        //更新任务信息显示
        UpdateBountyInfoShow(bountyAttribute);
        yield return null;
        _introSize1.enabled = true;
        _introSize2.enabled = true;
        yield return null;
        _canvasGroup.alpha = 1;
    }

    /// <summary>
    /// 更新任务信息显示
    /// </summary>
    /// <param name="bountyAttribute"></param>
    private void UpdateBountyInfoShow(BountyAttribute bountyAttribute)
    {
        if (bountyAttribute == null)
        {
            return;
        }
        try
        {
            //更新目标
            foreach (List<int> item in bountyAttribute.BountyTemplate.bountyTarget)
            {
                GameObject obj = ResourceLoadUtil.InstantiateRes(_numIntroObj, _introList1);
                _introLoadObjs.Add(obj);
                //
                List<string> list = BountySystem.Instance.GetTargetStr(item);
                //
                obj.transform.Find("Text").GetComponent<Text>().text = list[0] + list[1];
                obj.transform.Find("Text/Bg/Text").GetComponent<Text>().text = "0/" + list[2];
            }
            //地图
            Map_template template = Map_templateConfig.GetMap_templat(bountyAttribute.BountyTemplate.bountyLocation);
            _bountyMap.text = _mapIntroStr + (template == null ? string.Empty : template.mapName);
            //金币
            _goldText.text = "+" + bountyAttribute.BountyReward.gold;
            //声望
            foreach (KeyValuePair<int, int> item in bountyAttribute.BountyReward.renowRewards)
            {
                GameObject obj = ResourceLoadUtil.InstantiateRes(_renowObj, _introList2);
                _introLoadObjs.Add(obj);
                obj.transform.Find("Text").GetComponent<Text>().text = BountySystem.Instance.GetRenowRewardStrs(item.Key, item.Value);
            }
            //间隔线
            // _introLoadObjs.Add(ResourceLoadUtil.InstantiateRes(_lineObj, _introList2));
            bool isShow = bountyAttribute.BountyReward.itemRewards.Count > 0;
            _line2Obj.SetActive(isShow);
            //物品
            ResourceLoadUtil.DeleteChildObj(_intro2Item);
            foreach (ItemData item in bountyAttribute.BountyReward.itemRewards)
            {
                _introLoadObjs.Add(BountySystem.Instance.GetItem(item, _intro2Item, _itemIntroObj));
            }
        }
        catch (Exception e)
        {
            LogHelper_MC.LogError(e.Message);
        }
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }
        //
        Transform taskIntro = transform;
        _canvasGroup = taskIntro.GetComponent<CanvasGroup>();
        _introScrollRect1 = taskIntro.Find("Intro1/List").GetComponent<ScrollRect>();
        _introScrollRect2 = taskIntro.Find("Intro2/List").GetComponent<ScrollRect>();
        _introList1 = taskIntro.Find("Intro1/List/Viewport/Content");
        _introList2 = taskIntro.Find("Intro2/List/Viewport/Content");
        _line2Obj = _introList2.Find("Line").gameObject;
        _intro2Item = _introList2.Find("ItemIntro");
        _introSize1 = _introList1.GetComponent<ContentSizeFitter>();
        _introSize2 = _introList2.GetComponent<ContentSizeFitter>();
        _selectObj = taskIntro.Find("Select").gameObject;
        _confirmButton = _selectObj.transform.Find("Confirm").GetComponent<Button>();
        _selectButton = _selectObj.transform.Find("Select").GetComponent<Button>();
        _acceptedObj = _selectObj.transform.Find("Accepted").gameObject;
        //
        _confirmButton.onClick.AddListener(OnClickConfirm);
        _selectButton.onClick.AddListener(OnClickSelect);
        //
        _bountyName = _introList1.Find("Name/Text").GetComponent<Text>();
        _bountyIntro = _introList1.Find("Intro/Text").GetComponent<Text>();
        _bountyMap = _introList1.Find("MapIntro/Text").GetComponent<Text>();
        _numIntroObj = taskIntro.Find("Intro1/Temp/NumIntro").gameObject;
        //
        _goldText = _introList2.Find("Gold/Text").GetComponent<Text>();
        _renowObj = taskIntro.Find("Intro2/Temp/Renow").gameObject;
        _lineObj = taskIntro.Find("Intro2/Temp/Line").gameObject;
        _itemIntroObj = taskIntro.Find("Intro2/Temp/ItemIntro").gameObject;
        //
        _isFirst = true;
    }
    //
    private bool _isFirst;
    private int _bountyId;
    private CoroutineUtil _IUpdateBountyIntro;
    //
    private const string _mapIntroStr = "前往";
    private List<GameObject> _introLoadObjs = new List<GameObject>();
    //
    private ScrollRect _introScrollRect1;
    private ScrollRect _introScrollRect2;
    private Transform _introList1;
    private Transform _introList2;
    private Transform _intro2Item;
    private ContentSizeFitter _introSize1;
    private ContentSizeFitter _introSize2;
    private GameObject _acceptedObj;
    private GameObject _line2Obj;
    private CanvasGroup _canvasGroup;
    //
    private Text _bountyName;
    private Text _bountyIntro;
    private Text _bountyMap;
    private GameObject _numIntroObj;
    //
    private Text _goldText;
    private GameObject _renowObj;
    private GameObject _lineObj;
    private GameObject _itemIntroObj;
    private GameObject _selectObj;
    private Button _confirmButton;
    private Button _selectButton;
}
