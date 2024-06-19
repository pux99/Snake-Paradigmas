using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Pool<T1, T>
    {
        private Queue<T> _elements;
        private Func<T1, T> _elementGenerator;

        public Pool(Func<T1, T> elementGenerator) 
        { 
            _elements = new Queue<T>();
            _elementGenerator = elementGenerator;
        }

        public Queue<T> Elements  
        {
            get { return _elements; }   
            set { _elements = value; }  
        }

        public T GetElement(T1 id) 
        {
            T item;

            if (_elements.Count > 0)
            {
                item = _elements.Dequeue();
            }
            else
            {
                item = _elementGenerator(id);
            }
            return item;
        }

        public void ReleaseObject(T item)
        {
            _elements.Enqueue(item);
        }
    }
}
