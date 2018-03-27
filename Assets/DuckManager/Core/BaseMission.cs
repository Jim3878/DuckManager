using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckManager
{
    public class BaseMission : IState
    {
        DuckController _duck;
        protected DuckController duck
        {
            get
            {
                return _duck;
            }
        }

        public void SetProperty(DuckController duck)
        {
            this._duck = duck;
        }
        
        protected void TransToNext()
        {
            if (this.duck.missionQueue.Count != 0)
            {
                TransTo(duck.missionQueue.Dequeue());
            }
            else
            {
                TransTo(new WaitState(duck));
            }
        }
    }
}