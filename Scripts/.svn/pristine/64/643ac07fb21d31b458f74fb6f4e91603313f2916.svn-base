using System.Collections.Generic;

public interface IController
{
    void Initialize();
    void Uninitialize();
}

public class ControllerCenter: Singleton<ControllerCenter>
{
    private ControllerCenter()
    {
        
    }
    /// <summary>
    /// 商店
    /// </summary>
    public Shop.Controller.ShopController ShopController { get; private set; }
    /// <summary>
    /// 角色召唤
    /// </summary>
    public Altar.Controller.AltarController AltarController { get; private set; }
    /// <summary>
    /// 兵营
    /// </summary>
    public Barrack.Controller.BarrackController BarrackController { get; private set; }
    /// <summary>
    /// 附魔研究
    /// </summary>
    public College.Research.Controller.EnchanteResearchController EnchanteResearchController { get; private set; }
    /// <summary>
    /// 核心
    /// </summary>
    public Core.Controller.CoreController CoreController { get; private set; }
    /// <summary>
    /// 装备研究
    /// </summary>
    public WorkShop.EquipResearch.Controller.EquipResearchController EquipResearchController { get; private set; }
    /// <summary>
    /// 装备制造
    /// </summary>
    public WorkShop.EquipMake.Controller.EquipMakeController EquipMakeController { get; private set; }
    /// <summary>
    /// 装备附魔
    /// </summary>
    public College.Enchant.Controller.EquipEnchanteController EquipEnchanteController { get; private set; }
    /// <summary>
    /// 装备重铸
    /// </summary>
    public WorkShop.Recast.Controller.EquipRecastController EquipRecastController { get; private set; }

    private readonly List<IController> m_list = new List<IController>();

    public void Init()
    {
        CoreController = new Core.Controller.CoreController();
        m_list.Add(CoreController);

        AltarController = new Altar.Controller.AltarController();
        m_list.Add(AltarController);

        BarrackController = new Barrack.Controller.BarrackController();
        m_list.Add(BarrackController);

        EnchanteResearchController = new College.Research.Controller.EnchanteResearchController();
        m_list.Add(EnchanteResearchController);

        ShopController = new Shop.Controller.ShopController();
        m_list.Add(ShopController);

        EquipResearchController = new WorkShop.EquipResearch.Controller.EquipResearchController();
        m_list.Add(EquipResearchController);

        EquipMakeController = new WorkShop.EquipMake.Controller.EquipMakeController();
        m_list.Add(EquipMakeController);

        EquipEnchanteController=new College.Enchant.Controller.EquipEnchanteController();
        m_list.Add(EquipEnchanteController);

        EquipRecastController = new WorkShop.Recast.Controller.EquipRecastController();
        m_list.Add(EquipRecastController);
    }

    public void Initialize()
    {
        for(int i = 0; i < m_list.Count; i++)
        {
            m_list[i].Initialize();
        }
    }

    public void UnInitialize()
    {
        for(int i = 0; i < m_list.Count; i++)
        {
            m_list[i].Uninitialize();
            m_list[i] = null;
        }
        m_list.Clear();
    }
}
