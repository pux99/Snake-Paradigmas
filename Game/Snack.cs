using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Game
{
	public class Snake
	{
        public Action Move;
        public Snake(float Speed, int skankePartDelay)
        {
            speed = 1 / Speed;
            SkankePartDelay = skankePartDelay;
        }
        private int DelayCounter;
        public float timer=0;
        public int SkankePartDelay;
        public List<SnakePart> snake = new List<SnakePart>();
        public float speed = 0.5f;
        public string direction = "Down";
        public string nextDirection = "Down";
        public void DrawSnakeParts()
        {
            foreach (SnakePart snakePart in snake)
            {
                snakePart.Draw();
            }
        }

        public void Movement(float deltaTime)
        {
            if (Engine.GetKey(Keys.A))
                nextDirection = "Left";
            if (Engine.GetKey(Keys.D))
                nextDirection = "Right";
            if (Engine.GetKey(Keys.S))
                nextDirection = "Down";
            if (Engine.GetKey(Keys.W))
                nextDirection = "Up";
            timer -= deltaTime;
            Console.WriteLine(timer.ToString());
            if (timer <= 0)
            {
                snake.First().myPreviousPositionX = snake.First().x;
                snake.First().myPreviousPositionY = snake.First().y;
                switch (nextDirection)
                {
                    case "Left":
                        if (snake.First().y % 10 == 0 && direction != "Right")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Right":
                        if (snake.First().y % 10 == 0 && direction != "Left")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Down":
                        if (snake.First().x % 10 == 0 && direction != "Up")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Up":
                        if (snake.First().x % 10 == 0 && direction != "Down")
                        {
                            direction = nextDirection;
                        }
                        break;
                }
                switch (direction)
                {
                    case "Left":                      
                        snake.First().x -= 10;
                        break;
                    case "Right":
                        snake.First().x += 10;
                        break;
                    case "Down":
                        snake.First().y += 10;
                        break;
                    case "Up":;
                        snake.First().y -= 10;
                        break;
                }
                timer = speed;
                SnakePartsMovement();


            }
            
            
        }
        public void SnakePartsMovement()
        {
            DelayCounter++;
            for (int i = 1; i < snake.Count; i++)
            {
                
                snake[i].parentPositionsx.Add(snake[i - 1].x);
                snake[i].parentPositionsy.Add(snake[i - 1].y);
                if (snake[i].parentPositionsx.Count > SkankePartDelay+2) { 
                    snake[i].parentPositionsx.RemoveAt(0);
                    snake[i].x = snake[i].parentPositionsx[0];
                }
                if (snake[i].parentPositionsy.Count > SkankePartDelay+2) { 
                    snake[i].parentPositionsy.RemoveAt(0);
                    snake[i].y = snake[i].parentPositionsy[0];
                }
                //if (snake[i].parentPositionsx.Count > SkankePartDelay+1)
                //    snake[i].x = snake[i].parentPositionsx[SkankePartDelay];
                //if (snake[i].parentPositionsy.Count > SkankePartDelay + 1)
                //    snake[i].y = snake[i].parentPositionsy[SkankePartDelay];
            }
            if(DelayCounter== SkankePartDelay)
            {
                DelayCounter=0;
            }
        }
    }
    public class SnakePart
    {
        public SnakePart(int X, int Y, int Size,Snake body)
        {
            _x = X; _y = Y; _size = Size;_moveDelay = body.SkankePartDelay;
            body.Move += MoveToPatentLastPosition;
        }
        private int _moveDelay;
        private int _x;
        private int _y;
        private int _myPreviousPositionX;
        private int _myPreviousPositionY;
        public List<int> parentPositionsx = new List<int>();
        public List<int> parentPositionsy = new List<int>();
        private int _parentX;
        private int _parentY;
        private int _size = 10;
        private string _texture = "Sprites/rect4.png";
        public int x { get { return _x; } set { _x = value; } }
        public int y { get { return _y; } set { _y = value; } }
        public int parentX { get { return _parentX; } set { _parentX = value; } }
        public int parentY { get { return _parentY; } set { _parentY = value; } }
        public int myPreviousPositionX { get { return _myPreviousPositionX; } set { _myPreviousPositionX = value; } }
        public int myPreviousPositionY { get { return _myPreviousPositionY; } set { _myPreviousPositionY = value; } }

        public int size { get { return _size; } set { _size = value; } }

        public void Draw()
        {
            Engine.Draw(_texture, x, y, 1, 1);
        }
        public void MoveToPatentLastPosition()
        {
            parentPositionsx.Add(x);
            parentPositionsy.Add(y);
            //parentPositionsx.Count > 10 ? parentPositionsx.RemoveAt(0) : parentPositionsx.RemoveAt(0);
            if (parentPositionsx.Count > 10) { parentPositionsx.RemoveAt(0); }
            if (parentPositionsy.Count > 10) parentPositionsy.RemoveAt(0);
            x = parentPositionsx[_moveDelay];
            y = parentPositionsy[_moveDelay];
        }

    }
}
