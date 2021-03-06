﻿using GameFSM;

namespace GameFsmMachine
{
    public class ExitGameState: FSMState
    {
        public ExitGameState(string name) : base(name) { }

        public override void OnEnter(IState preState)
        {
            base.OnEnter(preState);
            if(ScriptSystem.Instance != null)
            {
                GameDataManager.SaveGameData();
            }
        }
    }
}
