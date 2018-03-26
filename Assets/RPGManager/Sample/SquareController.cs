using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckGame;

public class SquareController : MonoBehaviour
{

    public Transform cube;
    public Transform targetSphere;
    public float deltaTime = 5f;
    public float speed = 2f;

    private List<DuckController> ctrlList;
    float time;

    // Use this for initialization
    void Start()
    {
        ctrlList = new List<DuckController>();
        CreatNewCube();
        time = Time.time;
    }

    void CreatNewCube()
    {
        GameObject go = Instantiate<GameObject>(cube.gameObject);
        go.transform.position = transform.position;

        var ctrl = new DuckController(go);
        ctrl.AddMission(new MoveMission(go.transform, targetSphere.position, speed), new MoveMission(go.transform, go.transform.position, speed), new DestoryMission(ctrl, go));
        ctrlList.Add(ctrl);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > deltaTime)
        {
            time = Time.time;
            CreatNewCube();
        }
        foreach(var ctrl in ctrlList.ToArray())
        {
            if (ctrl.isTerminate)
            {
                ctrlList.Remove(ctrl);
            }
            else
            {
                ctrl.Update();
            }
        }
    }
}
