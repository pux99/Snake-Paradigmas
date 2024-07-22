using Game.Scripts;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Game
{
	public class Snake : Update , Inputs
	{       
        public Snake(float speed, int skankePartDelay)
        {
            _speed = 1 / speed;
            _snakePartDelay = skankePartDelay;
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
            LevelsManager.Instance.CurrentLevel.inputs.Add(this);
        }


        private int _snakePartDelay;
        private float _SnakeSpeed = 0;
        private float _timer=0;
        public float _speed = 0.5f;
        public string _direction = "Down";
        private string _nextDirection = "Down";
        public List<SnakePart> snake = new List<SnakePart>();
        public Pool<String, SnakePart> pool = new Pool<String, SnakePart>(generatePart);
       
        public int snakePartDelay { get { return _snakePartDelay; } set { _snakePartDelay = value; } }
        public float snakeSpeed = 0;

        public void Update()
        {
            Movement(MyDeltaTimer.deltaTime);
        }
        public void Input()
        {
            if (Engine.GetKey(Keys.A))
                _nextDirection = "Left";
            if (Engine.GetKey(Keys.D))
                _nextDirection = "Right";
            if (Engine.GetKey(Keys.S))
                _nextDirection = "Down";
            if (Engine.GetKey(Keys.W))
                _nextDirection = "Up";
        }
        public void Movement(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                switch (_nextDirection)
                {
                    case "Left":
                        if ( _direction != "Right")
                        {
                            _direction = _nextDirection;
                        }
                        break;
                    case "Right":
                        if ( _direction != "Left")
                        {
                            _direction = _nextDirection;
                        }
                        break;
                    case "Down":
                        if ( _direction != "Up")
                        {
                            _direction = _nextDirection;
                        }
                        break;
                    case "Up":
                        if ( _direction != "Down")
                        {
                            _direction = _nextDirection;
                        }
                        break;
                }
                switch (_direction)
                {
                    case "Left":                      
                        snake.First().transform.position.x -= 10 + snakeSpeed;
                        break;
                    case "Right":
                        snake.First().transform.position.x += 10 + snakeSpeed;
                        break;
                    case "Down":
                        snake.First().transform.position.y += 10 + snakeSpeed;
                        break;
                    case "Up":;
                        snake.First().transform.position.y -= 10 + snakeSpeed;
                        break;
                }
                _timer = _speed;
                SnakePartsMovement();
                //Console.WriteLine(_speed);
            }
        }
        public void SwapDirection()
        {
            switch (_direction)
            {
                case "Left":
                    _direction = "Right";
                    break;
                case "Right":
                    _direction = "Left";
                    break;
                case "Down":
                    _direction = "Up";
                    break;
                case "Up":
                    _direction = "Down";
                    break;
            }
            _nextDirection = _direction;
        }
        public void SnakePartsMovement()
        {
            snake[0].myPositions.Add(snake[0].transform.position);
            if (snake[0].myPositions.Count > _snakePartDelay +1)
            {
                snake[0].myPositions.RemoveAt(0);
            }
            for (int i = 1; i < snake.Count; i++)
            {
                snake[i].myPositions.Add(snake[i].transform.position);
                if (snake[i].myPositions.Count > _snakePartDelay)
                {
                    snake[i].myPositions.RemoveAt(0);
                }
                if (snake[i - 1].myPositions.Count > _snakePartDelay -1)
                {
                    snake[i].transform.position = snake[i - 1].myPositions[0];
                }  
            }
        }
        public void addSnakePiece(string part)
        {
            SnakePart snakePart = pool.GetElement(part);
            snake.Add(snakePart);
        }
        static SnakePart generatePart(String part)
        {

            if (part == "body")
            {
                return (SnakePart)SnakeFactory.CreateSnake(SnakeFactory.part.snakePart, new Vector2(-10, -10));
            }
            else if (part == "head")
            {
                return (SnakePart)SnakeFactory.CreateSnake(SnakeFactory.part.snakeHead, new Vector2(240, 240));
            }
            else
            {
                return null;
            }
        }
    }
    public class SnakePart:Draw
    {
        public SnakePart(float X, float Y, int Size, string path) 
        {
            active = true;
            transform.position.x = X;    transform.position.y = Y;
            transform.scale.x   = Size; transform.scale.y   = Size;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            _render = new Render(path, transform);
        }
        public bool active { get; set; }
        protected Render _render;
        public Render render { get { return _render; } }
        public Transform transform;
        private List<Vector2> _myPositions = new List<Vector2>();

        public List<Vector2> myPositions
        {
            get { return _myPositions; }
            set { _myPositions = value; }
        }
        public void Draw()
        {
            render.Draw(transform);
        }
    }
}
