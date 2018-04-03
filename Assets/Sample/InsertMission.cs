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
        TaskController ctrl = manager.ctrlList[t.gameObject];
        ctrl.TransTo(new InsertScale(transform, ctrl.currentState));
    }

    class InsertScale : ITask
    {
        IState nextState;
        Transform target;
        public InsertScale(Transform target, IState nextState)
        {
            this.target = target;
            this.nextState = nextState;
        }
        public override void StateUpdate()
        {
            target.localScale += Vector3.one * 0.1f;
            if (target.localScale.magnitude >= (Vector3.one * 2f).magnitude)
            {
                TransTo(nextState);
            }
        }
    }
}
