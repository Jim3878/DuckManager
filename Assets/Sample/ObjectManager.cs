using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckManager;

public class ObjectManager : MonoBehaviour
{

    public Transform cube;
    public Transform targetSphere;
    public float deltaTime = 5f;
    public float speed = 2f;
    public Dictionary<object, TaskController> ctrlList;
    float time;

    // Use this for initialization
    void Start()
    {
        ctrlList = new Dictionary<object, TaskController>();
        CreatNewCube();
        time = Time.time;
    }

    void CreatNewCube()
    {
        GameObject go = Instantiate<GameObject>(cube.gameObject);
        go.transform.position = transform.position;

        var ctrl = new TaskController(new IdleTask());
        ctrl.AddTask(new SimpleMove(go.transform, targetSphere.position, speed), new SimpleMove(go.transform, go.transform.position, speed), new Terminate());
        ctrlList.Add(go, ctrl);
    }

    // Update is called once per frame
    KeyValuePair<object,TaskController>[] GetCtrlListClone()
    {
        var ctrlListClone = new KeyValuePair<object, TaskController>[ctrlList.Count];
        int index = 0;
        foreach (var ctrl in ctrlList)
        {
            ctrlListClone[index] = ctrl;
            index++;
        }
        return ctrlListClone;
    }
    
   

    void Update()
    {
        if (Time.time - time > deltaTime)
        {
            time = Time.time;
            CreatNewCube();
        }

        foreach (var ctrl in GetCtrlListClone())
        {
            if (ctrl.Value.currentState is Terminate)
            {
                ctrlList.Remove(ctrl.Key);
                Destroy(ctrl.Key as GameObject);
            }
            else
            {
                ctrl.Value.StateUpdate();
            }
        }
    }
}
