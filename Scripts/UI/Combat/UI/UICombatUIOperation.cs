using GameEventDispose;
using UnityEngine;

/// <summary>
/// 战斗UI操作
/// </summary>
public class UICombatUIOperation : MonoBehaviour
{

    public CombatManager CombatManager { get; private set; }

    public void Init(bool isTest = false, float size = 36, Camera camera = null)
    {
        if (isFirst)
        {
            return;
        }
        //
        uiTransform = transform.Find("UI");
        effectTransform = transform.Find("Effect");
        logicTransform = transform.Find("Logic");
        //
        combatLoading = transform.Find("Load").gameObject.AddComponent<UICombatLoading>();
        CombatManager = logicTransform.gameObject.AddComponent<CombatManager>();
        UICombatTool.Instance.Init(CombatManager);
        //
        if (camera == null)
        {
            camera = Camera.main;
        }

        uiTransform.GetComponent<Canvas>().worldCamera = camera;
        effectTransform.GetComponent<Canvas>().worldCamera = camera;
        combatLoading.GetComponent<Canvas>().worldCamera = camera;
        //
        uiTransform.gameObject.AddComponent<UICombatUIInfo>();
        CombatManager.Init(isTest, size, uiTransform, effectTransform, logicTransform);
        combatLoading.Init();
        //
        isFirst = true;
        LoadCombat();
    }

    /// <summary>
    /// 加载战斗
    /// </summary>
    private void LoadCombat()
    {
        combatLoading.OnLoadOK = LoadOK;
        combatLoading.StartLoad();
    }

    private void LoadOK()
    {
        CombatManager.OpenUI();
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CombatPrepare, (object)null);
    }

    //
    private bool isFirst;
    private Transform uiTransform;
    private Transform effectTransform;
    private Transform logicTransform;
    private UICombatLoading combatLoading;

}
