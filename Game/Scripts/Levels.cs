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

        public List<Draw> draws = new List<Draw>();
        public List<Update> updates = new List<Update>();
        public List<Inputs> inputs = new List<Inputs>();
    }

    public class Menu : Levels
    {

        private Animation _uroboros;
        private Text _snack;
        private Text _start;
        private Text _opt;
        private Text _quit;
        public static Snake mySnake;
        public static List<Button> buttons;

        public Menu()
        {

        }

        public override void Inizialize()
        {

            buttons = new List<Button>();
            _snack = new Text(new Transform(110, 50, 0, 1, 1), "SNACK");
            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);
            buttons.Add(new Button(new Transform(190, 270, 0, 8, 2.5f)));
            _start = new Text(new Transform(190, 270, 0, 0.5f, 0.5f), "play");
            buttons.Add(new Button(new Transform(150, 330, 0, 13.5f, 2.5f)));
            _opt = new Text(new Transform(150, 330, 0, 0.5f, 0.5f), "Options");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f)));
            _quit = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Exit");
            mySnake = new Snake(50, 5);
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/SnakeHead.png"));
                }
                else
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
                }
            }
        }

        public override void Draw()
        {
            Engine.Draw("Sprites/bg_menu.jpg", 0, 0, 1.5f, 1.5f, 0, 200);

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
            //if (Engine.GetKey(Keys.SPACE)) { LevelsManager.Instance.SetLevel("Defeat"); }
        }

        public override void Update()
        {
            // Si la Snake toca el Boton PLAY collision.rectrect de snake con Boton gris
            Collisions();
            foreach (Update update in LevelsManager.Instance.CurrentLevel.updates)
            {
                update.Update();
            }

        }

        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.draws.Clear();
        }
        private void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
               buttons[i].transform.positon.x, buttons[i].transform.positon.y, buttons[i].imgSize.x, buttons[i].imgSize.y))
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
    }

    public class Gameplay : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<Fruit> fruits;
        public static List<Trash> trash;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Gameplay()
        {

        }
        public override void Inizialize()
        {
            mySnake = new Snake(50, 5);
            grid = new int[50, 50];
            fruits = new List<Fruit>();
            trash = new List<Trash>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;




            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    mySnake.addSnakePiece("head");
                }
                else
                {
                    mySnake.addSnakePiece("body");
                }
            }

            fruits.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(250, 300)));
            for (int i = 0; i < 10; i++)
            {
                walls.Add(new Wall(new Transform(100 + i * 10, 100), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(300 + i * 10, 100), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100 + i * 10, 400), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(300 + i * 10, 400), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100, 100 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(400, 100 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(100, 310 + i * 10), "Sprites/rect4.png"));
                walls.Add(new Wall(new Transform(400, 310 + i * 10), "Sprites/rect4.png"));
            }
        }
        public override void Input()
        {
            foreach (Inputs input in LevelsManager.Instance.CurrentLevel.inputs)
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

            if (GameManager.Instance.points >= 20)
            {
                LevelsManager.Instance.SetLevel("Victory");
            }
            if (GameManager.Instance.lives == 0)
            {
                LevelsManager.Instance.SetLevel("Defeat");
            }
            Collisions(lossLife,getPoint);


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
            foreach (Draw draw in LevelsManager.Instance.CurrentLevel.draws)
            {
                if (draw.active)
                    draw.Draw();
            }
        }

        static void Collisions(VoidEvent losslifes, VoidEvent getPoints)
        {
            foreach (Wall wall in walls)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
               wall.transform.positon.x, wall.transform.positon.y, wall.imgSize.x, wall.imgSize.y))
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
                if (Collision.RectRect(fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().imgSize.x*2, fruits.First().imgSize.y*2,
               wall.transform.positon.x, wall.transform.positon.y, wall.imgSize.x, wall.imgSize.y))
                {
                    fruits.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            eatFruit += fruits.First().Reproducer;
            if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().imgSize.x, mySnake.snake.First().imgSize.y,
                fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().imgSize.x, fruits.First().imgSize.y))
            {
                if (GameManager.Instance.leaveTrash)
                {
                    Trash trashs = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.First().transform.positon.x, (int)fruits.First().transform.positon.y));
                    trashs.active = false;
                    trash.Add(trashs);
                }
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
            foreach(Trash trashs in trash) {
                if (Collision.RectRect(mySnake.snake.First().transform.positon.x, mySnake.snake.First().transform.positon.y, mySnake.snake.First().imgSize.x, mySnake.snake.First().imgSize.y,
                trashs.transform.positon.x, trashs.transform.positon.y, trashs.imgSize.x, trashs.imgSize.y)&&trashs.active)
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
                if (Collision.RectRect(fruits.First().transform.positon.x, fruits.First().transform.positon.y, fruits.First().imgSize.x*2, fruits.First().imgSize.y*2,
                        trashs.transform.positon.x, trashs.transform.positon.y, trashs.imgSize.x, trashs.imgSize.y))
                {
                    fruits.First().ChangeToRandomPosition();
                }
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {

                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
                mySnake.snake[i].transform.positon.x, mySnake.snake[i].transform.positon.y, mySnake.snake[i].imgSize.x, mySnake.snake[i].imgSize.y))
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
            mySnake.addSnakePiece("body");
        }
        public static void NewSnake()
        {
            mySnake.snakePartDelay = GameManager.Instance.SnakeDelay;
            mySnake.addSnakePiece("head");
            for (int i = 1; i < 6; i++)
                mySnake.addSnakePiece("body");
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }

    public class Options : Levels
    {
        private Animation _uroboros;
        private Text _opt;
        private Text _back;
        private Text _trash;
        public static Snake mySnake;
        public static List<Button> buttons;
        public override void Inizialize()
        {
            buttons = new List<Button>();

            _opt = new Text(new Transform(60, 50, 0, 1, 1), "Options");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f)));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");
            buttons.Add(new Button(new Transform(175, 330, 0, 10, 2.5f)));
            _trash = new Text(new Transform(175, 330, 0, 0.5f, 0.5f), "Trash");
            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);

            mySnake = new Snake(50, 5);
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/SnakeHead.png"));
                }
                else
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
                }
            }   
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
               buttons[i].transform.positon.x, buttons[i].transform.positon.y, buttons[i].imgSize.x, buttons[i].imgSize.y))
                {
                    switch (i)
                    {
                        case 0:
                            LevelsManager.Instance.SetLevel("Menu");
                            break;
                        case 1:
                            LevelsManager.Instance.SetLevel("Menu");
                            GameManager.Instance.leaveTrash = !GameManager.Instance.leaveTrash;
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
            Engine.Draw("Sprites/bg_menu.jpg", 0, 0, 1.5f, 1.5f, 0, 200);
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
            //if (Engine.GetKey(Keys.SPACE))
            //LevelsManager.Instance.SetLevel("Gameplay");
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
      
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }

    public class Victory : Levels
    {
        private Animation _uroboros;
        private Text _victory;
        private Text _back;
        public static Snake mySnake;
        public static List<Button> buttons;

        public override void Inizialize()
        {
            buttons = new List<Button>();

            _victory = new Text(new Transform(60, 50, 0, 1, 1), "Victory");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f)));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");
            
            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);

            mySnake = new Snake(50, 5);
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/SnakeHead.png"));
                }
                else
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
                }
            }
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
               buttons[i].transform.positon.x, buttons[i].transform.positon.y, buttons[i].imgSize.x, buttons[i].imgSize.y))
                {
                    switch (i)
                    {
                        case 0:
                            LevelsManager.Instance.SetLevel("Menu");
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

            Engine.Draw("Sprites/bg_victory.png", 0, 0, 50f, 50f);
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
            //if (Engine.GetKey(Keys.SPACE))
            //LevelsManager.Instance.SetLevel("Gameplay");
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


        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }

    public class Defeat : Levels
    {
        private Animation _uroboros;
        private Text _over;
        private Text _game;
        private Text _back;
        public static Snake mySnake;
        public static List<Button> buttons;
        public override void Inizialize()
        {
            buttons = new List<Button>();

            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);
            _game = new Text(new Transform(140, 10, 0, 1, 1), "Game");
            _over = new Text(new Transform(140, 70, 0, 1, 1), "Over");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f)));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");


            mySnake = new Snake(50, 5);
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/SnakeHead.png"));
                }
                else
                {
                    mySnake.snake.Add(new SnakePart(50, 500, 1, "Sprites/rect4.png"));
                }
            }
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.positon.x, mySnake.snake[0].transform.positon.y, mySnake.snake[0].imgSize.x, mySnake.snake[0].imgSize.y,
               buttons[i].transform.positon.x, buttons[i].transform.positon.y, buttons[i].imgSize.x, buttons[i].imgSize.y))
                {
                    switch (i)
                    {
                        case 0:
                            LevelsManager.Instance.SetLevel("Menu");
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

            Engine.Draw("Sprites/bg_defeat.png", 0, 0, 50f, 50f);
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
            //if (Engine.GetKey(Keys.SPACE))
            //LevelsManager.Instance.SetLevel("Gameplay");
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
       
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }
}