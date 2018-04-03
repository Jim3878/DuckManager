using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class TaskController : StateController
    {
        public bool _isCompleteMode;
        private Queue<ITask> taskQueue;
        public int taskCount
        {
            get
            {
                return taskQueue.Count;
            }
        }
        public IdleTask idleTask;
        public bool isTerminated
        {
            get
            {
                return m_bTerminated;
            }
        }
        public bool isStart
        {
            get
            {
                return m_bStarted;
            }
        }

        public TaskController(IdleTask idleTask)
        {
            taskQueue = new Queue<ITask>();
            this.idleTask = idleTask;
            Start(idleTask);
        }

        public void AddTask(params ITask[] missions)
        {
            for (int i = 0; i < missions.Length; i++)
            {
                missions[i].SetProperty(this);
                taskQueue.Enqueue(missions[i]);
            }
        }

        public void ClearTask()
        {
            taskQueue.Clear();
        }

        public void TransToIdle()
        {
            TransTo(idleTask);
        }

        public void TransToNextTask()
        {
            if (taskQueue.Count != 0)
            {
                TransTo(taskQueue.Dequeue());
            }
            else
            {
                this.TransToIdle();
            }
        }

    }
}