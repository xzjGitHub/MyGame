using UnityEngine;

/// <summary>
/// 角色基类
/// </summary>
public class UICharBase : MonoBehaviour
{
    //
    public Transform moveTrans;
    public Transform fixedTrans;
    public int teamID;
    public int charID;
    public int charIndex;
    public TeamType teamType;
    public CombatUnit combatUnit;

    public int TemplateID { get { return combatUnit.charAttribute.templateID; } }
    //



    protected void InitInfo(CombatUnit combatUnit)
    {
        teamID = combatUnit.teamId;
        charID = combatUnit.charAttribute.charID;
        charIndex = combatUnit.initIndex;
        teamType = combatUnit.teamType;
        this.combatUnit = combatUnit;
    }

    protected void InitInfo(UICharBase charBase)
    {
        moveTrans = charBase.moveTrans;
        fixedTrans = charBase.fixedTrans;
        teamID = charBase.teamID;
        charID = charBase.charID;
        charIndex = charBase.charIndex;
        teamType = charBase.teamType;
        combatUnit = charBase.combatUnit;
    }

}

public class UICharInfo
{
    public int teamID;
    public int charID;
    public int charIndex;

    public UICharInfo(int teamID, int charID, int charIndex)
    {
        this.teamID = teamID;
        this.charID = charID;
        this.charIndex = charIndex;
    }

    public UICharInfo(CREffectResult effectResult)
    {
        teamID = effectResult.hitTeamId;
        charID = effectResult.hitCharId;
        charIndex = effectResult.hitIndex;
    }
}