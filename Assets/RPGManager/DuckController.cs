using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class DuckController
    {
        private Queue<ICommand> CommandQueue;
        private bool _isStart = false;
        public bool isStart
        {
            get
            {
                return _isStart;
            }
        }
        public bool isWait
        {
            get
            {
                return CommandQueue.Count == 0;
            }
        }
        private bool _isTerminate;
        public bool isTerminate
        {
            get
            {
                return _isTerminate;
            }
        }
        public DuckController() { }

        public DuckController(ICommand command)
        {
            this.Start(command);
        }

        public void Start(ICommand command)
        {
            if (isStart)
            {
                throw new System.Exception("[DuckController]has been start");
            }
            if (isTerminate)
            {
                throw new System.Exception("[DuckController]has been sterminated");
            }
            _isStart = true;
            CommandQueue = new Queue<ICommand>();
            CommandQueue.Enqueue(command);
        }

        public void Update()
        {
            if (isStart && !isTerminate && !isWait)
            {
                var command = CommandQueue.Peek();
                if (!command.isBegin)
                {
                    command.TouchCommandBegin();
                    command.Begin();
                }
                if (command.isExecutable)
                {
                    command.Update();
                }
                else
                {
                    if (!command.isTerminated)
                    {
                        command.TouchCommandTerminated();
                        command.End();
                    }
                    CommandQueue.Dequeue();
                }
            }
        }




    }
}