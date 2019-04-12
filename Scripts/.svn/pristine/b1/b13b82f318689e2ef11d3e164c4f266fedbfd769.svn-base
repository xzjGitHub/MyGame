using GameEventDispose;
using UnityEngine;

public class UICombatUnitManager : UICharBase
{
    public bool IsPlayCharUpgradeShowOk { get { return upgradeShow.IsOk; } }

    public bool IsFade { get { return actionOperation.IsFade; } }

    public int WeaponType { get { return weaponType; } }


    /// <summary>
    /// 初始化
    /// </summary>
    public void NewInit(CombatUnit _combatUnit, bool isNew = true)
    {
        combatUnit = _combatUnit;
        base.InitInfo(_combatUnit);
        //
        NewGetObj();
        //得到装备类型
        EquipAttribute equip = _combatUnit.charAttribute.equipAttribute.Find(a => a.itemCategory == 1 && a.itemType == 1);
        weaponType = equip != null
            ? equip.equipRnd.equip_template.equipType
            : Equip_templateConfig.GetEquip_template(_combatUnit.charAttribute.char_template.defaultWeapon).equipType;
        //得到武器Index
        weaponIndex = 0;
        //for(int i = 0; i < combatUnit.charAttribute.char_template.weaponType.Count; i++)
        //{
        //    if(combatUnit.charAttribute.char_template.weaponType[i] != weaponType) continue;
        //    weaponIndex = i;
        //    break;
        //}
        //
        // HpShow.gameObject.SetActive(false);
        //
        //清除已加载资源
        if (isNew)
        {
            ResourceLoadUtil.DeleteChildObj(transform.Find("Move"));
            ResourceLoadUtil.DeleteChildObj(transform.Find("Fixed"));
            actionOperation = gameObject.AddComponent<UICharActionOperation>();
            //actionOperation.InitAdd(combatUnit);
            gameObject.AddComponent<UICharActionEventEffectInfo>().Init(combatUnit);
        }
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Init, combatUnit.teamId, combatUnit.charAttribute.charID,
          (object)new object[] { combatUnit, isNew });
    }
    /// <summary>
    /// 重置资源
    /// </summary>
    public void ResetRes()
    {
        ResourceLoadUtil.DeleteChildObj(transform.Find("Move"));
        ResourceLoadUtil.DeleteChildObj(transform.Find("Fixed"));
    }
    /// <summary>
    /// 关闭升级显示
    /// </summary>
    public void CloseUpgradShow()
    {
        upgradeShow.gameObject.SetActive(false);
    }

    /// <summary>
    /// 播放胜利动作
    /// </summary>
    private void PlayCelebrateAction(CombatPlayEventType _type, bool _isLoop = true)
    {
        //角色开始播放动作
        if (teamID != 0) return;
        switch (_type)
        {
            case CombatPlayEventType.PlayCelebrate:
                upgradeShow.Show(CharSystem.Instance.CharAddExp(charID, 10000));
                EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Action, combatUnit.teamId, combatUnit.charAttribute.charID,
                    (object)new object[] { CharModuleAction.Celebrate, true });
                //  PlayAction(CharModuleAction.Celebrate, _isLoop);
                return;
            case CombatPlayEventType.PlayRewards:
                upgradeShow.gameObject.SetActive(false);
                break;
        }
    }
    /// <summary>
    /// 得到组件
    /// </summary>
    private void NewGetObj()
    {
        if (isFirst) return;
        //添加组件
        //
        //
        GameObject _obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadHpShow());
        ResourceLoadUtil.ObjSetParent(_obj, transform, transform.parent.localScale);
      //  _obj.AddComponent<UICharHpShow>().Init(combatUnit);
        _obj.SetActive(false);
        //
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null) cameraShake = Camera.main.gameObject.AddComponent<CameraShake>();
        //
        if (transform.parent.name == "Left")
        {
            upgradeShow = transform.Find("CharUpgradeRewards").gameObject.AddComponent<UICharUpgradeShow>();
        }
        //
        isFirst = true;
    }


    //
    private UICharActionOperation actionOperation;
    private new CombatUnit combatUnit;
    //
    private int weaponType = 1;
    private int weaponIndex;
    //
    private UICharHpShow HpShow;
    private CameraShake cameraShake;
    private UICharUpgradeShow upgradeShow;
    //
    private bool isFirst;
}
