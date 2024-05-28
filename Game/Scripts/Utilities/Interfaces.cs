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
            set;
        }
    }

    public interface Inputs
    {
        void Input();   
    }

    public interface PlayableLevel
    {
        VoidEvent getPoint { get; set; }
        VoidEvent lossLife { get; set; }
    }


    //public interface Collider
    //{
    //    int layer
    //    {
    //        get;
    //    }
    //    void Collicion();
    //}
    public interface IPickable
    {
        void PickAp();
    }
}
