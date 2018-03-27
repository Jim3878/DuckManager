using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class Terminate : BaseMission
    {
        public override void StateUpdate()
        {
            duck.Terminate();
        }
    }
}
