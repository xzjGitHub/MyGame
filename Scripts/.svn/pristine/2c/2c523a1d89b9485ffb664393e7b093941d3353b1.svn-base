using LskConfig;
/// <summary>
/// Event_selectionConfig配置表
/// </summary>
public partial class Event_selectionConfig : TxtConfig<Event_selectionConfig>
{

    /// <summary>
    /// 是否可用
    /// </summary>
    /// <param name="id"></param>
    /// <param name="teamAttribute"></param>
    /// <returns></returns>
    public static bool IsUsable(int id, TeamAttribute teamAttribute)
    {
        Event_selection selection = GetSelection(id);
        if (selection == null)
        {
            return false;
        }
        //检查种族
        foreach (System.Collections.Generic.List<int> charRaces in selection.charRaceReq)
        {
            if (!teamAttribute.IsContainCharRace(charRaces))
            {
                return false;
            }
        }
        //检查职业
        if (!teamAttribute.IsContainCharClass(selection.charClassReq))
        {
            return false;
        }

        return true;
    }
}
