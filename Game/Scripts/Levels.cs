using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts;
using Microsoft.VisualBasic;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Game
{
    public abstract class Levels
    {
        public abstract void Inizialize();
        public abstract void Input();
        public abstract void Update();
        public abstract void Draw();
        public abstract void Reset();

        public List<Draw> draws=new List<Draw>();
        public List<Update> updates=new List<Update>();
        public List<Inputs> inputs=new List<Inputs>();
    }

    public class Menu : Levels
    {

        private Animation uroboros;
        private Text snack;
        private Text start;
        private Text opt;
        private Text quit;

        public Texture texture;

        public static Snake mySnake;

        public static List<Button> buttons;

        public Menu()
        {
           
        }

        public override void Inizialize()
        {
           




            buttons = new List<Button>();

            snack = new Text(new Transform(110, 50, 0, 1, 1), "SNACK");
            uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);
            buttons.Add( new Button(new Transform(190, 270, 0, 8, 2.5f)));
            start = new Text(new Transform(190, 270, 0, 0.5f, 0.5f), "play");
            buttons.Add(new Button(new Transform(150, 330, 0, 13.5f, 2.5f)));
            opt = new Text(new Transform(150, 330, 0, 0.5f, 0.5f), "Options");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f)));
            quit = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Exit");

            mySnake = new Snake(50, 5);
            for (int i = 1; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
          
        }

        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
       


                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].transform.scale.x, mySnake.snake[0].transform.scale.y,
               buttons[i].transform.positon.x, buttons[i].transform.positon.y, buttons[i].transform.scale.x, buttons[i].transform.scale.y))
                {
                    switch (i)
                    {
                        case 0:
                            LevelsManager.Instance.SetLevel("Gameplay");
                            break;
                        case 1:
                            LevelsManager.Instance.SetLevel("Options");
                            break;
                        case 2:
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("No nay nivel");
                            break;
                    }
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
        public override void Draw()
        {
           

            foreach (Draw draw in LevelsManager.Instance.CurrentLevel.draws)
            {
                draw.Draw();
            }
        }

        public override void Input()
        {
            foreach (Inputs input in LevelsManager.Instance.CurrentLevel.inputs)
            {
                input.Input();
            }
            if (Engine.GetKey(Keys.SPACE))
                LevelsManager.Instance.SetLevel("Gameplay");
        }

        public override void Update()
        {
            // Si la Snake toca el Boton PLAY collision.rectrect de snake con Boton gris
            //LevelsManager.Instance.SetLevel("Gameplay");
            //LevelsManager.Instance.SetLevel("Options");
            Collisions();
           foreach (Update update in LevelsManager.Instance.CurrentLevel.updates)
           {
               update.Update();
           }

        }
        public static void addSnakePiece()
        {
            mySnake.addSnakePiece();
        }
        public static void NewSnake()
        {
            mySnake.SkankePartDelay = 2;
            mySnake.snake.Add(new SnakePart(70, 400, 1, "Sprites/rect4.png"));
            for (int i = 1; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 460, 1, "Sprites/rect4.png"));
        }

        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }

    public class Gameplay : Levels,PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> fruits;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Gameplay()
        {

        }
        public override void Inizialize()
        {
            mySnake  = new Snake(50, 5);
            grid     = new int[50, 50];
            fruits   = new List<PickUP>();
            walls    = new List<Wall>();
            combatUI = new CombatUi(this);

            for (int i = 1; i < 6; i++) { 
                if(i==1)
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/SnakeHead.png"));
                }
                else
                { 
                mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
                }
            }

            fruits.Add(new Fruit(250, 300, 10));
            for (int i = 0; i < 10; i++)
            {
                walls.Add(new Wall(new Transform(100 + i * 10 , 100), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(300 + i * 10 , 100), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100 + i * 10 , 400), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(300 + i * 10 , 400), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100 , 100 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(400 , 100 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100 , 310 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(400 , 310 + i * 10), "Sprites/rect4.png"));
            }
            mySnake.snake.Add(new SnakePart(10, 200, 1, "Sprites/rect4.png"));
        }
        public override void Input()
        {
            foreach(Inputs input in LevelsManager.Instance.CurrentLevel.inputs)
            {
                input.Input();
            }
        }
        public override void Update()
        {
            foreach (Update update in LevelsManager.Instance.CurrentLevel.updates)
            {
                update.Update();
            }

            Console.WriteLine(GameManager.Instance.lives);
            if (GameManager.Instance.points >= 20)
            {
                LevelsManager.Instance.SetLevel("Victory");
            }
            if (GameManager.Instance.lives == 0)
            {
                LevelsManager.Instance.SetLevel("Defeat");
            }
            Collisions(this.lossLife,this.getPoint);


        }
        public override void Draw()
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
            //GameManager.Instance.sprites = GameManager.Instance.sprites.OrderBy(o=>o.order).ToList();
            foreach(Draw draw in LevelsManager.Instance.CurrentLevel.draws)
            {
                if (draw.active)
                    draw.Draw();
            }
        }

        static void Collisions(VoidEvent losslifes, VoidEvent getPoints)
        {
            foreach (Wall wall in walls)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].transform.scale.x, mySnake.snake[0].transform.scale.y,
               wall.transform.positon.x, wall.transform.positon.y, wall.transform.scale.x, wall.transform.scale.y))
                {
                    GameManager.Instance.lives--;
                    foreach (SnakePart snakePart in mySnake.snake)
                    {
                        LevelsManager.Instance.CurrentLevel.draws.Remove(snakePart);
                    }
                    mySnake.snake.Clear();
                    losslifes.Invoke();
                    NewSnake();
                }
                if (Collision.RectRect(fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().transform.scale.x, fruits.First().transform.scale.y,
               wall.transform.positon.x, wall.transform.positon.y, wall.transform.scale.x, wall.transform.scale.y))
                {
                    fruits.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().transform.scale.x * 10, mySnake.snake.First().transform.scale.y * 10,
                fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().transform.scale.x, fruits.First().transform.scale.y))
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
                getPoints();
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {

                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].transform.scale.x, mySnake.snake[0].transform.scale.y,
                mySnake.snake[i].transform.positon.x, mySnake.snake[i].transform.positon.y, mySnake.snake[i].transform.scale.x, mySnake.snake[i].transform.scale.y))
                {
                    GameManager.Instance.lives--;
                    foreach(SnakePart snakePart in mySnake.snake)
                    {
                        LevelsManager.Instance.CurrentLevel.draws.Remove(snakePart);
                    }
                    mySnake.snake.Clear();
                    losslifes.Invoke();
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
            mySnake.snake.Add(new SnakePart(70, 400, 1, "Sprites/SnakeHead.png"));
            for (int i = 1; i < 6; i++)
                mySnake.snake.Add(new SnakePart(50, 460, 1, "Sprites/rect4.png"));
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }

    public class Options : Levels
    {
        public override void Inizialize()
        {

        }
        public override void Input()
        {

        }
        public override void Update()
        {

        }
        public override void Draw()
        {

        }
        public override void Reset()
        {

        }
    }

    public class Victory : Levels
    {
        public override void Inizialize()
        {
            
        }
        public override void Input()
        {

        }
        public override void Update()
        {

        }
        public override void Draw()
        {

        }
        public override void Reset()
        {

        }
    }

    public class Defeat : Levels
    {
        public override void Inizialize()
        {

        }
        public override void Input()
        {

        }
        public override void Update()
        {

        }
        public override void Draw()
        {

        }
        public override void Reset()
        {

        }
    }


}
