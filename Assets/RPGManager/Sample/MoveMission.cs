using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class MoveMission : BaseMission
    {
        public enum DirectionEnum
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            IDLE
        }
        private Transform mTrans;
        private Vector3 mTarget;
        private float speed;
        private float lastFrameTime;

        public MoveMission(Transform t, Vector3 v, float speed)
        {
            this.mTrans = t;
            this.mTarget = v;
            this.speed = speed;
        }

        public override void Begin()
        {
            this.lastFrameTime = Time.time;
        }

        public override void Update()
        {
            float deltaTime = Time.time - this.lastFrameTime;
            this.lastFrameTime = Time.time;
            float deltaDistance = speed * deltaTime;
            for (int i = 0; i < 100; i++)
            {
                deltaDistance = MoveTo(FindDirection(), deltaDistance);
                if (deltaDistance == 0)
                {
                    break;
                }
                if (i == 100)
                {
                    throw new System.Exception("無窮迴圈");
                }
            }
        }

        private DirectionEnum FindDirection()
        {
            //float leftDistance;
            if (mTrans.position.y < mTarget.y)
            {
                return DirectionEnum.UP;
            }
            else if (mTrans.position.x > mTarget.x)
            {
                return DirectionEnum.LEFT;
            }
            else if (mTrans.position.x < mTarget.x)
            {
                return DirectionEnum.RIGHT;
            }
            else if (mTrans.position.y > mTarget.y)
            {
                return DirectionEnum.DOWN;
            }
            return DirectionEnum.IDLE;
        }

        private float MoveTo(DirectionEnum direct, float distance)
        {
            float leftDistance = GetLeftDistance(direct, distance);
            if (leftDistance == 0)
            {
                switch (direct)
                {
                    case DirectionEnum.LEFT:
                        this.mTrans.position = new Vector3(this.mTrans.position.x - distance, this.mTrans.position.y);
                        break;
                    case DirectionEnum.RIGHT:
                        this.mTrans.position = new Vector3(this.mTrans.position.x + distance, this.mTrans.position.y);
                        break;
                    case DirectionEnum.UP:
                        this.mTrans.position = new Vector3(this.mTrans.position.x, this.mTrans.position.y + distance);
                        break;
                    case DirectionEnum.DOWN:
                        this.mTrans.position = new Vector3(this.mTrans.position.x, this.mTrans.position.y - distance);
                        break;
                    case DirectionEnum.IDLE:
                    default:
                        break;
                }
            }
            else
            {
                switch (direct)
                {
                    case DirectionEnum.LEFT:
                    case DirectionEnum.RIGHT:
                        this.mTrans.position = new Vector3(this.mTarget.x, this.mTrans.position.y);
                        break;
                    case DirectionEnum.UP:
                    case DirectionEnum.DOWN:
                        this.mTrans.position = new Vector3(this.mTrans.position.x, this.mTarget.y);
                        break;
                    case DirectionEnum.IDLE:
                    default:
                        break;
                }
            }
            return leftDistance;
        }

        private float GetLeftDistance(DirectionEnum direct, float distance)
        {
            switch (direct)
            {
                case DirectionEnum.LEFT:
                case DirectionEnum.RIGHT:
                    return CanSpendAllDistance(mTrans.position.x, mTarget.x, distance);
                case DirectionEnum.UP:
                case DirectionEnum.DOWN:
                    return CanSpendAllDistance(mTrans.position.y, mTarget.y, distance);
                case DirectionEnum.IDLE:
                default:
                    return 0;
            }
        }

        private float CanSpendAllDistance(float start, float end, float distance)
        {
            if (Mathf.Abs(end - start) < distance)
            {
                return Mathf.Abs(distance - (end - start));
            }
            else
            {
                return 0;
            }
        }

        public override bool isExecutable
        {
            get
            {
                return !mTrans.position.Equals(mTarget);
            }
        }
    }
}