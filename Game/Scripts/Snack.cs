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
	public class Snake:GameObject
	{
        public Action Move;
        public Snake(float Speed, int skankePartDelay)
        {
            speed = 1 / Speed;
            _SkankePartDelay = skankePartDelay;
            GameManager.Instance.Update.Add(this);
        }
        private int DelayCounter;
        private float timer=0;
        private int _SkankePartDelay;
        public List<SnakePart> snake = new List<SnakePart>();
        private float speed = 0.5f;
        private string direction = "Down";
        private string nextDirection = "Down";
        public int SkankePartDelay { get { return _SkankePartDelay; } set { _SkankePartDelay = value; } }
        public void DrawSnakeParts()
        {
            foreach (SnakePart snakePart in snake)
            {
                snakePart.Draw();
            }
        }
        public override void Start()
        {

        }
        public override void Update()
        {
            Movement(MyDeltaTimer.DeltaTime());
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
            if (timer <= 0)
            {
                switch (nextDirection)
                {
                    case "Left":
                        if ( direction != "Right")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Right":
                        if ( direction != "Left")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Down":
                        if ( direction != "Up")
                        {
                            direction = nextDirection;
                        }
                        break;
                    case "Up":
                        if ( direction != "Down")
                        {
                            direction = nextDirection;
                        }
                        break;
                }
                switch (direction)
                {
                    case "Left":                      
                        snake.First().transform.positon.x -= 10;
                        break;
                    case "Right":
                        snake.First().transform.positon.x += 10;
                        break;
                    case "Down":
                        snake.First().transform.positon.y += 10;
                        break;
                    case "Up":;
                        snake.First().transform.positon.y -= 10;
                        break;
                }
                timer = speed;
                SnakePartsMovement();
            }
        }
        public void swapDirection()
        {
            switch (direction)
            {
                case "Left":
                    direction = "Right";
                    break;
                case "Right":
                    direction = "Left";
                    break;
                case "Down":
                    direction = "Up";
                    break;
                case "Up":
                    direction = "Down";
                    break;
            }
            nextDirection = direction;
        }
        public void SnakePartsMovement()
        {
            DelayCounter++;
            snake[0].myPositions.Add(snake[0].transform.positon);
            if (snake[0].myPositions.Count > _SkankePartDelay+1)
            {
                snake[0].myPositions.RemoveAt(0);
            }
            for (int i = 1; i < snake.Count; i++)
            {
                //snake[i].parentPositions.Add(snake[i - 1].transform.positon);
                //if (snake[i].parentPositions.Count > _SkankePartDelay + 2)
                //{
                //    snake[i].parentPositions.RemoveAt(0);
                //    snake[i].transform.positon = snake[i].parentPositions[0];
                //}

                snake[i].myPositions.Add(snake[i].transform.positon);
                if (snake[i].myPositions.Count > _SkankePartDelay)
                {
                    snake[i].myPositions.RemoveAt(0);
                }
                if (snake[i - 1].myPositions.Count > _SkankePartDelay - 1)
                {
                    snake[i].transform.positon = snake[i - 1].myPositions[0];
                }
                    
            }
            if (DelayCounter== _SkankePartDelay)
            {
                DelayCounter=0;
            }
        }
        public void addSnakePiece()
        {
            snake.Add(new SnakePart(snake.Last().transform.positon.x, snake.Last().transform.positon.y - 11, 1, this));
        }
        public void collition()
        {

        }
    }
    public class SnakePart:GameObject
    {
        public SnakePart(float X, float Y, int Size,Snake body)
        {
            transform.positon.x = X; transform.positon.y = Y;
            transform.scale.x = Size; transform.scale.y = Size;
            _moveDelay = body.SkankePartDelay;
            //body.Move += MoveToPatentLastPosition;
        }
        private int _moveDelay;
        private List<Vector2> _myPositions = new List<Vector2>();
        private List<Vector2> _parentPositions = new List<Vector2>();
        private int _size = 10;
        private string _texture = "Sprites/rect4.png";

        public List<Vector2> parentPositions
        {
            get { return _parentPositions; }set { _parentPositions = value; }
        }
        public List<Vector2> myPositions
        {
            get { return _myPositions; }
            set { _myPositions = value; }
        }
        public int size { get { return _size; } set { _size = value; } }

        public override void Draw()
        {
            Engine.Draw(_texture, transform.positon.x, transform.positon.y, transform.scale.x, transform.scale.y);
        }

    }
}
