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
        public static Snake mySnake = new Snake(25,10);
        public static MyDeltaTimer delta =new MyDeltaTimer();
        public static int[,] grid =new int[50,50];
        public static List<PickUP> fruits = new List<PickUP>();
        static void Main(string[] args)
        {
            Engine.Initialize("Snake",500,500);
            Start.start(mySnake);
            
            fruits.Add(new Fruit() { x = 250, y = 300, size = 10 });
            
            while (true)
            {
                //todo: update
                Render();
            }
        }

        static void Render()
        {


            Engine.Clear();
            Update();
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

            if (Engine.GetKey(Keys.N))
            {
                mySnake.snake.Add(new SnakePart(mySnake.snake.Last().x, mySnake.snake.Last().y,10,mySnake) );
            }
            mySnake.Movement(delta.DeltaTime());
        }
        static void Colicions()
        { 
            VoidEvent eatFruit=null;
            eatFruit += addSnakePiece;
            if (colicionRectRect(mySnake.snake.First().x, mySnake.snake.First().y, mySnake.snake.First().size,
                fruits.First().x, fruits.First().y, fruits.First().size))
            {
                fruits.First().ChangeToRandomPosition();
                eatFruit();
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {
                if (colicionRectRect(mySnake.snake.First().x, mySnake.snake.First().y, mySnake.snake.First().size,
                mySnake.snake[i].x, mySnake.snake[i].y, mySnake.snake[i].size))
                {
                    mySnake.snake.Clear();
                    NewSnake();
                }
            }
            if (mySnake.snake.First().x < -10)
                mySnake.snake.First().x = 500;
            if (mySnake.snake.First().x > 510)
                mySnake.snake.First().x = 0;
            if (mySnake.snake.First().y < -10)
                mySnake.snake.First().y = 500;
            if (mySnake.snake.First().y > 510)
                mySnake.snake.First().y = 0;


        }

        public static bool colicionRectRect(int rect1X,int rect1Y,int rect1Size, int rect2X, int rect2Y, int rect2Size)
        {
            return (rect1X < rect2X + rect2Size &&
                    rect1Y < rect2Y + rect2Size &&
                    rect1X + rect1Size > rect2X &&
                    rect1Y + rect1Size > rect2Y);
        }
        public static void addSnakePiece()
        {
            mySnake.snake.Add(new SnakePart(mySnake.snake.Last().x, mySnake.snake.Last().y - 11, 10,mySnake));
        }
        public static void NewSnake()
        {
            for (int i = 0; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 500, 10,mySnake));
        }
       

    }
    

}
    
    
