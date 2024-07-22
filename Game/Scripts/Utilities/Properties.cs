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
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform(Vector2 pos,float ang,Vector2 scale)
        {
            this.position = pos;
            this.rotation = ang;
            this.scale = scale;
        }
        public Transform(float x=0, float y=0, float ang=0,float Sx=1,float Sy = 1)
        {
            position.x = x;
            position.y = y;
            rotation = ang;
            scale.x = Sx;
            scale.y = Sy;
        }
    }
    
    public class Render
    {
        public Render(string path,Transform transform)
        {
            this.textures = new Texture(path);
            this._path = path;
            this.imgSize = new Vector2(textures.Width * transform.scale.x, textures.Height * transform.scale.y);
        }
        public string _path;
        public Texture textures;
        public Vector2 imgSize;
        public void Draw(Transform transform)
        {
            Engine.Draw(textures, transform.position.x, transform.position.y, transform.scale.x, transform.scale.y);
        }
        public void chageTexture(string NewPath)
        {
            textures = new Texture(NewPath);
        }
    }
    internal class Properties
    {

    }
}
