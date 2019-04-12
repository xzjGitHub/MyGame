

public partial class BuildingAttribute
{
    private static BuildingAttribute buildingAttribute;
    public static BuildingAttribute Building
    {
        get
        {
            if(buildingAttribute == null)
            {
                buildingAttribute = new BuildingAttribute();
            }
            return buildingAttribute;
        }
    }

    public float GetFinalRewardVaue(Building_template bt,Building_levelup bl,int charaCount,int buiLevel)
    {
        building_template = bt;
        return finalRewardVaue;
    }

    public float GetFinalRewardVaue(Core_lvup core_Lvup,Building_template bt)
    {
        core_lvup = core_Lvup;
        building_template = bt;
        return finalRewardVaue;
    }


    public float GetbaseRewardValue(Building_template bt,float currentPower)
    {
        building_template = bt;
        currentCorePower = currentPower;
        return buildingAttribute.baseRewardValue;
    }

    public float GetfinalRewardVaue(Building_template bt,Core_lvup core_Lvup,float currentPower,float coreCapacity)
    {
        building_template = bt;
        currentCorePower = currentPower;
        this.coreCapacity = coreCapacity;
        this.core_lvup = core_Lvup;
        return buildingAttribute.finalRewardVaue;
    }

    public float GetcombatBonus()
    {
        return 0;
    }

    public int GetMacChar(Building_levelup bl)
    {
        return 0;
    }


    public float GetUnitManaCost(HR_config hR_Config)
    {
        hr_config = hR_Config;
        return buildingAttribute.unitManaCost;
    }

    public int GetFinalGoldSales(Core_lvup cl,Shop_config sc)
    {
        core_lvup = cl;
        shop_config = sc;
        return (int)buildingAttribute.finalGoldSales;
    }


    public float finalRewardLevel(Building_template bt,Blackmarket_template blackmarket = null,int index = 0)
    {
        this.building_template = bt;
        blackmarket_template = blackmarket;
        switch(building_template.buildingType)
        {
            case 1:
                return rr_template.baseRewardLevel[index];
            case 2:
                return 100;
            case 3:
                return 100;
            case 4:
                return 100;
            case 5:
                return 100;
            case 6:
                return blackmarket_template.baseRewardLevel[index];
            default:
                return 0;
        }

    }

    public float GetEquipMakeGoldCost(Forge_template forge)
    {
        forge_template = forge;
        return buildingAttribute.forgeGoldCost;
    }

    public float GetEquipMakeManaCost(Forge_template forge)
    {
        forge_template = forge;
        return buildingAttribute.manaCost;
    }
}
