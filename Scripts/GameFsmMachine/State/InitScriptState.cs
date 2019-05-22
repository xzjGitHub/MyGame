﻿using EventCenter;
using GameFSM;

namespace GameFsmMachine
{
    /// <summary>
    ///  初始化剧本信息
    /// </summary>
    public class InitScriptState: FSMState
    {
        public InitScriptState(string name) : base(name){ }

        public override void OnEnter(IState preState)
        {
            base.OnEnter(preState);
            InitData();
        }

        public override void OnExit(IState nextState)
        {
            base.OnExit(nextState);
        }

        private void InitData()
        {
            ControllerCenter.Instance.Init();
            Script_template scriptTemplate = Script_templateConfig.GetAll()[0];
            ScriptController.InitLevelData(scriptTemplate.templateID);
            ControllerCenter.Instance.Initialize();
            ScriptTimeSystem.Instance.StartTiming();

            if(ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front)
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.FrontFight);
            else
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.MainScene);
        }
    }
}
