
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/29/2019
//Note:     
//--------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Guide
{
    /// <summary>
    /// 
    /// </summary>
    public class GuideController: IController
    {
        //引导过程 任务ID
        private Dictionary<GuideStep,int> m_taskRe = new Dictionary<GuideStep,int>();

        public void Initialize()
        {
#if UNITY_EDITOR
            GuideSys.Instance.SetAllHaveEnd(!Game.Instance.GameSetting.ShowGuide);
#endif
            Script_storyLine ss = Script_storyLineConfig.GetScByUid((int)GuideStep.EmptyCity);
            m_taskRe[GuideStep.EmptyCity] = ss.bountyReq;

            ss = Script_storyLineConfig.GetScByUid((int)GuideStep.Core);
            m_taskRe[GuideStep.Core] = ss.bountyReq;

            ss = Script_storyLineConfig.GetScByUid((int)GuideStep.Hall);
            m_taskRe[GuideStep.Hall] = ss.bountyReq;

            ss = Script_storyLineConfig.GetScByUid((int)GuideStep.Barrack);
            m_taskRe[GuideStep.Barrack] = ss.bountyReq;

            //ss = Script_storyLineConfig.GetScByUid((int)GuideStep.WorkShop);
            //m_taskRe[GuideStep.WorkShop] = ss.bountyReq;

            //ss = Script_storyLineConfig.GetScByUid((int)GuideStep.Shop);
            //m_taskRe[GuideStep.Shop] = ss.bountyReq;
        }

        public void Uninitialize() { }

        private bool HaveEndTask(int id)
        {
            return true;
            // return BountySystem.Instance.MainFinisheds.Contains(id);
        }

        public GuideStep GetCurrentStep()
        {
            if(GuideSys.Instance.GetAllHaveEndGuide())
            {
                return GuideStep.None;
            }
            if(!GuideSys.Instance.HaveEnd(GuideStep.EmptyCity) &&
               HaveEndTask(m_taskRe[GuideStep.EmptyCity]))
            {
                return GuideStep.EmptyCity;
            }
            else if(!GuideSys.Instance.HaveEnd(GuideStep.Core) && GuideSys.Instance.HaveEnd(GuideStep.EmptyCity)
                && HaveEndTask(m_taskRe[GuideStep.Core]))
            {
                return GuideStep.Core;
            }
            else if(!GuideSys.Instance.HaveEnd(GuideStep.Hall) && GuideSys.Instance.HaveEnd(GuideStep.Core) &&
                 HaveEndTask(m_taskRe[GuideStep.Hall]))
            {
                return GuideStep.Hall;
            }
            else if(!GuideSys.Instance.HaveEnd(GuideStep.Barrack) && GuideSys.Instance.HaveEnd(GuideStep.Hall) &&
                HaveEndTask(m_taskRe[GuideStep.Barrack]))
            {
                return GuideStep.Barrack;
            }
            //if(!GuideSys.Instance.HaveEnd(GuideStep.WorkShop) && GuideSys.Instance.HaveEnd(GuideStep.Barrack) &&
            //    HaveEndTask(m_taskRe[GuideStep.WorkShop]))
            //{
            //    return GuideStep.WorkShop;
            //}
            //if(!GuideSys.Instance.HaveEnd(GuideStep.Shop) && GuideSys.Instance.HaveEnd(GuideStep.WorkShop) &&
            //     HaveEndTask(m_taskRe[GuideStep.Shop]))
            //{
            //    return GuideStep.Shop;
            //}
            else
            {
                return GuideStep.None;
            }
        }

        public void SetHaveEnd(GuideStep step)
        {
            GuideSys.Instance.SetHaveEnd(step);
        }

        public bool HaveEndStep(GuideStep step)
        {
            if(GuideSys.Instance.GetAllHaveEndGuide())
                return true;
            return GuideSys.Instance.HaveEnd(step);
        }
    }
}