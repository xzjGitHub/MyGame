﻿using System.Collections.Generic;

public class ScriptController
{
    public static void InitLevelData(int levelID)
    {
        ScriptSystem scriptSystem = PlayerSystem.Instance.InitScriptSystem(levelID,0);
        TimeUtil.InitTime(Script_templateConfig.GetInitTime(levelID));
        if(!PlayerSystem.Instance.HasPlay(levelID))
        {
            //初始化角色 初始装备
            List<List<int>> characterList = Script_templateConfig.GetInitCharaterList(levelID);

            //角色id,角色装备列表
            Dictionary<int,List<int>> equipLists = Script_templateConfig.GetPlayerAndEquipList(levelID);
            for(int i = 0; i < characterList.Count; i++)
            {
                CharCreate charCreate = new CharCreate(characterList[i][0], characterList[i][1]);
                CharAttribute charAttribute = CharSystem.Instance.CreateChar(charCreate);
                for(int j = 0; equipLists.Count > i && j < equipLists[characterList[i][0]].Count; j++)
                {
                    int equipId = equipLists[characterList[i][0]][j];
                    ItemAttribute equipAttribute = ItemSystem.Instance.CreateItem(equipId,true);
                    if(equipAttribute == null)
                        LogHelper_MC.LogError("装备为空："+ equipId);
                    CharSystem.Instance.CharWearEquipment(equipAttribute.itemID,charAttribute.charID);
                }
            }
            //初始物品
            List<List<int>> itemList = Script_templateConfig.GetIniItemst(levelID);
            for(int i = 0; i < itemList.Count; i++)
            {
                ItemSystem.Instance.CreateItem(itemList[i][0],itemList[i][1],true);
            }

            scriptSystem.AddGold((int)Script_templateConfig.GetInitGold(levelID));
            scriptSystem.AddMana((int)Script_templateConfig.GetInitMana(levelID));
            PlayerSystem.Instance.AddToken(PlayerSystem.Instance.Token);

            ControllerCenter.Instance.ShopController.UpdateProductionCycle();
            ControllerCenter.Instance.AltarController.Init();

            PlayerSystem.Instance.AddScriptId(levelID);
        }
        //测试
        if(ScriptSystem.Instance.Mana < 10000)
        {
            ScriptSystem.Instance.AddMana(10000 - (int)ScriptSystem.Instance.Mana);
        }
        if(ScriptSystem.Instance.Gold < 10000)
        {
            ScriptSystem.Instance.AddGold(10000 - (int)ScriptSystem.Instance.Gold);
        }
        if(PlayerSystem.Instance.Token < 10000)
        {
            PlayerSystem.Instance.AddToken(1000);
        }
        // scriptSystem.AddMana(1000);
       // GameStatusManager.Instance.ChangeStatus(GameStatus.EnterScriptEnd);
    }


    public static void InitLevelData(bool _isNew,int levelID,int _selectIndex)
    {
        ScriptSystem scriptSystem = PlayerSystem.Instance.InitScriptSystem(_isNew,levelID,0,_selectIndex);
        if(_isNew)
        {
            List<List<int>> characterList = Script_templateConfig.GetInitCharaterList(levelID);

            //角色id,角色装备列表
            Dictionary<int,List<int>> equipLists = Script_templateConfig.GetPlayerAndEquipList(levelID);
            for(int i = 0; i < characterList.Count; i++)
            {
                CharAttribute charAttribute = CharSystem.Instance.CreateChar(new CharCreate(characterList[i][0],characterList[i][1]));
                int equipId = 0;
                for(int j = 0; j < equipLists[characterList[i][0]].Count; j++)
                {
                    equipId = (ItemSystem.Instance.CreateItem(equipLists[characterList[i][0]][j],true) as EquipAttribute).itemID;
                    CharSystem.Instance.CharWearEquipment(equipId,charAttribute.charID);
                }
            }
            //初始物品
            List<List<int>> itemList = Script_templateConfig.GetIniItemst(levelID);
            for(int i = 0; i < itemList.Count; i++)
            {
                ItemSystem.Instance.CreateItem(itemList[i][0],itemList[i][1],true);
            }
            //初始建筑
            //   ControllerCenter.Instance.ArchitectureController.InitBuildingLevel(Script_templateConfig.GetIniArchList(levelID));
            //初始金币
            scriptSystem.AddGold((int)Script_templateConfig.GetInitGold(levelID));
            //初始魔力
            scriptSystem.AddMana((int)Script_templateConfig.GetInitMana(levelID));
            scriptSystem.SaveData();
            //初始代币
            PlayerSystem.Instance.AddToken(PlayerSystem.Instance.Token);
            PlayerSystem.Instance.SaveData();
        }
       // GameStatusManager.Instance.ChangeStatus(GameStatus.EnterScriptEnd);
        //GameEventrCenter.Instance.EmitGameStatusChangeEvent(GameStatus.EnterScriptEnd);
    }

}
