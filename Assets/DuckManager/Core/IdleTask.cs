using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class IdleTask : ITask
    {
        public override void StateUpdate()
        {
            if (GetTaskCount() != 0)
            {
                TransToNextTask();
            }
        }

        public override void StateEnd()
        {
            base.StateEnd();
            base.BackToBeginState();
        }
    }
}