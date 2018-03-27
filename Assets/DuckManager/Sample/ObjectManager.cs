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
    public Dictionary<object, DuckController> ctrlList;
    float time;

    // Use this for initialization
    void Start()
    {
        ctrlList = new Dictionary<object, DuckController>();
        CreatNewCube();
        time = Time.time;
    }

    void CreatNewCube()
    {
        GameObject go = Instantiate<GameObject>(cube.gameObject);
        go.transform.position = transform.position;

        var ctrl = new DuckController(go);
        ctrl.AddMission(new SimpleMove(go.transform, targetSphere.position, speed), new SimpleMove(go.transform, go.transform.position, speed), new Terminate());
        ctrlList.Add(go.transform, ctrl);
    }

    // Update is called once per frame
    KeyValuePair<object,DuckController>[] GetCtrlListClone()
    {
        var ctrlListClone = new KeyValuePair<object, DuckController>[ctrlList.Count];
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
            if (ctrl.Value.isTermanated)
            {
                ctrlList.Remove(ctrl.Key);
            }
            else
            {
                ctrl.Value.Update();
            }
        }
    }
}
