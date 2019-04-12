/// <summary>
/// 界面战斗工具
/// </summary>
public class UICombatTool
{
    public static UICombatTool Instance
    {
        get
        {
            if (combatTool == null)
            {
                combatTool = new UICombatTool();
            }
            return combatTool;
        }
    }

    public CombatManager CombatManager
    {
        get
        {
            return combatManager;
        }
    }

    public CombatSystem CombatSystem
    {
        get
        {
            object temp = GameModules.Find(ModuleName.combatSystem);
            combatSystem = temp != null ? temp as CombatSystem : null;
            return combatSystem;
        }
    }

    public UICombatTeam GetTeamUI(int teamID, bool isOneself = true)
    {
        return isOneself ? combatManager.CombatTeams.Find(a => a.teamID == teamID) : combatManager.CombatTeams.Find(a => a.teamID != teamID);
    }

    public UICharUnit GetCharUI(int teamID, int index)
    {
        return GetTeamUI(teamID).GetChar(index);
    }

    public void Init(CombatManager combat)
    {
        combatManager = combat;
    }


    //
    private CombatSystem combatSystem;
    private CombatManager combatManager;
    //
    private static UICombatTool combatTool;
}
