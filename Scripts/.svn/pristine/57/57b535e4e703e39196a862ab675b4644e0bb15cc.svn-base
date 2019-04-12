using System;
using System.Collections.Generic;
using Barrack.Data;
using Core.Data;


namespace Barrack.Controller
{
    public partial class BarrackController
    {
        //   状态（1：添加 2：结束  4：取消
        public Action<TraniCharInfo,int> TrainCharChange;
        public Action<int> ExpChange;

        private List<TraniCharInfo> m_trainList;

        private TraniCharInfo m_info;

        private void AddTrainsTime()
        {
            BarrackSystem.Instance.AddTrainsTime(TimeUtil.DaySeconds);
        }

        private void SecondUpdateTrain()
        {
            m_trainList = BarrackSystem.Instance.GetTrainChars();
            for(int i = 0; i < m_trainList.Count; i++)
            {
                m_info = m_trainList[i];
                if(!m_info.SubManaSuc || m_info.HasPause)
                {
                    continue;
                }
                m_info.HaveUseTime++;
                // UnityEngine.Debug.LogError(m_info.HaveUseTime);
                if(m_info.HaveUseTime >= TimeUtil.DaySeconds)
                {
                    DayUpdateExp();
                    m_info.HaveUseTime = 0;
                }
            }
        }

        private void DayUpdateTrain()
        {
            DayTrainSubMana();
        }

        public float GetDayAddExp()
        {
            Core_lvup core_Lvup = GetCore_Lvup();
            return core_Lvup.dailyExpReward;
        }

        private void DayUpdateExp()
        {
            m_trainList = BarrackSystem.Instance.GetTrainChars();
            CharAttribute attr = null;
            Core_lvup core_Lvup = GetCore_Lvup();
            attr = CharSystem.Instance.GetAttribute(m_info.CharId);
            attr.SetCharExp(core_Lvup.dailyExpReward);
            //UnityEngine.Debug.Log("增加经验: " + core_Lvup.dailyExpReward);
            if(ExpChange != null)
            {
                ExpChange(m_info.CharId);
            }
            //   CheckArriveMaxLevel(attr.charID,attr.charLevel,core_Lvup.trainningMaxLevel);
            CheckArriveMaxLevel(attr.charID,attr.charLevel,GetMaxTrainLevel());
        }

        private void CheckArriveMaxLevel(int charId,int nowLevel,int maxLevel)
        {
            if(nowLevel >= maxLevel)
            {
                BarrackSystem.Instance.RemoveCharTrain(charId);
                CharSystem.Instance.SetCharSate(charId,CharStatus.Idle);
                LogHelperLSK.Log("自动结束训练，到达最大可训练等级");
                if(TrainCharChange != null)
                {
                    TrainCharChange(m_info,2);
                }
            }
        }

        private void DayTrainSubMana()
        {
            List<TraniCharInfo> list = BarrackSystem.Instance.GetTrainChars();
            float cost = GetManaCost();
            bool suc = false;
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].HasPause)
                    continue;
                suc = ScriptSystem.Instance.Mana >= cost;
                list[i].SubManaSuc = suc;
                ScriptSystem.Instance.SubMana((int)cost);

                if(CharCanWork != null)
                {
                    CharCanWork(list[i].CharId,ScriptSystem.Instance.Mana >= cost);
                }
            }
        }


        public bool CanTrain(int nowLevel)
        {
            if(GetNowTrainChar() >= GetMaxTrainChar())
            {
                TipManager.Instance.ShowTip("达到当前可训练的人数上限，无法继续添加角色");
                return false;
            }
            if(nowLevel >= GetMaxTrainLevel())
            {
                TipManager.Instance.ShowTip("达到当前可训练的最大等级，无法继续训练");
                return false;
            }
            return true;
        }


        public void TrainChar(int charId)
        {
            TraniCharInfo traniCharInfo = new TraniCharInfo();
            traniCharInfo.CharId = charId;
            float manaCost = GetManaCost();
            bool suc = ScriptSystem.Instance.Mana >= manaCost;
            if(suc)
            {
                ScriptSystem.Instance.SubMana((int)manaCost);
                LogHelperLSK.Log("训练角色扣除魔力Id:  " + manaCost);
            }
            traniCharInfo.SubManaSuc = suc;
            traniCharInfo.HasPause = false;
            BarrackSystem.Instance.AddCharTrain(traniCharInfo);

            CharSystem.Instance.SetCharSate(charId,CharStatus.Train);

            if(TrainCharChange != null)
            {
                TrainCharChange(traniCharInfo,1);
            }
            LogHelperLSK.Log("新增训练角色，角色Id:  " + charId);
        }

        public void CancelTrain(int charId)
        {
            TraniCharInfo traniCharInfo = BarrackSystem.Instance.GetTraniCharInfo(charId);
            if(traniCharInfo != null)
            {
                BarrackSystem.Instance.RemoveCharTrain(charId);
                CharSystem.Instance.SetCharSate(charId,CharStatus.Idle);

                if(TrainCharChange != null)
                {
                    TrainCharChange(traniCharInfo,4);
                }
                LogHelperLSK.Log("取消训练角色，角色Id:  " + charId);

            }
        }

        private Core_lvup GetCore_Lvup()
        {
            int coreLevel = CoreSystem.Instance.GetLevel();
            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);
            return core_Lvup;
        }

        public float GetManaCost()
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            return BuildingAttribute.Building.GetUnitManaCost(hR_Config);
        }


        public int GetNextLevelExp(CharAttribute attr)
        {
            Char_lvup char_Levelup = Char_lvupConfig.GetChar_Lvup(attr.charLevel);
            return (int)char_Levelup.levelupExp;
        }

        public int GetMaxTrainLevel()
        {
            //  return 12;
            Core_lvup core_Lvup = GetCore_Lvup();
            return core_Lvup.trainningMaxLevel;
        }

        public bool CanUseToken()
        {
            // return true;
            Token_cost token_Cost = Token_costConfig.GetToken_Cost();
            return PlayerSystem.Instance.Token >= token_Cost.instantTraining;
        }

        public void UseTokenToTrain(int charId)
        {
            Token_cost token_Cost = Token_costConfig.GetToken_Cost();
            PlayerSystem.Instance.SubToken(token_Cost.instantTraining);
            CharAttribute attr = CharSystem.Instance.GetAttribute(charId);
            attr.SetCharExp(token_Cost.tokenExpReward);

            if(ExpChange != null)
            {
                ExpChange(charId);
            }
            Core_lvup core_Lvup = GetCore_Lvup();
            CheckArriveMaxLevel(attr.charID,attr.charLevel,core_Lvup.trainningMaxLevel);
        }

        public int GetMaxTrainChar()
        {
            // Core_lvup core_Lvup = GetCore_Lvup();
            return 20;
        }

        public int GetNowTrainChar()
        {
            return BarrackSystem.Instance.GetTrainChars().Count;
        }
    }
}
