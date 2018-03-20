using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class DuckController
    {
        private CommandQueue<ICommand> commandQueue;
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
                return commandQueue.Count == 0;
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
                throw new System.Exception("[DuckController]has been terminated");
            }
            _isStart = true;
            commandQueue = new CommandQueue<ICommand>();
            commandQueue.Enqueue(command);
        }

        public void Update()
        {
            if (isStart && !isTerminate && !isWait)
            {
                var command = commandQueue.Peek();
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
                    commandQueue.Dequeue();
                }
            }
        }
        
        public void AddCommand(ICommand command)
        {
            StateCheck();
            commandQueue.Enqueue(command);
        }

        public void InsertCommand(params ICommand[] command)
        {
            StateCheck();
            commandQueue.Insert(command);
        }
        private void StateCheck()
        {
            if (!isStart)
            {
                throw new System.Exception("[DuckCountroller]have to start first.");
            }
            if (isTerminate)
            {
                throw new System.Exception("[DuckController]has been terminated.");
            }
        }
    }
}