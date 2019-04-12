using GameEventDispose;
using UnityEngine;


/// <summary>
/// 角色阶段特效——战斗
/// </summary>
public class UICharPhaseEffectInfo : MonoBehaviour
{
    public UICharStartPhaseEffect CharStartPhaseEffect;
    public UICharProcessPhaseEffect CharProcess1PhaseEffect;
    public UICharProcessPhaseEffect CharProcess2PhaseEffect;
    public UICharEndPhaseEffect CharEndPhaseEffect;
    //
    private bool isOk;
    private bool isStartUpdate;
    private int teamId;
    private int charIndex;
    //
    private CoroutineUtil IE_StartPhasePlayEffect;
    private CoroutineUtil IE_Process1PlayEffect;
    private CoroutineUtil IE_Process2PlayEffect;
    private CoroutineUtil IE_EndPhasePlayEffect;

    public bool IsOk
    {
        get { return isOk; }
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(int _teamId,int _charIndex)
    {
        teamId = _teamId;
        charIndex = _charIndex;
    }

    /// <summary>
    /// 播放阶段特效
    /// </summary>
    public void PlayCharPhaseEffect(int phaseId, CharPhaseStateType _phaseStateType)
    {
        switch (_phaseStateType)
        {
            case CharPhaseStateType.Start:
                isOk = false;
                isStartUpdate = true;
                IE_StartPhasePlayEffect=new CoroutineUtil(CharStartPhaseEffect.PlayEffect(phaseId));
                break;
            case CharPhaseStateType.Process1:
                IE_Process1PlayEffect=new CoroutineUtil(CharProcess1PhaseEffect.PlayEffect(phaseId, CharPhaseStateType.Process1));
                break;
            case CharPhaseStateType.Process2:
                IE_Process2PlayEffect=new CoroutineUtil(CharProcess2PhaseEffect.PlayEffect(phaseId, CharPhaseStateType.Process2));
                break;
            case CharPhaseStateType.END:
                IE_EndPhasePlayEffect=new CoroutineUtil(CharEndPhaseEffect.PlayEffect(phaseId));
                break;
        }
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_EndPhasePlayEffect != null) IE_EndPhasePlayEffect.Stop();
        IE_EndPhasePlayEffect = null;
        if (IE_StartPhasePlayEffect != null) IE_StartPhasePlayEffect.Stop();
        IE_StartPhasePlayEffect = null;
        if (IE_Process1PlayEffect != null) IE_Process1PlayEffect.Stop();
        IE_Process1PlayEffect = null;
        if (IE_Process2PlayEffect != null) IE_Process2PlayEffect.Stop();
        IE_Process2PlayEffect = null;
    }

    private void OnDestroy()
    {
        StopAllCoroutine();
    }

}



public enum CharActionStateType
{
    /// <summary>
    /// 默认为空
    /// </summary>
    Default = 0,
    /// <summary>
    /// 战斗待机
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 冲锋
    /// </summary>
    Charge = 2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack = 3,
    /// <summary>
    /// 受击
    /// </summary>
    Hurt = 4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 5,
    /// <summary>
    /// 胜利
    /// </summary>
    Celebrate = 6,
    /// <summary>
    /// 跑
    /// </summary>
    Run=7,
}

/// <summary>
/// 阶段状态类型
/// </summary>
public enum CharPhaseStateType
{
    /// <summary>
    /// 开始
    /// </summary>
    Start = 1,
    /// <summary>
    /// 过程1
    /// </summary>
    Process1 = 2,
    /// <summary>
    /// 过程2
    /// </summary>
    Process2 = 3,
    /// <summary>
    /// 结束
    /// </summary>
    END = 4,
}