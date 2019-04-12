using GameEventDispose;
using UnityEngine;

public class UIRoundCountdown : MonoBehaviour
{

    public delegate void CallBack();

    public CallBack CallTimeOver;

    public void Init(bool isTest = false)
    {
        this.isTest = isTest;
        isStart = false;
        gameObject.SetActive(false);
        //
        maxTime = Combat_configConfig.GetCombat_config().roundPause;
        if (!isFirst)
        {
            coolDownInfo = gameObject.AddComponent<UICoolDownInfo>();
            coolDownInfo.InitInfo(maxTime);
        }
        //
        if (isStart)
        {
            return;
        }
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
        isFirst = true;
    }


    private void OnCombatEvent(PlayCombatStage arg1, object arg2)
    {
        if (isTest)
        {
            return;
        }

        switch (arg1)
        {
            case PlayCombatStage.CreateCombat:
                break;
            case PlayCombatStage.Opening:
                break;
            case PlayCombatStage.CombatPrepare:
                break;
            case PlayCombatStage.CreateRound:
                gameObject.SetActive(true);
                isStart = true;
                time = 0;
                break;
            case PlayCombatStage.ChooseSkill:
                break;
            case PlayCombatStage.ImmediateSkillEffect:
                break;
            case PlayCombatStage.RoundInfo:
                isStart = false;
                gameObject.SetActive(false);
                break;
            case PlayCombatStage.CombatEnd:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!isStart || isTest)
        {
            return;
        }

        time += Time.deltaTime;
        if (time >= maxTime)
        {
            isStart = false;
            if (CallTimeOver != null)
            {
                CallTimeOver();
            }

        }
        coolDownInfo.UpdateValue(time);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
    }


    //

    private float maxTime = 5f;
    private float time;
    private bool isStart;
    private bool isFirst;
    private bool isTest;

    //
    private UICoolDownInfo coolDownInfo;

}
