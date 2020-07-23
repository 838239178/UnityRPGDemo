using System;
using System.Collections.Generic;
using System.Text;

namespace MyStructure 
{
    public abstract class myCompara<T>
    {
        public abstract bool Compare(T a, T b);
    }

    /// <summary>
    /// a小则返回真
    /// </summary>
    /// <typeparam name="T">实现IComparable的类</typeparam>
    public class Less<T> : myCompara<T> where T: IComparable
    {
        public override bool Compare(T a, T b)
        {
            if (a.CompareTo(b) < 0) return true;
            else return false;
        }
    }

    /// <summary>
    /// a大则返回真
    /// </summary>
    /// <typeparam name="T">实现IComparable的类</typeparam>
    public class Greater<T> : myCompara<T> where T : IComparable 
    {
        public override bool Compare(T a, T b)
        {
            if (a.CompareTo(b) > 0) return true;
            else return false;
        }
    }


    /// <summary>
    /// 优先队列
    /// </summary>
    /// <typeparam name="T">队列内容的类</typeparam>
    /// <typeparam name="U">规定大、小优先的类</typeparam>
    public class PriorityQueue<T,U> where U: myCompara<T>, new()
    {
        const int TOPINDEX = 1;

        List<T> heap;
        U compare = new U();

        public PriorityQueue()
        {
            heap = new List<T>();
            heap.Add(default(T));
        }

        /// <summary>
        /// 将ts中的数据构造成优先队列
        /// </summary>
        /// <param name="ts">原始数据</param>
        public PriorityQueue(List<T> ts)
        {
            heap = ts;
            heap.Insert(0, default(T));
            for(int i = (heap.Count-1)/2; i>=1; i--)
            {
                _adjustDown(i);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 1; i<heap.Count; i++)
            {
                yield return heap[i];
            }
        }

        public void Pop()
        {
            top = rear;
            heap.RemoveAt(heap.Count-1);
            _adjustDown(TOPINDEX);
        }
        
        public void Push(T obj)
        {
            heap.Add(obj);
            _adjustUp(heap.Count - 1);
        }

        public bool Empty()
        {
            if (heap.Count > 1) return true;
            else return false;
        }

        public T Front
        {
            get
            { 
                return top;
            }
        }

        private T top
        {
            get
            {
                return heap[TOPINDEX];
            }
            set
            {
                heap[TOPINDEX] = value;
            }
        }

        private T rear
        {
            get
            {
                return heap[heap.Count - 1];
            }
            set
            {
                heap[heap.Count - 1] = value;
            }
        }

        private void _adjustDown(int par)
        {
            int child;
            for(int i = par; i*2 < heap.Count; i = child)
            {
                child = i * 2;

                if(child+1 < heap.Count && compare.Compare(heap[child+1], heap[child]))
                {
                    child++;
                }

                if (compare.Compare(heap[child], heap[i]))
                {
                    T temp = heap[child];
                    heap[child] = heap[i];
                    heap[i] = temp;
                }
                else break;
            }
        }

        private void _adjustUp(int child)  
        {
            int parent;
            for(int i = child; i/2 >= 1; i = parent)
            {
                parent = i / 2;
                if (compare.Compare(heap[i], heap[parent]))
                {
                    T temp = heap[parent];
                    heap[parent] = heap[i];
                    heap[i] = temp;
                }
                else break;
            }
        }
    }
}
