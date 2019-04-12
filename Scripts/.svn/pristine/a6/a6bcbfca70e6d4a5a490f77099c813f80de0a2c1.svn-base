using System.Collections;
using UnityEngine;

public class UIPlayImmediateShowEffect : MonoBehaviour
{

    public void StartPlay(CRImmediateShowEffect effect)
    {
        UICharUnit charUnit = UICombatTool.Instance.GetCharUI(effect.teamId, effect.index);
        if (charUnit == null)
        {
            PlayEnd();
            return;
        }

        UIPlayEffect playEffect = null;
        switch (effect.effectType)
        {
            case ImmediateShowEffectType.HPRecover:
                // playEffect = PlayEffectTool.PlayEffect(new EffectInfo());
                break;
            case ImmediateShowEffectType.ArmorRecover:
                string effectName = ResourceLoadUtil.armorRegRes;
                playEffect = PlayEffectTool.PlayEffect(new EffectInfo(effectName, effectName, charUnit.moveTrans,
                    charUnit.AtkSortingOrder, charUnit.BonePos(SkeletonTool.RootName)));
                break;
            case ImmediateShowEffectType.ShieldRecover:
                effectName = ResourceLoadUtil.shieldRegRes;
                playEffect = PlayEffectTool.PlayEffect(new EffectInfo(effectName, effectName, charUnit.moveTrans,
                    charUnit.AtkSortingOrder, charUnit.BonePos(SkeletonTool.RootName)));
                break;
        }

        if (playEffect == null)
        {
            PlayEnd();
            return;
        }

        new CoroutineUtil(IECheckPlay(playEffect));
    }

    private IEnumerator IECheckPlay(UIPlayEffect playEffect)
    {
        while (!playEffect.IsPlayEnd)
        {
            yield return null;
        }
        DestroyImmediate(playEffect);
        PlayEnd();
    }

    /// <summary>
    /// 播放结束
    /// </summary>
    private void PlayEnd()
    {
        DestroyImmediate(this);
    }

}
