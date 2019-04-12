using System;
using System.Collections.Generic;
using Altar.Data;
using Core.Data;
using GameEventDispose;

namespace Altar.Controller
{
    public partial class AltarController: IController
    {
        private const int SpecialCharTag = 2;   //稀有角色标志

        public Action Refresh;

        public void Initialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        //一周更新一次
        private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
        {
            if(arg1 == ScriptTimeUpdateType.Week)
            {
                UpdateNowAndNextCallChars();
                if(Refresh != null)
                {
                    Refresh();
                }
            }
        }

        public void Init()
        {
            InitCallPool();
            UpdateNowAndNextCallChars();
        }

        private void InitCallPool()
        {
            int scriptId = ScriptSystem.Instance.ScriptId;
            Summon_list summon_List = Summon_listConfig.GetSummon_List(scriptId);
            for(int i = 0; i < summon_List.summonList.Count; i++)
            {
                AltarSystem.Instance.InitAltarCharList(summon_List.summonList[i]);
            }
        }

        #region 普通召唤

        public void UpdateNowAndNextCallChars()
        {
            Dictionary<string,CharData> nextList = AltarSystem.Instance.GetNextCallList();
            if(nextList.Count == 0)
            {
                AltarSystem.Instance.UpdateNextCallChars(GetRandomPlayer());
            }
            nextList = AltarSystem.Instance.GetNextCallList();
            AltarSystem.Instance.UpdateNowCallChars(nextList);

            AltarSystem.Instance.UpdateNextCallChars(GetRandomPlayer());
            LogHelperLSK.Log("刷新召唤");
        }

        private Dictionary<string,CharData> GetRandomPlayer()
        {
            Dictionary<string,CharData> list = new Dictionary<string,CharData>();
            List<int> randomPool = AltarSystem.Instance.GetAllAltarList();
            if(randomPool.Count == 0)
            {
                return list;
            }

            Core_lvup core_Lvup = GetCore_Lvup();
            int randomCount = core_Lvup.maxSummonList;

            for(int i = 0; i < randomCount; i++)
            {
                int charId = GetRandomCharId();
                if(charId == -1)
                {
                    continue;
                }
                CharCreate charCreate = new CharCreate(charId);
                int baseLevel = core_Lvup.summonCharLevel;
                float deviation = Game_configConfig.GetGame_Config().summonDeviation;
                int finalLevel = (int)(baseLevel * (UnityEngine.Random.Range(1 - deviation,1 + deviation)));
                charCreate.charLevel = finalLevel == 0 ? 1 : finalLevel;
                CharAttribute charAttribute = CharSystem.Instance.CreateChar(charCreate,false);
                //这个Id是给我用的
                charAttribute.charID = i;

                list.Add(Utility.GenerateOnlyId(),charAttribute.GetCharData());
            }
            return list;
        }

        private int GetRandomCharId()
        {
            List<int> chanceList = GetChanceList();
            int sum = Utility.GetListCount(chanceList);
            int random = UnityEngine.Random.Range(1,sum);
            List<int> sumList = Utility.GetSumList(chanceList);
            int index = Utility.GetIndex(sumList,random);
            if(index != -1)
            {
                List<int> randomPool = AltarSystem.Instance.GetAllAltarList();
                return randomPool[index];
            }
            else
            {
                return -1;
            }
        }

        private List<int> GetChanceList()
        {
            List<int> tempList = new List<int>();
            int scriptId = ScriptSystem.Instance.ScriptId;
            Summon_list summon_List = Summon_listConfig.GetSummon_List(scriptId);
            List<int> randomPool = AltarSystem.Instance.GetAllAltarList();
            for(int i = 0; i < summon_List.summonList.Count; i++)
            {
                if(randomPool.Contains(summon_List.summonList[i]))
                {
                    tempList.Add(summon_List.selectChance[i]);
                }
            }
            return tempList;
        }


        public Dictionary<string,CharAttribute> GetNormalCallChars()
        {
            return AltarSystem.Instance.GetNormalCallChars();
        }

        public bool CanCall(int charTemplateId)
        {
            if(CharSystem.Instance.CharAttributeList.Count >= GetMaxCharNum())
            {
                TipManager.Instance.ShowTip("角色数量已经达到上限，无法继续召唤");
                return false;
            }

            Summon_cost summon_Cost = Summon_costConfig.GetSummon_Cost(charTemplateId);
            if(ScriptSystem.Instance.Gold < summon_Cost.goldCost)
            {
                TipManager.Instance.ShowTip("金币不足");
                return false;
            }
            if(ScriptSystem.Instance.Mana < summon_Cost.manaCost)
            {
                TipManager.Instance.ShowTip("魔力不足");
                return false;
            }
            return true;
        }

        public void Call(string uid,CharAttribute charAttribute)
        {
            Summon_cost summon_Cost = Summon_costConfig.GetSummon_Cost(charAttribute.templateID);

            ScriptSystem.Instance.SubGold(summon_Cost.goldCost);
            ScriptSystem.Instance.SubGold(summon_Cost.manaCost);

            CharSystem.Instance.AddChar(charAttribute);

            Char_template char_Template = Char_templateConfig.GetTemplate(charAttribute.templateID);
            //判断该角色是否是稀有角色 
            if(char_Template.charType == SpecialCharTag)
            {
                //如果是 将他从召唤列表移除
                AltarSystem.Instance.RemoveCharFromAltarList(charAttribute.charID);
            }

            TipManager.Instance.ShowTip("召唤到" + char_Template.charName);

            AltarSystem.Instance.AddToHasCall(uid);

            GetCharPanel panel = UIPanelManager.Instance.Show<GetCharPanel>();
            panel.UpdateInfo(charAttribute);
        }

        #endregion


        private Core_lvup GetCore_Lvup()
        {
            int coreLevel = CoreSystem.Instance.GetLevel();
            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);
            return core_Lvup;
        }

        public int GetMaxCharNum()
        {
            return 100;
            //todo
            //return GetCore_Lvup().maxChar;
        }
    }
}