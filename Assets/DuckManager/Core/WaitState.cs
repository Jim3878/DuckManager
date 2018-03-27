using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class WaitState : BaseMission
    {

        public WaitState( DuckController duck)
        {
            this.SetProperty( duck);
        }

        public override void StateUpdate()
        {
            if (duck.missionQueue.Count != 0)
            {
                TransTo(duck.missionQueue.Dequeue());
            }
        }
    }
}