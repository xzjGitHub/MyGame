using System;
using System.Reflection;

public partial class EnchantRnd
{
    public int finalItemLevel;
    public float upgradeAll;
    public int templatID;
    public int instanceID;
    //
    public Char_config char_config;
    public Item_instance item_instance;
    public Char_lvup char_lvup;


    public EnchantRnd(int instanceID, int maxItemLevel)
    {
        char_config = Char_configConfig.GetConfig();
        item_instance = Item_instanceConfig.GetItemInstance(instanceID);

        finalItemLevel = Math.Min(item_instance.maxItemLevel, maxItemLevel != 0 ? maxItemLevel : item_instance.maxItemLevel);
        //
        this.instanceID = instanceID;
        templatID = RandomBuilder.RandomValues(item_instance.template,1)[0];
        enchant_template = Enchant_templateConfig.GetEnchant_Template(templatID);
        char_lvup = Char_lvupConfig.GetChar_Lvup(finalItemLevel);
        //
        upgradeAll = GetRandom_Normal(enchant_template.upgrade[0], enchant_template.upgrade[1]);
        //随机字段操作
      //  RandomFieldOperation(enchant_template.rndAttribute);
        //
        TestLog();
    }

    public EnchantRnd(int instanceID,int maxItemLevel,int upgradeall,int upgradernd)
    {
        char_config = Char_configConfig.GetConfig();
        item_instance = Item_instanceConfig.GetItemInstance(instanceID);

        finalItemLevel = maxItemLevel;
        //
        templatID = RandomBuilder.RandomValues(item_instance.template,1)[0];
        enchant_template = Enchant_templateConfig.GetEnchant_Template(templatID);
        char_lvup = Char_lvupConfig.GetChar_Lvup(finalItemLevel);
        //
        upgradeAll = upgradeall;
        //随机字段操作
      //  RandomFieldOperation(enchant_template.rndAttribute);
        //
        TestLog();
    }

    public EnchantRnd(EnchantRnd enchantRnd)
    {
        finalItemLevel = enchantRnd.finalItemLevel;
        upgradeAll = enchantRnd.upgradeAll;
        templatID = enchantRnd.templatID;
        instanceID = enchantRnd.instanceID;
        //
        char_config = Char_configConfig.GetConfig();
        item_instance = Item_instanceConfig.GetItemInstance(instanceID);
        char_lvup = Char_lvupConfig.GetChar_Lvup(finalItemLevel);
        enchant_template = Enchant_templateConfig.GetEnchant_Template(templatID);
        //创建随机字段
        CreateRandField();
        //
        TestLog();
    }

    public EnchantRnd(EnchantRndData rndData)
    {
        finalItemLevel = rndData.finalItemLevel;
        upgradeAll = rndData.upgradeAll;
        templatID = rndData.templatID;
        instanceID = rndData.instanceID;
        //
        char_config = Char_configConfig.GetConfig();
        item_instance = Item_instanceConfig.GetItemInstance(instanceID);
        char_lvup = Char_lvupConfig.GetChar_Lvup(finalItemLevel);
        enchant_template = Enchant_templateConfig.GetEnchant_Template(templatID);
        //创建随机字段
        CreateRandField();
        //
        TestLog();
    }



    public EnchantRndData GetData()
    {
        return new EnchantRndData
        {
            finalItemLevel = finalItemLevel,
            upgradeAll = upgradeAll,
            templatID = templatID,
            instanceID = instanceID,
        };
    }

    /// <summary>
    /// 创建随机字段
    /// </summary>
    private void CreateRandField()
    {
        //
        //if (_randomField.Count <= 0)
        //{
        //    return;
        //}

        //for (int i = 0; i < _randomField.Count; i++)
        //{
        //    if (_randomField[i] == "")
        //    {
        //        continue;
        //    }
        //    FieldInfo memberInfo = CombatSystemTools.GetField(this, "_is" + _randomField[i]);
        //    if (memberInfo == null)
        //    {
        //        continue;
        //    }
        //    memberInfo.SetValue(this, true);
        //}
    }

    /// <summary>
    /// 获得正态分布
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    private float GetRandom_Normal(float max, float min)
    {
        return (int)(RandomBuilder.Random_Normal(max, min) * 100) / 100f;
    }

    /// <summary>
    /// 测试日志
    /// </summary>
    private void TestLog()
    {
        return;
    }
}