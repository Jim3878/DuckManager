using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class ITask : IState
    {
        //QueueuStateController _controller;
        private TaskController GetQueueStateController()
        {
            return controller as TaskController;
        }

        protected void TransToIdle()
        {
            GetQueueStateController().TransToIdle();
        }
        
        protected void TransToNextTask()
        {
            GetQueueStateController().TransToNextTask();
        }

        protected int GetTaskCount()
        {
            return GetQueueStateController().taskCount;
        }
    }
}