using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class DestoryMission : BaseMission
    {
        DuckController mCtrl;
        GameObject go;
        bool isExcute = false;

        public DestoryMission(DuckController ctrl, GameObject go)
        {
            this.mCtrl = ctrl;
            this.go = go;
        }

        public override void Begin()
        {
            isExcute = true;
        }

        public override void End()
        {
            GameObject.Destroy(go);
            mCtrl.Terminate();
        }

        public override bool isExecutable
        {
            get
            {
                return !isExcute;
            }
        }
    }
}