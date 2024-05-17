using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface Update
    {
        void Update();
    }

    public interface Draw
    {
        void Draw();
        bool active
        {
            get;
        }
    }

    public interface Inputs
    {
        void Input();
    }


    //public interface Collider
    //{
    //    int layer
    //    {
    //        get;
    //    }
    //    void Collicion();
    //}
}
