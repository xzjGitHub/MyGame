using System;
using UnityEngine;
using System.Collections;
using GameEventDispose;

/// <summary>
/// 战斗内容显示
/// </summary>
public class UICombatContentShow : MonoBehaviour
{


    void Start()
    {
        //
        EventDispatcher.Instance.CombatEffect.AddEventListener<CombatContent,SkillTargetType, object>(EventId.CombatEffect, OnCombatContentShow);
    }

    private void OnCombatContentShow(CombatContent arg1, SkillTargetType arg2, object arg3)
    {
        switch (arg1)
        {
            case CombatContent.UseSkill:
                break;
            case CombatContent.HitResult:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


}
