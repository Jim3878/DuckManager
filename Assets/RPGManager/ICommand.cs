using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public  class ICommand
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
        private bool _isTerminated=false;
        public bool isTerminated
        {
            get
            {
                return _isTerminated;
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

        public void TouchCommandTerminated()
        {
            _isTerminated = true;
        }
        
        public virtual void Begin() { }
        public virtual void Update() {}
        public virtual void End() { }
        public virtual bool isExecutable { get { return true; } }

        public override string ToString()
        {
            return "<Command>" + GetType().Name;
        }
    }
}
