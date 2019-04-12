using System;
using Spine.Unity;
using UnityEngine;

public class UICombatResult : MonoBehaviour
{
    private GameObject aureolegGameObject;
    private GameObject winGameObject;
    private GameObject loseGameObject;
    private SkeletonAnimation winAnimation;
    private SkeletonAnimation loseAnimation;
    //
    private bool isFirst;




    private void Init()
    {
        if (isFirst) return;
        gameObject.AddComponent<UIAlterParticleSystemLayer>().Init();
        aureolegGameObject = transform.Find("effect_StateEffect06/guang").gameObject;
        winGameObject = transform.Find("effect_StateEffect06/effect/Win").gameObject;
        loseGameObject = transform.Find("effect_StateEffect06/effect/Lose").gameObject;
        //
        winAnimation = winGameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        loseAnimation = loseGameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        isFirst = true;
    }


    /// <summary>
    /// 播放胜利
    /// </summary>
    public void PlayWin()
    {
         Init();
        //
        gameObject.SetActive(true);
        aureolegGameObject.SetActive(true);
        loseGameObject.SetActive(false);
        //
        if (winAnimation == null) return;
        winGameObject.SetActive(true);
        winAnimation.AnimationState.SetAnimation(0, "effect_BattleEffect03", false);
    }

    /// <summary>
    /// 播放失败
    /// </summary>
    public void PlayLose()
    {
        Init();
        //
        gameObject.SetActive(true);
        aureolegGameObject.SetActive(false);
        winGameObject.SetActive(false);
        //
        if (loseAnimation == null) return;
        try
        {
            loseGameObject.SetActive(true);
            loseAnimation.AnimationState.SetAnimation(0, "effect_BattleEffect04", false);
        }
        catch (Exception e)
        {
            LogHelperLSK.LogWarning("000");
        }

    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }

}
