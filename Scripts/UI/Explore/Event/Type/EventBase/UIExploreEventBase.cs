using System;
using System.Collections;
using System.Collections.Generic;
using GameEventDispose;
using Spine;
using Spine.Unity;
using UnityEngine;

public partial class UIExploreEventBase : MonoBehaviour
{
    public int SceneIndex;
    public float ScenePos;

    public delegate void CallBackBool(object param, Action action);

    public CallBackBool OnVisitReady;

    public bool IsEnterScreen { get { return eventPosDetection.IsEnterScreen; } }

    /// <summary>
    /// 立即访问
    /// </summary>
    public void ImmediateVisit()
    {
        StartVisit();
    }

    public void ResetButton()
    {
        buttonImage.raycastTarget = true;
    }
    /// <summary>
    /// 添加事件
    /// </summary>
    protected void AddEvent(List<EventAttribute> events)
    {
        if (OnAddEvent != null)
        {
            OnAddEvent(events);
        }
    }

    private void OnClickHei()
    {
        if (!ExploreSystem.Instance.IsCanVisit(eventIndex))
        {
            return;
        }

        OpenEventShow();
        if (eventType == WPEventType.Trap)
        {
            OnClickOpenOption(ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Abandon));
            return;
        }
        buttonImage.raycastTarget = true;
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        if (!ExploreSystem.Instance.IsCanVisit(eventIndex))
        {
            return;
        }

        buttonImage.raycastTarget = false;
        if (OnVisitReady != null)
        {
            OnVisitReady(this, StartVisit);
        }
    }
    /// <summary>
    /// 开始访问
    /// </summary>
    private void StartVisit()
    {
        if (eventType != WPEventType.Trap)
        {
            if (OnStartOpen != null)
            {
                OnStartOpen(this);
            }
        }

        exploreEventPopup.OnOpen = OnClickOpenOption;
        exploreEventPopup.OnExit = OnClickExitOption;
        exploreEventPopup.OnOpenPrepare = OnClickOpenPrepare;
        exploreEventPopup.OpenUI(eventAttribute);
    }

    /// <summary>
    /// 点击了打开选项
    /// </summary>
    protected void OnClickOpenOption(object param)
    {
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisitEvent, (object)eventAttribute);
        wpVisitEventResult = param as WPVisitEventResult;

        switch (eventType)
        {
            case WPEventType.Combat:
            case WPEventType.Boss:
                OnEventProgressAchieve(param);
                return;
        }
        if (eventType == WPEventType.Trap)
        {
            showTranS.gameObject.SetActive(true);
            if (OnStartOpen != null)
            {
                OnStartOpen(this);
            }
        }
        //
        if (eventProgress != null)
        {
            eventProgress.OnProgressAchieve = OnEventProgressAchieve;
            eventProgress.param = param;
            eventProgress.OpenUI(event_Info.visitTime);
        }

        if (OnProgressing != null)
        {
            OnProgressing(null);
        }
    }
    /// <summary>
    /// 点击了离开选项
    /// </summary>
    protected void OnClickExitOption(object param)
    {
        if (OnExitEvent != null)
        {
            OnExitEvent(param);
        }

        OnCallEventAutoAbandonVisit2();
    }


    protected void EventList(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case bxName2Str:
                //打开爆点特效
                if (explodeGameObject != null)
                {
                    explodeGameObject.SetActive(true);
                }

                if (itemLaunchPosition != null)
                {
                    //发射物品
                    itemLaunchPosition.PlayLaunchItem();
                }

                VisitEventEnd();

                //  alterParticleSystemAlpha.StartUpdate();
                break;
            case bxName3Str:
                break;
        }
    }

    /// <summary>
    /// 打开准备
    /// </summary>
    private void OnClickOpenPrepare(object param)
    {
        if (OnOpenPrepare != null)
        {
            OnOpenPrepare(null);
        }
    }

    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
        if (OnVisiting != null)
        {
            OnVisiting(null);
        }

        if (OnProgressAchieve != null)
        {
            OnProgressAchieve(param);
        }
    }

    /// <summary>
    /// 事件访问结束
    /// </summary>
    protected void VisitEventEnd()
    {
        if (Text != null)
        {
            Text.gameObject.SetActive(false);
        }

        if (heightLightEffect != null)
        {
            heightLightEffect.gameObject.SetActive(false);
        }

        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisitEventEnd, (object)eventAttribute);
    }


    protected void Scale(int initSize = 0)
    {
        return;
        new CoroutineUtil(IEScale(initSize));
    }
    /// <summary>
    /// 渐隐渐显
    /// </summary>
    protected void FadeFadeIn(object param, bool isDestroy = true, bool isFade = true)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        new CoroutineUtil(IEFade(param, isDestroy, isFade));
        //  new CoroutineUtil(IEFade(param, false, false));
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow(string sortingLayerName, int sortingOrder)
    {
        if (uiCanvas == null)
        {
            uiCanvas = gameObject.GetComponent<Canvas>();
        }

        if (uiCanvas == null)
        {
            uiCanvas = gameObject.AddComponent<Canvas>();
        }

        uiCanvas.overrideSorting = true;
        uiCanvas.sortingOrder = sortingOrder;
        uiCanvas.sortingLayerName = sortingLayerName;
        //
        foreach (Canvas item in canvas)
        {
            if (item == null)
            {
                continue;
            }

            item.sortingOrder = sortingOrder;
            item.sortingLayerName = sortingLayerName;
        }
        //
        foreach (UIAlterParticleSystemLayer item in alterParticles)
        {
            if (item == null)
            {
                continue;
            }

            item.Init();
            item.UpdateShow(sortingLayerName, sortingOrder);
        }
        //
        foreach (Renderer item in renderers)
        {
            if (item == null)
            {
                continue;
            }

            item.sortingOrder = sortingOrder;
            item.sortingLayerName = sortingLayerName;
        }
    }

    private IEnumerator IEFade(object param, bool isDestroy, bool isFade)
    {
        if (isFade)
        {
            foreach (UIAlterParticleSystemLayer item in alterParticles)
            {
                item.gameObject.SetActive(false);
            }
        }
        //
        sum1 = 0;
        sum = 0;
        //
        sum++;
        if (Text != null)
        {
            new CoroutineUtil(IEFade(Text, isFade));
            //
            if (param is SkeletonAnimation)
            {
                sum++;
                new CoroutineUtil(IEFade(param as SkeletonAnimation, isFade));
            }
        }
        //
        if (param is GameObject)
        {
            sum++;
            new CoroutineUtil(IEFade(param as GameObject, isFade));
        }
        //
        if (param is Canvas)
        {
            sum++;
            new CoroutineUtil(IEFade((param as Canvas).gameObject, isFade));
        }
        //
        while (sum != sum1)
        {
            yield return null;
        }
        //
        if (OnFadeOk != null)
        {
            OnFadeOk(null);
        }

        if (isDestroy)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 渐隐
    /// </summary>
    private IEnumerator IEFade(SkeletonAnimation skeletonAnimation, bool isFade)
    {
        if (isFade)
        {
            skeletonAnimation.skeleton.A = 1;
            while (skeletonAnimation.skeleton.A > 0)
            {
                if (skeletonAnimation.skeleton.A - Time.deltaTime * aspd < 0)
                {
                    skeletonAnimation.skeleton.A = 0;
                    continue;
                }
                skeletonAnimation.skeleton.A -= Time.deltaTime * aspd;
                yield return null;
            }
            skeletonAnimation.skeleton.A = 0;
            skeletonAnimation.GetComponent<MeshRenderer>().enabled = false;
            sum1++;
            yield break;
        }
        skeletonAnimation.skeleton.A = 0;
        skeletonAnimation.GetComponent<MeshRenderer>().enabled = true;
        while (skeletonAnimation.skeleton.A < 1)
        {
            if (skeletonAnimation.skeleton.A + Time.deltaTime * aspd > 1)
            {
                skeletonAnimation.skeleton.A = 1;
                continue;
            }
            skeletonAnimation.skeleton.A += Time.deltaTime * aspd;
            yield return null;
        }
        skeletonAnimation.skeleton.A = 1;
        sum1++;
    }
    /// <summary>
    /// 渐隐
    /// </summary>
    private IEnumerator IEFade(GameObject obj, bool isFade)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        yield return null;
        if (isFade)
        {
            canvasGroup.alpha = 1;
            while (canvasGroup.alpha > 0)
            {
                if (canvasGroup.alpha - Time.deltaTime * aspd < 0)
                {
                    canvasGroup.alpha = 0;
                    continue;
                }
                canvasGroup.alpha -= Time.deltaTime * aspd;
                yield return null;
            }
            canvasGroup.alpha = 0;
            sum1++;
            yield break;
        }
        //
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            if (canvasGroup.alpha + Time.deltaTime * aspd > 1)
            {
                canvasGroup.alpha = 1;
                continue;
            }
            canvasGroup.alpha += Time.deltaTime * aspd;
            yield return null;
        }
        canvasGroup.alpha = 1;
        sum1++;
    }
    private IEnumerator IEScale(int initSize = 0)
    {
        yield return null;
        ShowTranS.localScale = Vector3.one * initSize;
        if (initSize == 0)
        {
            while (ShowTranS.localScale.x < 1)
            {
                if (ShowTranS.localScale.x + Time.deltaTime * aspd > 1)
                {
                    ShowTranS.localScale = Vector3.one;
                    yield return null;
                }
                ShowTranS.localScale += Vector3.one * Time.deltaTime * aspd;
                yield return null;
            }
            yield break;
        }
        while (ShowTranS.localScale.x > 0)
        {
            if (ShowTranS.localScale.x - Time.deltaTime * aspd < 0)
            {
                ShowTranS.localScale = Vector3.zero;
                yield return null;
            }
            ShowTranS.localScale -= Vector3.one * Time.deltaTime * aspd;
            yield return null;
        }

    }

    private void OpenEventShow()
    {
        //if (isShow) return;
        if (heiObj != null)
        {
            heiObj.gameObject.SetActive(false);
        }

        if (shotObj != null)
        {
            shotObj.SetActive(true);
        }

        if (particleSystemAlpha != null)
        {
            particleSystemAlpha.ResetAlpha();
        }
        //
        if (heightLightEffect != null)
        {
            heightLightEffect.gameObject.SetActive(true);
        }

        if (OnOpenShow != null)
        {
            OnOpenShow(null);
        }
        //isShow = true;
    }



}
