using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class NormalState : IState
    {
        private IMissionManager<BaseMission> missionManager;

        public NormalState( IMissionManager<BaseMission> missionManager)
        {
            this.missionManager = missionManager;
        }

        public override void StateUpdate()
        {
            if (missionManager.Count > 0)
            {
                var command = missionManager.PeekMission();
                if (!command.isBegin)
                {
                    command.TouchCommandBegin();
                    command.Begin();
                }
                if (command.isExecutable)
                {
                    command.Update();
                }
                else
                {
                    command.End();
                    missionManager.PopMission();
                }
            }
        }
    }
}
