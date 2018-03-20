using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuckGame
{
    public class CommandQueue<T>:IEnumerable<T>
    {
        Queue<T> mQueue;

        public bool isEmpty
        {
            get
            {
                return mQueue.Count == 0;
            }
        }

        public int Count
        {
            get
            {
                return mQueue.Count;
            }
        }

        public void Insert(T[] Items)
        {
            if (!isEmpty)
            {
                Queue<T> queue = new Queue<T>();
                queue.Enqueue(mQueue.Dequeue());
                for (int i = 0; i < Items.Length; i++)
                {
                    queue.Enqueue(Items[i]);
                }
                while (mQueue.Count > 0)
                {
                    queue.Enqueue(mQueue.Dequeue());
                }
                mQueue = queue;
            }else
            {
                for(int i = 0; i < Items.Length; i++)
                {
                    mQueue.Enqueue(Items[i]);
                }
            }
        }
  
        public T[] ToArray()
        {
            //mQueue.GetEnumerator
            return mQueue.ToArray();
        }

        public CommandQueue()
        {
            this.mQueue = new Queue<T>();
        }

        public void EnQueue(T item)
        {
            mQueue.Enqueue(item);
        }

        public T DeQueue()
        {
            return mQueue.Dequeue();
        }

        public T Peek()
        {
            return mQueue.Peek();
        }

        public void Clear()
        {
            mQueue.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return mQueue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mQueue.GetEnumerator();
        }
    }
}
