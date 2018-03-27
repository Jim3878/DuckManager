using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckManager;

public class InsertMission : MonoBehaviour
{
    public Transform t;
    public ObjectManager manager;


    private void OnMouseUp()
    {
        Insert();
    }

    public void Insert()
    {
        Debug.Log("Click");
        DuckController ctrl = manager.ctrlList[t];
        ctrl.GlobalTransTo(new InsertScale(ctrl.currentState as BaseMission));
    }

    class InsertScale : BaseMission
    {
        BaseMission nextState;
        public InsertScale(BaseMission nextState)
        {
            this.nextState = nextState;
        }
        public override void StateUpdate()
        {
            var trans = (duck.target as GameObject).transform;
            trans.localScale += Vector3.one * 0.1f;
            if (trans.localScale.magnitude >= (Vector3.one * 2f).magnitude)
            {
                TransTo(nextState);
            }
        }
    }
}
