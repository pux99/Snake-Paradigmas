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
            buttons.Add(new Button(new Transform(190, 270, 0, 8, 2.5f),"Sprites/grey.png"));
            _start = new Text(new Transform(190, 270, 0, 0.5f, 0.5f), "play");
            buttons.Add(new Button(new Transform(150, 330, 0, 13.5f, 2.5f), "Sprites/grey.png"));
            _opt = new Text(new Transform(150, 330, 0, 0.5f, 0.5f), "Options");
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f), "Sprites/grey.png"));
            _quit = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Exit");
            mySnake = new Snake(50, 5);
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
            mySnake.snake.First().transform.position.x = 50;
            mySnake.snake.First().transform.position.y = 0;
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
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               buttons[i].transform.position.x, buttons[i].transform.position.y, buttons[i].render.imgSize.x, buttons[i].render.imgSize.y))
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

            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
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
            Collisions(lossLife, getPoint);


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
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    KillSnake(losslifes);
                }
                if (Collision.RectRect(fruits.First().transform.position.x, fruits.First().transform.position.y, fruits.First().render.imgSize.x * 2, fruits.First().render.imgSize.y * 2,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    fruits.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            eatFruit += fruits.First().Reproducer;
            if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                fruits.First().transform.position.x, fruits.First().transform.position.y, fruits.First().render.imgSize.x, fruits.First().render.imgSize.y))
            {
                if (GameManager.Instance.leaveTrash)
                {
                    Trash trashs = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.First().transform.position.x, (int)fruits.First().transform.position.y));
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
            foreach (Trash trashs in trash)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                trashs.transform.position.x, trashs.transform.position.y, trashs.render.imgSize.x, trashs.render.imgSize.y) && trashs.active)
                {
                    KillSnake(losslifes);
                }
                if (Collision.RectRect(fruits.First().transform.position.x, fruits.First().transform.position.y, fruits.First().render.imgSize.x * 2, fruits.First().render.imgSize.y * 2,
                        trashs.transform.position.x, trashs.transform.position.y, trashs.render.imgSize.x, trashs.render.imgSize.y))
                {
                    fruits.First().ChangeToRandomPosition();
                }
            }
            for (int i = 1; i < mySnake.snake.Count; i++)
            {

                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
                mySnake.snake[i].transform.position.x, mySnake.snake[i].transform.position.y, mySnake.snake[i].render.imgSize.x, mySnake.snake[i].render.imgSize.y))
                {
                    KillSnake(losslifes);
                }
            }
            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
        }
        public static void addSnakePiece()
        {
            mySnake.addSnakePiece("body");
            int lastPosition = mySnake.snake.Count - 1;
            LevelsManager.Instance.CurrentLevel.draws.Add(mySnake.snake[lastPosition]);
        }
        public static void NewSnake()
        {
            mySnake.snakePartDelay = GameManager.Instance.SnakeDelay;
            mySnake.snake.First().transform.position.x = 240;
            mySnake.snake.First().transform.position.y = 240;

            for (int i = 1; i < 5; i++)
            {
                mySnake.addSnakePiece("body");
                //mySnake.snake[i].transform.position = new Vector2 { x = -10, y = -10 };
                LevelsManager.Instance.CurrentLevel.draws.Add(mySnake.snake[i]);
            }
        }
        public static void KillSnake(VoidEvent lossLife)
        {
            GameManager.Instance.lives--;
            foreach (SnakePart snakePart in mySnake.snake)
            {
                if (snakePart != mySnake.snake[0])
                {
                    LevelsManager.Instance.CurrentLevel.draws.Remove(snakePart);
                    snakePart.myPositions.Clear();
                    mySnake.pool.ReleaseObject(snakePart);
                }
            }
            for (int i = mySnake.snake.Count; i > 1; i--)
            {
                mySnake.snake[i - 1].myPositions.Clear();
                mySnake.snake[i - 1].transform.position = new Vector2 { x = -10, y = -10 };
                mySnake.snake.Remove(mySnake.snake[i - 1]);
            }
            mySnake.snake.First().myPositions.Clear();
            lossLife.Invoke();
            NewSnake();
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
        private Text _title;
        private Text _back;
        private Text _trash;
        private Text _skins;
        public static Snake mySnake;
        public static List<Button> buttons;
        public override void Inizialize()
        {
            buttons = new List<Button>();

            _title = new Text(new Transform(60, 50, 0, 1, 1), "Options");
            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);

            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f), "Sprites/grey.png"));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");
            buttons.Add(new Button(new Transform(175, 330, 0, 10, 2.5f), "Sprites/grey.png"));
            _trash = new Text(new Transform(175, 330, 0, 0.5f, 0.5f), "Trash");
            buttons.Add(new Button(new Transform(175, 270, 0, 10, 2.5f), "Sprites/grey.png"));
            _skins = new Text(new Transform(175, 270, 0, 0.5f, 0.5f), "Skins");



            mySnake = new Snake(50, 5);
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
            mySnake.snake.First().transform.position.x = 50;
            mySnake.snake.First().transform.position.y = 0;
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               buttons[i].transform.position.x, buttons[i].transform.position.y, buttons[i].render.imgSize.x, buttons[i].render.imgSize.y))
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
                        case 2:
                            LevelsManager.Instance.SetLevel("Skins");
                            break;
                        default:
                            Console.WriteLine("No nay nivel");
                            break;
                    }
                }
            }

            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
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
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f), "Sprites/grey.png"));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");

            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);

            mySnake = new Snake(50, 5);
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
            mySnake.snake.First().transform.position.x = 50;
            mySnake.snake.First().transform.position.y = 0;
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               buttons[i].transform.position.x, buttons[i].transform.position.y, buttons[i].render.imgSize.x, buttons[i].render.imgSize.y))
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

            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
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
            buttons.Add(new Button(new Transform(190, 390, 0, 8, 2.5f), "Sprites/grey.png"));
            _back = new Text(new Transform(190, 390, 0, 0.5f, 0.5f), "Menu");


            mySnake = new Snake(50, 5);
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
            mySnake.snake.First().transform.position.x = 50;
            mySnake.snake.First().transform.position.y = 0;
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               buttons[i].transform.position.x, buttons[i].transform.position.y, buttons[i].render.imgSize.x, buttons[i].render.imgSize.y))
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

            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
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

    public class Skins : Levels
    {
        private Text _title;
        private Text _back;
        private Text _head;
        private Text _body;
        public static Snake mySnake;
        public static List<Button> buttons;
        public override void Inizialize()
        {
            buttons = new List<Button>();

            _title = new Text(new Transform(110, 50, 0, 1, 1), "Skins");

            buttons.Add(new Button(new Transform(190, 450, 0, 8, 2.5f), "Sprites/grey.png"));
            _back = new Text(new Transform(190, 450, 0, 0.5f, 0.5f), "Menu");

            
            _head = new Text(new Transform(85, 150, 0, 0.5f, 0.5f), "Head");
            buttons.Add(new Button(new Transform(125, 200, 0, 5, 5f), "Sprites/SnakeHead.png"));
            buttons.Add(new Button(new Transform(125, 275, 0, 5, 5f), "Sprites/Head_1.png"));
            buttons.Add(new Button(new Transform(125, 350, 0, 5, 5f), "Sprites/Head_1.png"));

            _body = new Text(new Transform(285, 150, 0, 0.5f, 0.5f), "Body");
            buttons.Add(new Button(new Transform(325, 200, 0, 5, 5f), "Sprites/Rect4.png"));
            buttons.Add(new Button(new Transform(325, 275, 0, 5, 5f), "Sprites/Body_1.png"));
            buttons.Add(new Button(new Transform(325, 350, 0, 5, 5f), "Sprites/Body_1.png"));

            mySnake = new Snake(50, 5);

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
            mySnake.snake.First().transform.position.x = 50;
            mySnake.snake.First().transform.position.y = 0;
        }
        public void Collisions()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake[0].transform.position.x, mySnake.snake[0].transform.position.y, mySnake.snake[0].render.imgSize.x, mySnake.snake[0].render.imgSize.y,
               buttons[i].transform.position.x, buttons[i].transform.position.y, buttons[i].render.imgSize.x, buttons[i].render.imgSize.y))
                {
                    switch (i)
                    {
                        case 0:
                            LevelsManager.Instance.SetLevel("Menu");
                            break;
                        case 1:
                            GameManager.Instance.snackHead = 0;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        case 2:
                            GameManager.Instance.snackHead = 1;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        case 3:
                            GameManager.Instance.snackHead = 2;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        case 4:
                            GameManager.Instance.snackBody = 0;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        case 5:
                            GameManager.Instance.snackBody = 1;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        case 6:
                            GameManager.Instance.snackBody = 2;
                            LevelsManager.Instance.SetLevel("Skins");
                            mySnake.snake.First().transform.position.x = 50;
                            mySnake.snake.First().transform.position.y = 0;
                            break;
                        default:
                            Console.WriteLine("No nay nivel");
                            break;
                    }
                }
            }

            if (mySnake.snake.First().transform.position.x < -10)
                mySnake.snake.First().transform.position.x = 500;
            if (mySnake.snake.First().transform.position.x > 510)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < -10)
                mySnake.snake.First().transform.position.y = 500;
            if (mySnake.snake.First().transform.position.y > 510)
                mySnake.snake.First().transform.position.y = 0;
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
}