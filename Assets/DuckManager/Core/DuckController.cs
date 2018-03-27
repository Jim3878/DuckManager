using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class DuckController
    {
        object _target;
        public object target
        {
            get
            {
                return _target;
            }
        }
        StateController ctrl;
        public bool _isCompleteMode;
        public IState currentState
        {
            get
            {
                return ctrl.CurrentState;
            }
        }
        public Queue<BaseMission> missionQueue;
        public bool isTermanated
        {
            get
            {
                return ctrl == null;
            }
        }
        
        public DuckController(object target)
        {
            this._target = target;
            missionQueue = new Queue<BaseMission>();
            ctrl = new StateController(new WaitState(this));
        }

        public void AddMission(params BaseMission[] missions)
        {
            for (int i = 0; i < missions.Length; i++)
            {
                missions[i].SetProperty(this);
                missionQueue.Enqueue(missions[i]);
            }
        }

        public void ClearMission()
        {
            missionQueue.Clear();
        }

        public void GlobalTransTo(BaseMission mission)
        {
            mission.SetProperty(this);
            ctrl.TransTo(mission);
        }

        public void Terminate()
        {
            missionQueue.Clear();
            ctrl.Terminate();
            ctrl = null;
            if (target is UnityEngine.Object)
            {
                GameObject.Destroy(target as UnityEngine.Object);
            }
        }

        public void Update()
        {
            ctrl.StateUpdate();
        }
    }
}