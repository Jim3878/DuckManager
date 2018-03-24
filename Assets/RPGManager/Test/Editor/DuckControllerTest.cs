using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DuckGame;
using System;

public class DuckControllerTest
{
    [Test]
    public void Complete_BaseTest()
    {
        DuckController duck = new DuckController(0);
        int missionTarget = 5;
        DummyMission msn = new DummyMission(missionTarget);
        duck.AddMission(msn);

        duck.Update();
        duck.EndCurrentMission(true);

        Assert.AreEqual(missionTarget, msn.UpdataCount);
    }

    [Test]
    public void EndMission_WithoutAnyUpdate_WillBeginTheEnd()
    {
        DuckController duck = new DuckController(0);
        int missionTarget = 5;
        DummyMission msn = new DummyMission(missionTarget);
        duck.AddMission(msn);
        
        duck.EndCurrentMission(true);

        Assert.AreEqual(1, msn.BeginCount);
    }

    [Test]
    public void BasicFlowTest()
    {
        DuckController duck = new DuckController(0);
        int updateTarget = 5;
        DummyMission msn = new DummyMission(updateTarget);

        Assert.IsTrue(duck.isIdle);//沒有任務時回傳True

        duck.AddMission(msn);
        
        for (int i = 0; i < 100; i++)
        {
            if (i < 6)
            {
                Assert.IsFalse(duck.isIdle);//任務在Update完成後還需要一次update來關閉
            }
            duck.Update();
        }

        Assert.IsTrue(duck.isIdle);
        Assert.AreEqual(1, msn.BeginCount);
        Assert.AreEqual(updateTarget, msn.UpdataCount);
        Assert.AreEqual(1, msn.EndCount);
    }

    [Test]
    public void InsertCommand_CanTransToNextCommandNextUpdate()
    {
        DuckController duck = new DuckController(0);
        int cmd_Target = 5;
        DummyMission cmd1 = new DummyMission(cmd_Target);
        DummyMission cmd2 = new DummyMission(cmd_Target);
        DummyMission cmd3 = new DummyMission(cmd_Target);
        duck.AddMission(cmd1);

        duck.Update();
        duck.Update();
        duck.Update();
        duck.InsertMission(false, cmd2, cmd3);
        duck.Update();
        duck.Update();
        duck.Update();
        duck.Update();

        Assert.AreEqual(3, cmd1.UpdataCount);
        Assert.AreEqual(4, cmd2.UpdataCount);
        Assert.AreEqual(0, cmd3.UpdataCount);
    }

    [Test]
    public void Update_FlowTest_UpdateWillTransToNextWhenCurrentCmdCantExcutable()
    {
        DuckController duck = new DuckController(0);
        DummyMission[] cmdArr = new DummyMission[5];
        for (int i = 0; i < cmdArr.Length; i++)
        {
            cmdArr[i] = new DummyMission(i);
        }
        duck.AddMission(cmdArr);

        for (int i = 0; i < cmdArr.Length; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                duck.Update();
            }

            //arrage
            Assert.AreEqual(1, cmdArr[i].BeginCount);
            Assert.AreEqual(i, cmdArr[i].UpdataCount);
            Assert.AreEqual(1, cmdArr[i].EndCount);
        }
    }

    [Test]
    public void UpdateWithOutStart_ThrowException()
    {
        DuckController duck = new DuckController();

        Assert.Throws<Exception>(() => duck.Update());
    }

}
