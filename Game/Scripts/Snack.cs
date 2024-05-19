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
	public class Snake : Update , Inputs
	{       
        public Snake(float speed, int skankePartDelay)
        {
            _speed = 1 / speed;
            _skankePartDelay = skankePartDelay;
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
            LevelsManager.Instance.CurrentLevel.inputs.Add(this);
        }


        private int _skankePartDelay;
        private float _timer=0;
        private float _speed = 0.5f;
        private string _direction = "Down";
        private string _nextDirection = "Down";
        public List<SnakePart> snake = new List<SnakePart>();

        public int SkankePartDelay { get { return _skankePartDelay; } set { _skankePartDelay = value; } }

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
                _timer = _speed;
                SnakePartsMovement();
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
        public void DrawSnakeParts()
        {
            foreach (SnakePart snakePart in snake)
            {
                snakePart.Draw();
            }
        }
        public void SnakePartsMovement()
        {
            snake[0].myPositions.Add(snake[0].transform.positon);
            if (snake[0].myPositions.Count > _skankePartDelay+1)
            {
                snake[0].myPositions.RemoveAt(0);
            }
            for (int i = 1; i < snake.Count; i++)
            {
                snake[i].myPositions.Add(snake[i].transform.positon);
                if (snake[i].myPositions.Count > _skankePartDelay)
                {
                    snake[i].myPositions.RemoveAt(0);
                }
                if (snake[i - 1].myPositions.Count > _skankePartDelay -1)
                {
                    snake[i].transform.positon = snake[i - 1].myPositions[0];
                }  
            }
        }
        public void addSnakePiece()
        {
            snake.Add(new SnakePart(snake.Last().transform.positon.x, snake.Last().transform.positon.y, 1, this));
        }
    }
    public class SnakePart:Draw
    {
        public SnakePart(float X, float Y, int Size,Snake body)
        {
            active = true;
            transform.positon.x = X;    transform.positon.y = Y;
            transform.scale.x   = Size; transform.scale.y   = Size;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
        public Transform transform;

        private string _texture = "Sprites/rect4.png";
        private List<Vector2> _myPositions = new List<Vector2>();
        public bool active { get; set; }
        public List<Vector2> myPositions
        {
            get { return _myPositions; }
            set { _myPositions = value; }
        }

        public void Draw()
        {
            Engine.Draw(_texture, transform.positon.x, transform.positon.y, transform.scale.x, transform.scale.y);
        }
    }
}
