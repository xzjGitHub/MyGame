using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipTipAttrInfo: MonoBehaviour
{
    private GameObject m_prefab;
    private GameObject m_linePrefab;

    private VerticalLayoutGroup m_verticalLayoutGroup;
    private ContentSizeFitter m_contentSizeFitter;

    private CoroutineUtil m_coroutine;

    private bool m_hasInit;

    private void InitCompont()
    {
        m_prefab = transform.Find("Prefab").gameObject;
        m_prefab.SetActive(false);
        m_verticalLayoutGroup = gameObject.GetComponent<VerticalLayoutGroup>();
        m_contentSizeFitter = gameObject.GetComponent<ContentSizeFitter>();
    }


    public void Init(EquipAttribute equipAttribute,List<AtrDesInfo> list)
    {
        if(!m_hasInit)
        {
            InitCompont();
            m_hasInit = true;
        }

        Init(list,transform);

        UpdatePosInfo();
    }

    private void Init(List<AtrDesInfo> list,Transform parent)
    {
        GameObject temp = null;
        for(int i = 0; i < list.Count; i++)
        {
            temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.EquipTipAttrItem,m_prefab);
            Utility.SetParent(temp,parent);
            temp.GetComponent<Text>().text = list[i].Des;
        }
    }

    private void UpdatePosInfo()
    {
        if(m_coroutine != null)
        {
            if(m_coroutine.Running)
            {
                m_coroutine.Stop();
            }
        }
        m_coroutine = new CoroutineUtil(UpdatePos(),false);
        m_coroutine.Start();
    }

    private IEnumerator UpdatePos()
    {
        m_verticalLayoutGroup.enabled = false;
        yield return null;
        m_verticalLayoutGroup.enabled = true;
        yield return null;

        m_contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        yield return null;
        m_contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public void Free()
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipTipAttrItem);
    }
}
