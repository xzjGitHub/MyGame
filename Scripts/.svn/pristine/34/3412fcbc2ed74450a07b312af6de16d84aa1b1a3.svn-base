using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayCheckBuff : MonoBehaviour
{

    public bool IsPlayOk { get { return isPlayOk; } }

    /// <summary>
    /// 开始播放
    /// </summary>
    /// <param name="removeStates"></param>
    /// <param name="combatTeams"></param>
    public void StartPlay(List<CRRemoveState> removeStates, List<UICombatTeam> combatTeams)
    {
        new CoroutineUtil(IEStart(removeStates, combatTeams));
    }


    private IEnumerator IEStart(List<CRRemoveState> removeStates, List<UICombatTeam> combatTeams)
    {
        isPlayOk = false;
        yield return null;
        for (int i = 0; i < removeStates.Count; i++)
        {
            CRRemoveState removeState = removeStates[i];
            UICombatTeam hitTeam = combatTeams.Find(a => a.teamID == removeState.teamID);
            //
            UICharUnit hitCharUnit = hitTeam.GetChar(removeState.index);
            if (hitCharUnit != null)
            {
                foreach (int item in removeState.stateID)
                {
                    hitCharUnit.StateManager.RemoveState(item);
                }
            }
        }
        isPlayOk = true;
    }



    private bool isPlayOk;
}
