using Game.Scripts;
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
        public static Snake mySnake = new Snake(50,5);
       // public static MyDeltaTimer delta =new MyDeltaTimer();
        public static int[,] grid =new int[50,50];
        public static List<PickUP> fruits = new List<PickUP>();
        public static Level1 level1 = new Level1();
        static void Main(string[] args)
        {
            Engine.Initialize("Snake",500,500);
            //level1.Start();
            Start.start(mySnake,fruits);
            
           

            
            while (true)
            {
                Update();//todo: update
                Render();
            }
        }
        // zeki es un capo
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
                        //GameManager.Instance.sprites.Add(new Sprite("Sprites/green.png", i * 10, j * 10,0, .625f, .625f,0,0,0));
                        Engine.Draw("Sprites/green.png", i * 10, j * 10, .625f, .625f);
                    }
                    else
                    {
                        //GameManager.Instance.sprites.Add(new Sprite("Sprites/grey.png", i * 10, j * 10, 0, .625f, .625f, 0, 0, 0));
                        Engine.Draw("Sprites/grey.png", i * 10, j * 10, .625f, .625f);
                    }
                    
                    
                }
            }
            //GameManager.Instance.sprites = GameManager.Instance.sprites.OrderBy(o=>o.order).ToList();
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
            foreach(GameObject gameObject in GameManager.Instance.Update) 
            {
                gameObject.Update();
            }
            Colicions();
        }
        static void Colicions()
        { 
            VoidEvent eatFruit=null;
            eatFruit += addSnakePiece;
            if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().transform.scale.x*10,
                fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().transform.scale.x))
            {
                fruits.First().ChangeToRandomPosition();
                //for (int i = 0;i< mySnake.snake.Count/2; i++)
                //{
                //    List<Vector2> buffer = new List<Vector2>();
                //    buffer = mySnake.snake[i].myPositions;
                //    mySnake.snake[i].myPositions = mySnake.snake[mySnake.snake.Count - 1 - i].myPositions;
                //    mySnake.snake[mySnake.snake.Count - 1 - i].myPositions=buffer;
                //    
                //}
                //foreach (SnakePart part in mySnake.snake)
                //{
                //   //part.myPositions.Reverse();
                //   part.myPositions.Clear();
                //}
                //mySnake.snake.Reverse();
                //mySnake.swapDirection();
                eatFruit();
                GameManager.Instance.points++;
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].transform.scale.x,
                mySnake.snake[i].transform.positon.x, mySnake.snake[i].transform.positon.y, mySnake.snake[i].transform.scale.x))
                {
                    GameManager.Instance.lives--;
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
            mySnake.SkankePartDelay = 2;
            mySnake.snake.Add(new SnakePart(70, 400, 1, mySnake));
            for (int i = 1; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 460, 1,mySnake));
        }
       

    }
    

}
    
    
