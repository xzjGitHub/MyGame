using System.Collections.Generic;

public class SkillDesEx
{
    public static string GetDes(int skillId,CharAttribute charAttr)
    {
        StateAttribute state = new StateAttribute();

        List<string> valueList = new List<string>();

        Combatskill_template skill = Combatskill_templateConfig.GetCombatskill_template(skillId);
        for(int i = 0; i < skill.valueAttribute.Count; i++)
        {
            int tagetSetIndex = skill.valueAttribute[i][0];
            int targetSetId = skill.targetSetList[tagetSetIndex];
            Targetset_template targetset = Targetset_templateConfig.GetTargetset_template(targetSetId);
            int stateIdIndex = skill.valueAttribute[i][1];
            int stateId = targetset.stateList[stateIdIndex];
            State_template st = State_templateConfig.GetState_template(stateId);

            state.charAttribute = charAttr;
            state.targetset_template = targetset;
            state.bvFormula = st.bvFormula;

            string finalValue = FormatValue(state.baseValue,skill.isPercentage[i] == 1);
            valueList.Add(finalValue);

        }
        string finalDes= GetFormatName(skill.skillDescription1,valueList);
        return finalDes;
    }


    private static string FormatValue(float value,bool isPersent)
    {
        string s = "";
        if(isPersent)
        {
            s = Utility.GetPercent(value,2);
            return s;
        }
        s = Utility.GetNumberPoint(value,2).ToString();
        return s;
    }

    private static string GetFormatName(string s,List<string> value)
    {
        if(value.Count == 0)
        {
            return string.Empty;
        }
        if(value.Count == 1)
        {
            return string.Format(s,value[0]);
        }
        if(value.Count == 2)
        {
            return string.Format(s,value[0],value[1]);
        }
        if(value.Count == 3)
        {
            return string.Format(s,value[0],value[1],value[2]);
        }
        if(value.Count == 4)
        {
            return string.Format(s,value[0],value[1],value[2],value[3]);
        }
        if(value.Count == 5)
        {
            return string.Format(s,value[0],value[1],value[2],value[3],value[4]);
        }
        return string.Format(s,value[0],value[1],value[2],value[3],value[4],value[5]);
    }
}

