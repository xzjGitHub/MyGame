using System.Collections.Generic;

namespace Barrack.Data
{
    /// <summary>
    /// 兵营系统
    /// </summary>
    public class BarrackSystem: Building
    {
        private const string m_barrackPath = "Barrack";

        public static BarrackSystem Instance { get; private set; }

        private BarrackData m_barrackData;

        public BarrackSystem()
        {
            Instance = this;
        }

        public override void Init()
        {
            if(m_barrackData == null) m_barrackData = new BarrackData();
        }

        /// <summary>
        /// 读取存档信息
        /// </summary>
        /// <param name="parentPath"></param>
        public override void ReadData(string parentPath)
        {
            this.parentPath = parentPath;
            m_barrackData = GameDataManager.ReadData<BarrackData>(parentPath + m_barrackPath) as BarrackData;
        }

        public override void SaveData(string parentPath)
        {
            GameDataManager.SaveData(parentPath,m_barrackPath,m_barrackData);
        }


        #region 兵营管理 人员分配

        public void UpdateCharInfo(int charId,bool canWork)
        {
            WorkCharInfo workCharInfo = GetWorkCharInfo(charId);
            workCharInfo.CanWork = canWork;
        }

        public List<WorkCharInfo> GetAllBarrackChar()
        {
            return m_barrackData.m_charUse;
        }

        public WorkCharInfo GetWorkCharInfo(int charId)
        {
            for(int i = 0; i < m_barrackData.m_charUse.Count; i++)
            {
                if(m_barrackData.m_charUse[i].CharId == charId)
                {
                    return m_barrackData.m_charUse[i];
                }
            }
            return null;
        }

        public void AddCharToUse(WorkCharInfo workCharInfo)
        {
            m_barrackData.m_charUse.Add(workCharInfo);
        }

        public void RemoveCharFromUse(int charId)
        {
            WorkCharInfo workCharInfo = GetWorkCharInfo(charId);
            m_barrackData.m_charUse.Remove(workCharInfo);
        }

        public bool HavIn(int charId)
        {
            WorkCharInfo workCharInfo = GetWorkCharInfo(charId);
            return workCharInfo != null;
        }

        public int GetCurrentWorkNum()
        {
            return m_barrackData.m_charUse.Count;
        }

        public List<WorkCharInfo> GetCharStatusList(CharStatus status)
        {
            List<WorkCharInfo> list = new List<WorkCharInfo>();
            foreach(var item in m_barrackData.m_charUse)
            {
                if(item.Status == status)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetUserTypeCharCount(CharStatus status)
        {
            return GetCharStatusList(status).Count;
        }

        public int GetNowUserTypeCharCount(CharStatus status)
        {
            int count = 0;
            foreach(var item in m_barrackData.m_charUse)
            {
                if(item.Status == status && item.CanWork)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetAllCount()
        {
            return m_barrackData.m_charUse.Count;
        }

        public bool CanWork(int charId)
        {
            WorkCharInfo workCharInfo = GetWorkCharInfo(charId);
            if(workCharInfo != null)
                return workCharInfo.CanWork;
            return true;
        }

        #endregion

        #region 训练

        public List<TraniCharInfo> GetTrainChars()
        {
            return m_barrackData.TrainChars;
        }

        public bool IsTraining(int charId)
        {
            return GetTraniCharInfo(charId) != null;
        }

        public void AddCharTrain(TraniCharInfo traniCharInfo)
        {
            m_barrackData.TrainChars.Add(traniCharInfo);
        }

        public void RemoveCharTrain(int charId)
        {
            TraniCharInfo trainingCharInfo = GetTraniCharInfo(charId);
            m_barrackData.TrainChars.Remove(trainingCharInfo);
        }

        public TraniCharInfo GetTraniCharInfo(int charId)
        {
            for(int i = 0; i < m_barrackData.TrainChars.Count; i++)
            {
                if(m_barrackData.TrainChars[i].CharId == charId)
                {
                    return m_barrackData.TrainChars[i];
                }
            }
            return null;
        }

        public void AddTrainsTime(int time)
        {
            for(int i = 0; i < m_barrackData.TrainChars.Count; i++)
            {
                m_barrackData.TrainChars[i].HaveUseTime += time;
            }
        }

        #endregion

        #region 复活
        public bool HasFuHo(int id)
        {
            return m_barrackData.HaveFuHuoId.Contains(id);
        }

        public void AddToFuHuoList(int id)
        {
            if(!m_barrackData.HaveFuHuoId.Contains(id))
            {
                m_barrackData.HaveFuHuoId.Add(id);
            }
        }
        #endregion
    }
}

