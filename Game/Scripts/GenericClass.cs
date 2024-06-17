using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    class GenericClass<T>
    {
        private T genericMemberVariable;
        public GenericClass(T value)
        {
            genericMemberVariable = value;
        }
        public T genericProperty { get; set; }
    }
}
