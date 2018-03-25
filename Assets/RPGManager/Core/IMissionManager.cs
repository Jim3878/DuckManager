using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public interface IMissionManager<T> where T : BaseMission
    {
        int Count { get; }
        MissionQueue<T> GetMissionQueue();
        T PopMission();
        T PeekMission();
        void PushiMission(params T[] mission);
        void InsertMission(T[] mission, bool isComplete = false);
        void Clear();
        T[] ToArray();
    }
}
