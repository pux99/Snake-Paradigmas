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
        public float x, y;
        public Vector2(float x, float y) {
            this.x = x;
            this.y = y;
        }
        public static Vector2 cero()
        {
            return new Vector2(0, 0);
        }
        public override int GetHashCode()// porque nesesito esto
        {
            return 0;
        }
        public override bool Equals(object o)//porque nesestio esto
        {
            return true;
        }
        public static Vector2 operator + (Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x , a.y + b.y);
        }
        public static Vector2 operator - (Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static bool operator == (Vector2 left, Vector2 right)
        {
            return(left.x==right.x && left.y==right.y);
        }
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return (left.x == right.x && left.y == right.y);
        }
        //public static Vector2 oprator +
        //AGREGAR OPERADOR
    }
    public struct Transform
    {
        public Vector2 positon;
        public float rotation;
        public Vector2 scale;

        public Transform(Vector2 pos,float ang,Vector2 scale)
        {
            this.positon = pos;
            this.rotation = ang;
            this.scale = scale;
        }
        public Transform(float x=0, float y=0, float ang=0,float Sx=1,float Sy = 1)
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
        public float order;
        public Sprite(string path,Transform transform,Vector2 offset,float oreder)
        {
            this.path = path;
            this.transform = transform;
            this.offset = offset;
            this.order =0;
        }
        public Sprite(string path,Vector2 posittion,float rotation, Vector2 scale,Vector2 offset,float oreder=0)
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
            float posX=0, float posY = 0,
            float rotation = 0,
            float sclX = 1,float sclY = 1,
            float offsX = 0,float offsY = 0,
            float oreder = 0)
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
