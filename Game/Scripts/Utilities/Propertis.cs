using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {
        public int x, y;
        public Vector2 (int x, int y) {
            this.x = x;
            this.y = y;
        }
        public static Vector2 cero()
        {
            return new Vector2 (0, 0);
        }
    }
    public struct Transform
    {
        public Vector2 positon;
        public int rotation;
        public Vector2 scale;
        public Transform(Vector2 pos,int ang,Vector2 scale)
        {
            this.positon = pos;
            this.rotation = ang;
            this.scale = scale;
        }
        public Transform(int x=0, int y=0, int ang=0,int Sx=1,int Sy = 1)
        {
            positon.x = x;
            positon.y = y;
            rotation = ang;
            scale.x = Sx;
            scale.y = Sy;
        }
    }

    public struct Sprite
    {
        public string path;
        public Transform transform;
        public Vector2 offset;
        public int order;
        public Sprite(string path,Transform transform,Vector2 offset,int oreder)
        {
            this.path = path;
            this.transform = transform;
            this.offset = offset;
            this.order =0;
        }
        public Sprite(string path,Vector2 posittion,int rotation, Vector2 scale,Vector2 offset,int oreder=0)
        {
            this.path = path;
            transform.positon = posittion;
            transform.rotation = rotation;
            transform.scale = scale;
            this.offset = offset;
            this.order = oreder;
        }
        public Sprite(
            string path,
            int posX=0, int posY = 0,
            int rotation = 0,
            int sclX = 1,int sclY = 1,
            int offsX = 0,int offsY = 0,
            int oreder = 0)
        {
            this.path = path;
            transform.positon.x = posX;
            transform.positon.y = posY;
            transform.rotation = rotation;
            transform.scale.x = sclX;
            transform.scale.y = sclY;
            offset.x = offsX;
            offset.y = offsY;
            this.order = oreder;
        }
    }
    internal class Propertis
    {

    }
}
