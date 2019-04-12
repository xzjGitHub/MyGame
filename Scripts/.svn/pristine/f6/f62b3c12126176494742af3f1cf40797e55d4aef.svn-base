using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameEventDispose;

public class UICharPlayerSkillOperation : MonoBehaviour
{
    private List<UICharPlayerSkillInfo> playerSkillInfos = new List<UICharPlayerSkillInfo>();
    //
    private CombatSystem combatSystem;
    //
    private bool isFirst;

    private List<Vector3> vectors = new List<Vector3>
    {
        new Vector3(-259f,-112,0),
        new Vector3(-83f,-112,0),
        new Vector3(91f,-112,0),
        new Vector3(265,-112,0)
    };


    public void Init(bool isTest = false)
    {
        if (isTest) return;
        GetObj();
        //
        if (GameModules.Find(ModuleName.combatSystem) == null) return;
        //
        LoadSkillShow();
    }

    private void LoadSkillShow()
    {
        combatSystem = GameModules.Find(ModuleName.combatSystem) as CombatSystem;
        //
        for (int i = 0; i < combatSystem.PlayerSkills.Count; i++)
        {
            playerSkillInfos[i].LoadSkillShow(combatSystem.PlayerSkills[i], vectors[i]);
        }
    }

    void GetObj()
    {
        if (isFirst) return;
        //
        playerSkillInfos.Clear();
        foreach (Transform item in transform)
        {
            playerSkillInfos.Add(item.gameObject.AddComponent<UICharPlayerSkillInfo>());
        }
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
        //
        isFirst = true;
    }

    private void OnCombatEvent(PlayCombatStage arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatStage.CreateCombat:
                break;
            case PlayCombatStage.Opening:
                break;
            case PlayCombatStage.CombatPrepare:
                break;
            case PlayCombatStage.CreateRound:
                LoadSkillShow();
                break;
            case PlayCombatStage.ChooseSkill:
                break;
            case PlayCombatStage.ImmediateSkillEffect:
                break;
            case PlayCombatStage.RoundInfo:
                break;
            case PlayCombatStage.CombatEnd:
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
    }
}
