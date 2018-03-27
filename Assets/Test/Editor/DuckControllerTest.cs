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
        DuckController duck = new DuckController(0);

        Assert.IsTrue(duck.currentState is WaitState);//初始化後預設狀態為waitState;

        duck.AddMission(new DummyMission(0));
        duck.Update();//退出Wait
        duck.Update();//退出dummyMission

        Assert.IsTrue(duck.currentState is WaitState);//dummyMission結束後轉入 waitState;
    }
    
}
