using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class GameObject
    {
        public Transform transform =new Transform();

        public GameObject() { }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Draw() { }

        public virtual void OnDestroy() { }

    }
}
