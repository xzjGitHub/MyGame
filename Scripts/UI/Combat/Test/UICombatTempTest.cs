using GameEventDispose;
using System.Collections.Generic;
using UnityEngine;
public class UICombatTempTest : MonoBehaviour
{
    public UICombatUIOperation combatUIOperation;

    private void Start()
    {
     //   CreateCombatSystem();
     //   combatUIOperation.Init();
    }


    private void CreateCombatSystem()
    {
        combatSystem = new CombatSystem();
        CombatTeamInfo _playCombatTeamInfo = CreateCombatTeam(0, TeamType.Player);
        CombatTeamInfo _enemyCombatTeamInfo = CreateCombatTeam(1, TeamType.Enemy);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat,
            (object)new CombatTeamInfo[] { _playCombatTeamInfo, _enemyCombatTeamInfo });
    }

    private CombatTeamInfo CreateCombatTeam(int teamID, TeamType teamType = TeamType.Player)
    {

        List<CombatUnit> combatUnits = new List<CombatUnit>();
        for (int i = 0; i < 4; i++)
        {
            combatUnits.Add(CreateCombatUnit(6001, i + 1, i));
        }
        return new CombatTeamInfo(teamID, teamType, combatUnits);
    }

    private CombatUnit CreateCombatUnit(int charTemplate, int charID = 1, int index = 0)
    {
        return new CombatUnit(new CharAttribute(new CharCreate(charTemplate,1, charID)), index);
    }

    //
    private CombatSystem combatSystem;
}
