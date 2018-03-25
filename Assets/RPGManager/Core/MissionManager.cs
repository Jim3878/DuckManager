using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class MissionManager<T> : IMissionManager<T> where T : BaseMission
    {
        private MissionQueue<T> missionQueue;

        public MissionManager( )
        {
            missionQueue = new MissionQueue<T>();
        }

        public MissionManager(  MissionQueue<T> missionQueue)
        {
            this.missionQueue = missionQueue;
        }

        public int Count
        {
            get
            {
                return missionQueue.Count;
            }
        }

        public void Clear()
        {
            missionQueue.Clear();
        }

        public MissionQueue<T> GetMissionQueue()
        {
            return missionQueue;
        }

        public void InsertMission(T[] mission, bool isComplete = false)
        {
            missionQueue.Insert(mission);
            if (isComplete)
            {
                missionQueue.Peek().Complete();
            }
            missionQueue.Dequeue().End();
        }

        public T PeekMission()
        {
            return missionQueue.Peek();
        }

        public T PopMission()
        {
            return missionQueue.Dequeue();
        }

        public void PushiMission(params T[] mission)
        {
            for (int i = 0; i < mission.Length; i++)
            {
                missionQueue.Enqueue(mission[i]);
            }
        }

        public T[] ToArray()
        {
            return missionQueue.ToArray();
        }
    }
}
