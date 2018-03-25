using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DuckGame
{
    public class DummyMission : BaseMission
    {
       
        public int BeginCount=0;
        public int UpdataCount=0;
        public int EndCount=0;

        public int updateTarget;
        public DummyMission(int updateTarget)
        {
            this.updateTarget = updateTarget;
        }

        public override void Begin( )
        {
            BeginCount++;
        }

        public override void End( )
        {
            EndCount++;
        }

        public override void Complete( )
        {
            UpdataCount = updateTarget;
        }

        public override void Update()
        {
            UpdataCount++;
        }

        public override bool isExecutable
        {
            get
            {
                return updateTarget > UpdataCount;
            }
        }
    }

}