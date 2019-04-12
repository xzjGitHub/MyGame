using System;
using GameEventDispose;
using Shop.Data;


namespace Shop.Controller
{

    public enum ShopType
    {
        None,
        NormalShop,
        BlackShop
    }

    public partial class ShopController: IController
    {

        public Action RefreshShop;
        public Action<int> RefreshTime;

        public void Initialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        private void AddResfreshTime()
        {
            MerchantSystem.Instance.AddTime(TimeUtil.DaySeconds);
        }

        private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
        {
            if(arg1 == ScriptTimeUpdateType.Second)
            {
                MerchantSystem.Instance.AddTime();
                if(MerchantSystem.Instance.GetTime() >= TimeUtil.WeekSeconds)
                {
                    UpdateProductionCycle();
                    MerchantSystem.Instance.ResetTime();
                }
            }
            //todo
            if(arg1 == ScriptTimeUpdateType.Day)
            {
                if(RefreshTime != null)
                {
                    RefreshTime(TimeUtil.GetNextWeekNeedDays());
                }
            }
        }

        public void UpdateProductionCycle()
        {
            MerchantSystem.Instance.ClearShopGoods();
            UpdateNormalGoods();
            UpdateBlackMarketGoods();
            if(RefreshShop != null)
            {
                RefreshShop();
            }
        }
    }
}
