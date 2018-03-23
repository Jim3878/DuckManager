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
    public void Update_FlowTest()
    {
        DuckController duck = new DuckController();
        int updateTarget = 5;
        DummyCommand cmd = new DummyCommand(updateTarget);
        duck.Start(cmd);
        int i;

        for (i = 0; i < 100; i++)
        {
            duck.Update();
        }

        Assert.IsTrue(duck.isWait);
        Assert.AreEqual(1, cmd.BeginCount);
        Assert.AreEqual(updateTarget, cmd.UpdataCount);
        Assert.AreEqual(1, cmd.EndCount);
    }

    [Test]
    public void InsertCommand_CanTransToNextCommandNextUpdate()
    {
        DuckController duck = new DuckController();
        int cmd_Target = 5;
        DummyCommand cmd1 = new DummyCommand(cmd_Target);
        DummyCommand cmd2 = new DummyCommand(cmd_Target);
        DummyCommand cmd3 = new DummyCommand(cmd_Target);
        duck.Start(cmd1);

        duck.Update();
        duck.Update();
        duck.Update();
        duck.InsertCommand(cmd2, cmd3);
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
        DuckController duck = new DuckController();
        DummyCommand[] cmdArr = new DummyCommand[5];
        for (int i = 0; i < cmdArr.Length; i++)
        {
            cmdArr[i] = new DummyCommand(i);
        }
        duck.AddCommand(cmdArr);

        for (int i = 0; i < cmdArr.Length; i++)
        {
            for (int j = 0; j < i+1; j++)
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
