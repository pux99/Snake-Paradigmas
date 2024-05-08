using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Game
{
    public delegate void VoidEvent();

    public class Program
    {
        public static Snake mySnake = new Snake(25,5);
        public static MyDeltaTimer delta =new MyDeltaTimer();
        public static int[,] grid =new int[50,50];
        public static List<PickUP> fruits = new List<PickUP>();
        static void Main(string[] args)
        {
            Engine.Initialize("Snake",500,500);
            Start.start(mySnake,fruits);
           

            
            while (true)
            {
                Update();//todo: update
                Render();
            }
        }

        static void Render()
        {
            Engine.Clear();
            Drawing();
            Engine.Show();
        }
        static void Drawing()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Engine.Draw("Sprites/green.png", i * 10, j * 10, .625f, .625f);
                    }
                    else
                    {
                        Engine.Draw("Sprites/grey.png", i * 10, j * 10, .625f, .625f);
                    }
                    
                    
                }
            }
            GameManager.Instance.sprites = GameManager.Instance.sprites.OrderBy(o=>o.order).ToList();
            foreach (Sprite sprite in GameManager.Instance.sprites)
            {
                Engine.Draw(sprite.path,
                            sprite.transform.positon.x,sprite.transform.positon.y,
                            sprite.transform.scale.x,sprite.transform.scale.y,
                            sprite.transform.rotation,
                            sprite.offset.x,sprite.offset.y);
            }
            mySnake.DrawSnakeParts();
            foreach (Fruit fruit in fruits)
            {
                fruit.Draw();
            }
            
        }
        static void Update()
        {

            Movement();
            Colicions();
        }
        static void Movement()
        {
            mySnake.Movement(delta.DeltaTime());
        }
        static void Colicions()
        { 
            VoidEvent eatFruit=null;
            eatFruit += addSnakePiece;
            if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().transform.scale.x*10,
                fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().transform.scale.x))
            {
                fruits.First().ChangeToRandomPosition();
                eatFruit();
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().transform.scale.x,
                mySnake.snake[i].transform.positon.x, mySnake.snake[i].transform.positon.y, mySnake.snake[i].transform.scale.x))
                {
                    mySnake.snake.Clear();
                    NewSnake();
                }
            }
            if (mySnake.snake.First().transform.positon.x < -10)
                mySnake.snake.First().transform.positon.x = 500;
            if (mySnake.snake.First().transform.positon.x > 510)
                mySnake.snake.First().transform.positon.x = 0;
            if (mySnake.snake.First().transform.positon.y < -10)
                mySnake.snake.First().transform.positon.y = 500;
            if (mySnake.snake.First().transform.positon.y > 510)
                mySnake.snake.First().transform.positon.y = 0;


        }

       
        public static void addSnakePiece()
        {
            mySnake.addSnakePiece();
        }
        public static void NewSnake()
        {
            mySnake.SkankePartDelay = 0;
            for (int i = 0; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 500, 1,mySnake));
        }
       

    }
    

}
    
    
