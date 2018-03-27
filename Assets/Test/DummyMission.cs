using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class DummyMission : BaseMission
    {
        public int targetUpdate;
        public int updateCount;
        public DummyMission(int targetUpdate)
        {
            this.targetUpdate = targetUpdate;
            this.updateCount = 0;
        }
        public override void StateUpdate()
        {
            TransToNext();
            updateCount++;
            if (updateCount >= targetUpdate)
            {
                TransToNext();
            }

        }
    }
}