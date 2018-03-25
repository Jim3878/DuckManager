using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public  class BaseMission
    {
       
        private bool _isBegin=false;
        public bool isBegin
        {
            get
            {
                return _isBegin;
            }
        }
        private object _target;
        protected object target
        {
            get
            {
                return _target;
            }
        }

        public void SetProperty(object target)
        {
            this._target = target;
        }

        public void TouchCommandBegin()
        {
            _isBegin = true;
        }
        
        public virtual void Begin( ) { }
        public virtual void Update( ) {}
        public virtual void Complete( ) { }
        public virtual void End() { }
        public virtual bool isExecutable { get { return true; } }

        public override string ToString()
        {
            return "<Command>" + GetType().Name;
        }
    }
}
