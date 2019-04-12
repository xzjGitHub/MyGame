/// <summary>
/// 作者：XZJ
/// 日期：2019/4/9 15:37:49
/// 作用：装备附魔属性工具类
/// 注意：不要在此类里面写代码,如有需要,请添加一个此类的部分类
/// <summary>

public partial class EnchantAttriUtil
{
	public static float GetAttrValue(EquipAttribute attr,string attrName)
	{
		switch(attrName)
		{
			case "finalItemLevel":
			    return attr.enchantRnd.finalItemLevel;
			case "enchantAPBonus":
			    return attr.enchantRnd.enchantAPBonus;
			case "enchantSPBonus":
			    return attr.enchantRnd.enchantSPBonus;
			case "enchantSkillPB":
			    return attr.enchantRnd.enchantSkillPB;
			case "enchantShieldDB":
			    return attr.enchantRnd.enchantShieldDB;
			case "enchantArmorDB":
			    return attr.enchantRnd.enchantArmorDB;
			case "enchantHPDB":
			    return attr.enchantRnd.enchantHPDB;
			case "enchantShieldBonus":
			    return attr.enchantRnd.enchantShieldBonus;
			case "enchantArmorBonus":
			    return attr.enchantRnd.enchantArmorBonus;
			case "enchantHPBonus":
			    return attr.enchantRnd.enchantHPBonus;
			default:
			  return 0;
		}
	}
}
