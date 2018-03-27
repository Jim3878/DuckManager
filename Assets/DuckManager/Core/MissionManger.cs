using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class MissionManger
    {
        public Queue<BaseMission> missionQueue = new Queue<BaseMission>();
        public bool isCompleteMode = false;
    }
}