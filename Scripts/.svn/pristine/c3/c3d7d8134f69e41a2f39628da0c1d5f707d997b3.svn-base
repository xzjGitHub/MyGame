using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public class SkillInfo1: MonoBehaviour
    {
        private GameObject m_prefab;
        private Transform m_parent;

        public void InitComponent()
        {
            m_prefab = transform.Find("Grid/SkillItem1").gameObject;
            m_prefab.SetActive(false);
            m_parent = transform.Find("Grid");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skillIdList"></param>
        /// <param name="attr"></param>
        /// <param name="skillType">1 基本技能 2被动技能</param>
        public void UpdateInfo(List<int> skillIdList,CharAttribute attr,bool hasCall)
        {
            Free();
            for(int i = 0; i < skillIdList.Count; i++)
            {
                GameObject obj = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.SkillItem1,m_prefab);
                Utility.SetParent(obj,m_parent);

                int skillId = skillIdList[i];
                Combatskill_template skill = Combatskill_templateConfig.GetCombatskill_template(skillId);

                GameObject notCall = obj.transform.Find("NotHave").gameObject;
                notCall.SetActive(true);
                if(i==0) {
                    notCall.SetActive(!hasCall);
                }

                Image icon = obj.transform.Find("Icon").GetComponent<Image>();
                if(i == 0 && hasCall)
                {
                    icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.SkillIcon,skill.skillIcon);
                }
            }
        }

        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.SkillItem1);
        }
    }
}
