using System;
using System.Collections;
using System.Collections.Generic;
using Altar.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Altar.View
{
    public class CallPlayerList: MonoBehaviour
    {
        private Transform m_parent;
        private GameObject m_prefab;
        private GameObject m_lastObj;

        private Dictionary<string,GameObject> m_dict = new Dictionary<string,GameObject>();
        private Action<string,CharAttribute> m_clickAction;

        private CoroutineUtil m_coroutine;

        private void OnDisable()
        {
            StopCortine();
        }

        private void StopCortine()
        {
            if(m_coroutine != null)
            {
                if(m_coroutine.Running)
                {
                    m_coroutine.Stop();
                }
            }
        }

        public void InitComponent()
        {
            m_parent = transform.Find("CharList/Grid");
            m_prefab = transform.Find("CharList/Grid/CharItem").gameObject;
            m_prefab.SetActive(false);
        }

        public void Init(Dictionary<string,CharAttribute> allInfo,Action<string,CharAttribute> action)
        {
            m_clickAction = action;
            m_lastObj = null;
            m_dict.Clear();

            StopCortine();
            m_coroutine = new CoroutineUtil(InitInfo(allInfo));
        }


        public IEnumerator InitInfo(Dictionary<string,CharAttribute> allInfo)
        {

            Summon_cost summon_Cost = null;

            bool hasClick = false;

            foreach(var item in allInfo)
            {
                GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.NewCallItem,
                    m_prefab);
                temp.name = item.Key;

                string uid = item.Key;
                Utility.SetParent(temp,m_parent);
                m_dict[item.Key] = temp;

                CharAttribute attribute = item.Value;

                temp.transform.Find("Select").gameObject.SetActive(false);
                temp.transform.Find("Info/Icon").GetComponent<Image>().sprite =
                    ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,attribute.char_template.HeadIcon);
                temp.transform.Find("Info/Level").GetComponent<Text>().text = "Lv." + attribute.charLevel;

                bool hasCall = AltarSystem.Instance.HasCall(uid);
                temp.transform.Find("NotCall").gameObject.SetActive(!hasCall);
                temp.transform.Find("HaveCall").gameObject.SetActive(hasCall);
                if(!hasCall)
                {
                    temp.transform.Find("NotCall/PJ/Image").GetComponent<Image>().sprite
                 = CharaterUti.GetRankSprite((int)item.Value.CharRank);
                    temp.transform.Find("NotCall/Name").GetComponent<Text>().text = attribute.CharName;
                    summon_Cost = Summon_costConfig.GetSummon_Cost(item.Value.char_template.templateID);
                    temp.transform.Find("NotCall/GoldCost/Num").GetComponent<Text>().text =
                       summon_Cost!=null? summon_Cost.goldCost.ToString():"";
                    temp.transform.Find("NotCall/ManaCost/Num").GetComponent<Text>().text =
                        summon_Cost != null ? summon_Cost.manaCost.ToString():"";
                }

                Utility.AddButtonListener(temp.transform.Find("Btn"),() => ClickPlayer(uid,attribute,temp));

                if(!hasClick)
                {
                    ClickPlayer(uid,item.Value,temp);
                    hasClick = true;
                }

                yield return null;
            }

        }


        private void ClickPlayer(string uid,CharAttribute charAttribute,GameObject obj)
        {
            if(obj == m_lastObj)
            {
                return;
            }
            if(m_lastObj != null)
            {
                m_lastObj.transform.Find("Select").gameObject.SetActive(false);
            }
            m_lastObj = obj;
            m_lastObj.transform.Find("Select").gameObject.SetActive(true);

            if(m_clickAction != null)
            {
                m_clickAction(uid,charAttribute);
            }
        }

        public void UpdateWhenCallEnd(string uid)
        {
            m_dict[uid].transform.Find("NotCall").gameObject.SetActive(false);
            m_dict[uid].transform.Find("HaveCall").gameObject.SetActive(true);
        }


        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.NewCallItem);
        }
    }
}

