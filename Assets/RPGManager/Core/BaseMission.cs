using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public  class BaseMission
    {
        private DuckController _duckController;
        protected DuckController duckController
        {
            get
            {
                return _duckController;
            }
        }
        private bool _isBegin=false;
        public bool isBegin
        {
            get
            {
                return _isBegin;
            }
        }

        public void SetProperty(DuckController duckController)
        {
            this._duckController = duckController;
        }

        public void TouchCommandBegin()
        {
            _isBegin = true;
        }
        
        public virtual void Begin(object target) { }
        public virtual void Update(object target) {}
        public virtual void Complete(object target) { }
        public virtual void End(object target) { }
        public virtual bool isExecutable { get { return true; } }

        public override string ToString()
        {
            return "<Command>" + GetType().Name;
        }
    }
}
