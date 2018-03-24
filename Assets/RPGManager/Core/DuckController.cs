using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class DuckController
    {
        private MissionQueue<BaseMission> missionQueue = new MissionQueue<BaseMission>();
        private bool _isStart = false;
        private object _target;
        public object target
        {
            get
            {
                return _target;
            }
        }
        public bool isStart
        {
            get
            {
                return _isStart;
            }
        }
        public bool isIdle
        {
            get
            {
                return isStart && missionQueue.Count == 0;
            }
        }
        private bool _isPause = false;
        public bool isPause
        {
            get
            {
                return _isPause;
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

        public DuckController(object target)
        {
            this.Start(target);
        }

        public void Start(object target)
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
            this._target = target;
        }

        public void Update()
        {
            LifeCheck();
            if (isStart && !isPause && !isTerminate && !isIdle)
            {
                var command = missionQueue.Peek();
                if (!command.isBegin)
                {
                    command.TouchCommandBegin();
                    command.Begin(target);
                }
                if (command.isExecutable)
                {
                    command.Update(target);
                }
                else
                {
                    EndCurrentMission();
                }
            }
        }

        public void AddMission(params BaseMission[] commands)
        {
            if (isTerminate)
            {
                throw new System.Exception("[DuckController]has been terminated.");
            }
            for (int i = 0; i < commands.Length; i++)
            {
                missionQueue.Enqueue(commands[i]);
            }
        }

        /// <summary>
        /// 插入新任務會導致目前任務立即終止
        /// </summary>
        /// <param name="isComplete">如果為真，終止時立即完成任務</param>
        /// <param name="commands"></param>
        public void InsertMission(bool isComplete, params BaseMission[] commands)
        {
            LifeCheck();
            missionQueue.Insert(commands);
            EndCurrentMission(isComplete);
        }

        public void Pause()
        {
            this._isPause = true;
        }

        public void Resume()
        {
            this._isPause = false;
        }

        public void ClearMission(bool isComplete = false)
        {
            LifeCheck();
            EndCurrentMission(isComplete);
            this.missionQueue.Clear();
        }

        public BaseMission[] GetAllMission()
        {
            return missionQueue.ToArray();
        }

        public void Terminate(bool isComplete = false)
        {
            LifeCheck();
            this._isTerminate = true;
            EndCurrentMission(isComplete);
            this.missionQueue.Clear();
        }

        public void EndCurrentMission(bool isComplete = false)
        {
            if (!isIdle)
            {
                if (!missionQueue.Peek().isBegin)
                    missionQueue.Peek().Begin(target);
                if (isComplete)
                    missionQueue.Peek().Complete(target);
                missionQueue.Dequeue().End(target);
            }
        }

        private void LifeCheck()
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