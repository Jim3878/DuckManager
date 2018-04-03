using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DuckManager;
public class DuckControllerTest {
    

    [Test]
	public void WhenMissionQueeIsEmpty_WillTransToWaitState() {
        // Use the Assert class to test conditions.
        TaskController duck = new TaskController(new IdleTask());

        Assert.IsTrue(duck.currentState is IdleTask);//初始化後預設狀態為waitState;

        duck.AddTask(new DummyMission(0));
        duck.StateUpdate();//退出Wait
        duck.StateUpdate();//退出dummyMission

        Debug.Log(duck.currentState);
        Assert.IsTrue(duck.currentState is IdleTask);//dummyMission結束後轉入 waitState;
    }
    
}
