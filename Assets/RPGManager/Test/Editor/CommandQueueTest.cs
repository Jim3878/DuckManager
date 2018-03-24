using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DuckGame;

public class CommandQueueTest {

    [Test]
    public void InsertQueue_InsertWhenCountIsZero_EnqueueItemsAll()
    {
        MissionQueue<int> q = new MissionQueue<int>();
        int arrLength=10;
        int[] insertArr = new int[arrLength];
        for(int i = 0; i < arrLength; i++)
        {
            insertArr[i] = i;
        }

        q.Insert(insertArr);
        
        for(int i = 0; i < arrLength; i++)
        {
            Assert.AreEqual(i, q.Dequeue());
        }
    }

	[Test]
	public void InsertQueue_InsertItems_thisFirsItemWontChange() {
        // Use the Assert class to test conditions.
        MissionQueue<int> q = new MissionQueue<int>();
        int[] insertArr = new int[10];
        for(int i = 0; i < 10; i++)
        {
            q.Enqueue(i);
        }
        for(int i = 10; i < 20; i++)
        {
            insertArr[i-10] = i;
        }

        q.Insert(insertArr);

        Assert.AreEqual(0, q.Dequeue());
        for(int i = 10; i < 20; i++)
        {
            Assert.AreEqual(i, q.Dequeue());
        }
        for(int i = 1; i < 10; i++)
        {
            Assert.AreEqual(i, q.Dequeue());
        }
	}
}
