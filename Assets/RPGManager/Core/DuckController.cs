using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class DuckController
    {
        private IMissionManager<BaseMission> missionManager= new MissionManager<BaseMission>();
        
        private StateController stateController;
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
                return isStart && missionManager.Count == 0;
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
            //this.missionManager = new MissionManager<BaseMission>();
            Start(target, new NormalState(this.missionManager));
        }

        public void Start(object target,IState state)
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
            this.stateController = new StateController(state);
            this._target = target;
        }

        public void Update()
        {
            LifeCheck();
            this.stateController.StateUpdate();
        }

        public void AddMission(params BaseMission[] commands)
        {
            LifeCheck();
            missionManager.PushiMission(commands);
        }

        /// <summary>
        /// 插入新任務會導致目前任務立即終止
        /// </summary>
        /// <param name="isComplete">如果為真，終止時立即完成任務</param>
        /// <param name="commands"></param>
        public void InsertMission(bool isComplete, params BaseMission[] commands)
        {
            LifeCheck();
            missionManager.InsertMission(commands, isComplete);
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
            this.missionManager.Clear();
        }

        public BaseMission[] GetAllMission()
        {
            return missionManager.ToArray();
        }

        public void Terminate(bool isComplete = false)
        {
            LifeCheck();
            this._isTerminate = true;
            EndCurrentMission(isComplete);
            this.missionManager.Clear();
        }

        public void EndCurrentMission(bool isComplete = false)
        {
            if (!isIdle)
            {
                if (!missionManager.PeekMission().isBegin)
                    missionManager.PeekMission().Begin();
                if (isComplete)
                    missionManager.PeekMission().Complete();
                missionManager.PopMission().End();
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