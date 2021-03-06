﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;


public class SkillUtil
{
    public static float GetFinalAttrNumByName(string name,SkillAttribute skillAttribute)
    {
        PropertyInfo propertyInfo = skillAttribute.GetType().GetProperty(name);
        if(propertyInfo == null)
        {
            LogHelper_MC.LogError("skillAttribute not  contain : " + name);
            return 0;
        }
        return Mathf.FloorToInt((float)propertyInfo.GetValue(skillAttribute,null));
    }
}
