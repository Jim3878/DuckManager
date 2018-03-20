using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class CommandQueue<T>:Queue<T>
    {
        Queue<T> mQueue
        {
            get
            {
                return base.MemberwiseClone() as Queue<T>;
            }
        }

        public bool isEmpty
        {
            get
            {
                return mQueue.Count == 0;
            }
        }
        

        public void Insert(T[] Items)
        {
            if (!isEmpty)
            {
                Queue<T> queue = new Queue<T>();
                queue.Enqueue(base.Dequeue());
                for (int i = 0; i < Items.Length; i++)
                {
                    queue.Enqueue(Items[i]);
                }
                while (base.Count > 0)
                {
                    queue.Enqueue(base.Dequeue());
                }

                while (queue.Count > 0)
                {
                    base.Enqueue(queue.Dequeue());
                }
                
            }else
            {
                for(int i = 0; i < Items.Length; i++)
                {
                    base.Enqueue(Items[i]);
                }
            }
        }
    }
}
