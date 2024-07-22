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
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using Game;

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
            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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
                            LevelsManager.Instance.SetLevel("LevelSelector");
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

            if (mySnake.snake.First().transform.position.x < 0)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 500)
                mySnake.snake.First().transform.position.x = 0;
            if (mySnake.snake.First().transform.position.y < 0)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 500)
                mySnake.snake.First().transform.position.y = 0;
        }
    }

    public class LevelSelector : Levels
    {
        private Animation _uroboros;
        private Text _mainText;
        private Text _level1;
        private Text _level2;
        private Text _level3;
        private Text _level4;
        private Text _menu;
        public static Snake mySnake;
        public static List<Button> buttons;
        public LevelSelector() { }
        public override void Inizialize()
        {
            buttons = new List<Button>();
            _mainText = new Text(new Transform(90, 50, 0, 1, 1), "Levels");
            _uroboros = new Animation("Sprites/Animations/Uroboros/", new Transform(150, 130, 0, .25f, .25f), .2f, 27);

            buttons.Add(new Button(new Transform(110, 250, 0, 8, 2.5f), "Sprites/grey.png")); //Posicion de la lista 0.
            _level1 = new Text(new Transform(150, 250, 0, 0.5f, 0.5f), "1");

            buttons.Add(new Button(new Transform(290, 290, 0, 8, 2.5f), "Sprites/grey.png")); //Posicion de la lista 1.
            _level2 = new Text(new Transform(330, 290, 0, 0.5f, 0.5f), "2");


            buttons.Add(new Button(new Transform(110, 325, 0, 8, 2.5f), "Sprites/grey.png")); //Posicion de la lista 2.
            _level3 = new Text(new Transform(150, 325, 0, 0.5f, 0.5f), "3");


            buttons.Add(new Button(new Transform(290, 362, 0, 8, 2.5f), "Sprites/grey.png")); //Posicion de la lista 3.
            _level4 = new Text(new Transform(330, 362, 0, 0.5f, 0.5f), "4");

            buttons.Add(new Button(new Transform(110, 400, 0, 8, 2.5f), "Sprites/grey.png")); //Posicion de la lista 4.
            _level4 = new Text(new Transform(150, 400, 0, 0.5f, 0.5f), "5");


            buttons.Add(new Button(new Transform(190, 450, 0, 8f, 2.5f), "Sprites/grey.png")); //Posicion de la lista 4.
            _menu = new Text(new Transform(190, 450, 0, 0.5f, 0.5f), "Menu");

            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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
        }
        public override void Update()
        {
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
                            LevelsManager.Instance.SetLevel("Level1");
                            break;
                        case 1:
                            LevelsManager.Instance.SetLevel("Level2");
                            break;
                        case 2:
                            LevelsManager.Instance.SetLevel("Level3");
                            break;
                        case 3:
                            LevelsManager.Instance.SetLevel("Level4");
                            break;
                        case 4:
                            LevelsManager.Instance.SetLevel("Level5");
                            break;

                        case 5:
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

    }
    public class Level1 : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> pickUPs;
        public static List<Portal> portals;
        public static List<Fruit> fruits;
        public static List<Trash> trash;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public static int speedMult = GameManager.Instance.SnakeSpeed;
        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Level1()
        {

        }
        public override void Inizialize()
        {
            grid = new int[50, 50];
            pickUPs = new List<PickUP>();
            portals = new List<Portal>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            pickUPs.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(100, 100)));
            

            foreach (PickUP pickUP in pickUPs)
            {
                if (pickUP is Portal portal)
                    portals.Add((Portal)portal);
            }
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

            if (GameManager.Instance.points >= 10)
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
                if (Collision.RectRect(pickUPs.First().transform.position.x, pickUPs.First().transform.position.y, pickUPs.First().render.imgSize.x * 2, pickUPs.First().render.imgSize.y * 2,
                    wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    pickUPs.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            Fruit fruit = (Fruit)pickUPs.First();
            eatFruit += fruit.Reproducer;
            for (int i = 0; i < pickUPs.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                pickUPs[i].transform.position.x, pickUPs[i].transform.position.y, pickUPs[i].render.imgSize.x, pickUPs[i].render.imgSize.y))
                {
                    switch (pickUPs[i])
                    {
                        case Fruit fruits:
                            if (GameManager.Instance.leaveTrash)
                            {
                                Trash trash = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.transform.position.x, (int)fruits.transform.position.y));
                                trash.active = false;
                                pickUPs.Add(trash);
                            }
                            fruits.ChangeToRandomPosition();
                            foreach (PickUP item in pickUPs)
                            {
                                if (fruits != item && Collision.RectRect(fruits.transform.position.x, fruits.transform.position.y, fruits.render.imgSize.x, fruits.render.imgSize.y,
                                                                      item.transform.position.x, item.transform.position.y, item.render.imgSize.x, item.render.imgSize.y))
                                {
                                    fruits.ChangeToRandomPosition();
                                }
                            }
                            if (speedMult < 30)
                            {
                                speedMult++;
                                mySnake._speed = 1f / speedMult;
                            }
                            eatFruit();
                            GameManager.Instance.points++;
                            getPoints();
                            break;
                        case Trash trash:
                            KillSnake(losslifes);
                            break;
                        case Reverse reverse:
                            Texture body = mySnake.snake.Last().render.textures;
                            Texture head = mySnake.snake.First().render.textures;
                            mySnake.snake.Reverse();
                            mySnake.snake.Last().render.textures = body;
                            mySnake.snake.First().render.textures = head;
                            mySnake.SwapDirection();
                            foreach (SnakePart part in mySnake.snake)
                            {
                                part.myPositions.Clear();
                            }
                            break;
                        case Portal portal:
                            Random rnd = new Random();
                            portals.Remove(portal);
                            Vector2 offset = new Vector2(0,0);
                            switch (mySnake._direction)
                            {
                                case "Left":
                                    offset = new Vector2(-10 ,0);
                                    break;
                                case "Right":
                                    offset = new Vector2(10, 0);
                                    break;
                                case "Down":
                                    offset = new Vector2(0, 10);
                                    break;
                                case "Up":
                                    offset = new Vector2(0, -10);
                                    break;
                            }
                            mySnake.snake.First().transform.position = portals[rnd.Next(0, portals.Count)].transform.position + offset;
                            portals.Add(portal);
                            break;
                    }
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
            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
                //mySnake.snake[i].transform.positon = new Vector2 { x = -10, y = -10 };
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
            mySnake._speed = 1f / GameManager.Instance.SnakeSpeed;
            speedMult = GameManager.Instance.SnakeSpeed;
            lossLife.Invoke();
            NewSnake();
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }
    public class Level2 : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> pickUPs;
        public static List<Fruit> fruits;
        public static List<Trash> trash;
        public static List<Reverse> reverse;
        public static List<Portal> portals;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public static int speedMult = GameManager.Instance.SnakeSpeed;

        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Level2()
        {

        }
        public override void Inizialize()
        {
            grid = new int[50, 50];
            pickUPs = new List<PickUP>();
            fruits = new List<Fruit>();
            trash = new List<Trash>();
            reverse = new List<Reverse>();
            portals = new List<Portal>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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


            pickUPs.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(250, 300)));
            pickUPs.Add((Reverse)FruitFactory.CreateFruit(FruitFactory.fruit.revese, new Vector2(100, 230)));
            pickUPs.Add((Reverse)FruitFactory.CreateFruit(FruitFactory.fruit.revese, new Vector2(400, 270)));

            foreach (PickUP pickUP in pickUPs)
            {
                if (pickUP is Portal portal)
                    portals.Add((Portal)portal);
            }
            for (int i = 0; i < 10; i++)
            {
                walls.Add(new Wall(new Transform(0 + i * 10, 100), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(400 + i * 10, 100), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(0 + i * 10, 400), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(400 + i * 10, 400), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(100, 100 + i * 10), "Sprites/rect4.png")); // Paredes en y
                walls.Add(new Wall(new Transform(400, 100 + i * 10), "Sprites/rect4.png")); // Paredes en y
                walls.Add(new Wall(new Transform(100, 310 + i * 10), "Sprites/rect4.png")); // Paredes en y
                walls.Add(new Wall(new Transform(400, 310 + i * 10), "Sprites/rect4.png")); // Paredes en y
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

            if (GameManager.Instance.points >= 10)
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
                if (Collision.RectRect(pickUPs.First().transform.position.x, pickUPs.First().transform.position.y, pickUPs.First().render.imgSize.x * 2, pickUPs.First().render.imgSize.y * 2,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    pickUPs.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            Fruit fruit =(Fruit)pickUPs.First();
            eatFruit += fruit.Reproducer;
            for (int i = 0; i < pickUPs.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                pickUPs[i].transform.position.x, pickUPs[i].transform.position.y, pickUPs[i].render.imgSize.x, pickUPs[i].render.imgSize.y))
                {
                    switch (pickUPs[i])
                    {
                        case Fruit fruits:
                            if (GameManager.Instance.leaveTrash)
                            {
                                Trash trash = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.transform.position.x, (int)fruits.transform.position.y));
                                trash.active = false;
                                pickUPs.Add(trash);
                            }
                            fruits.ChangeToRandomPosition();
                            foreach (PickUP item in pickUPs)
                            {
                                if(fruits!= item&& Collision.RectRect(fruits.transform.position.x, fruits.transform.position.y, fruits.render.imgSize.x, fruits.render.imgSize.y,
                                                                      item.transform.position.x, item.transform.position.y, item.render.imgSize.x, item.render.imgSize.y))
                                {
                                    fruits.ChangeToRandomPosition();
                                }
                            }
                            if (speedMult < 30)
                            {
                                speedMult++;
                                mySnake._speed = 1f / speedMult;
                            }
                            eatFruit();
                            GameManager.Instance.points++;
                            getPoints();
                            break;
                        case Trash trash:
                            KillSnake(losslifes);
                            break;
                        case Reverse reverse:
                            Texture body = mySnake.snake.Last().render.textures;
                            Texture head = mySnake.snake.First().render.textures;
                            mySnake.snake.Reverse();
                            mySnake.snake.Last().render.textures = body;
                            mySnake.snake.First().render.textures = head;
                            mySnake.SwapDirection();
                            foreach (SnakePart part in mySnake.snake)
                            {
                                part.myPositions.Clear();
                            }
                            break;
                        case Portal portal:
                            Random rnd = new Random();
                            portals.Remove(portal);
                            Vector2 offset = new Vector2(0, 0);
                            switch (mySnake._direction)
                            {
                                case "Left":
                                    offset = new Vector2(-10, 0);
                                    break;
                                case "Right":
                                    offset = new Vector2(10, 0);
                                    break;
                                case "Down":
                                    offset = new Vector2(0, 10);
                                    break;
                                case "Up":
                                    offset = new Vector2(0, -10);
                                    break;
                            }
                            mySnake.snake.First().transform.position = portals[rnd.Next(0, portals.Count)].transform.position + offset;
                            portals.Add(portal);
                            break;
                    }
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
            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
            mySnake._speed = 1f / GameManager.Instance.SnakeSpeed;
            speedMult = GameManager.Instance.SnakeSpeed;
            lossLife.Invoke();
            NewSnake();
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }
    public class Level3 : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> pickUPs;
        public static List<Portal> portals;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public static int speedMult = GameManager.Instance.SnakeSpeed;

        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Level3()
        {

        }
        public override void Inizialize()
        {
            grid = new int[50, 50];
            pickUPs = new List<PickUP>();
            portals = new List<Portal>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            pickUPs.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(250, 300)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(75, 450)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(425, 50)));

            foreach (PickUP pickUP in pickUPs)
            {
                if (pickUP is Portal portal)
                    portals.Add((Portal)portal);
            }
            for (int i = 0; i < 10; i++)
            {
                walls.Add(new Wall(new Transform(80 + i * 10, 200), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(0 + i * 10, 200), "Sprites/rect4.png")); // Paredes en x

                walls.Add(new Wall(new Transform(330 + i * 10, 310), "Sprites/rect4.png")); // Paredes en x
                walls.Add(new Wall(new Transform(430 + i * 10, 310), "Sprites/rect4.png")); // Paredes en x

                walls.Add(new Wall(new Transform(170, i * 10), "Sprites/rect4.png")); // Paredes en y
                walls.Add(new Wall(new Transform(170, 100 + i * 10), "Sprites/rect4.png")); // Paredes en y

                walls.Add(new Wall(new Transform(330, 310 + i * 10), "Sprites/rect4.png")); // Paredes en y
                walls.Add(new Wall(new Transform(330, 410 + i * 10), "Sprites/rect4.png")); // Paredes en y


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

            if (GameManager.Instance.points >= 10)
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
                if (Collision.RectRect(pickUPs.First().transform.position.x, pickUPs.First().transform.position.y, pickUPs.First().render.imgSize.x * 2, pickUPs.First().render.imgSize.y * 2,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    pickUPs.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            Fruit fruit = (Fruit)pickUPs.First();
            eatFruit += fruit.Reproducer;
            for (int i = 0; i < pickUPs.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                pickUPs[i].transform.position.x, pickUPs[i].transform.position.y, pickUPs[i].render.imgSize.x, pickUPs[i].render.imgSize.y))
                {
                    switch (pickUPs[i])
                    {
                        case Fruit fruits:
                            if (GameManager.Instance.leaveTrash)
                            {
                                Trash trash = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.transform.position.x, (int)fruits.transform.position.y));
                                trash.active = false;
                                pickUPs.Add(trash);
                            }
                            fruits.ChangeToRandomPosition();
                            foreach (PickUP item in pickUPs)
                            {
                                if (fruits != item && Collision.RectRect(fruits.transform.position.x, fruits.transform.position.y, fruits.render.imgSize.x, fruits.render.imgSize.y,
                                                                      item.transform.position.x, item.transform.position.y, item.render.imgSize.x, item.render.imgSize.y))
                                {
                                    fruits.ChangeToRandomPosition();
                                }
                            }
                            if (speedMult < 30)
                            {
                                speedMult++;
                                mySnake._speed = 1f / speedMult;
                            }
                            eatFruit();
                            GameManager.Instance.points++;
                            getPoints();
                            break;
                        case Trash trash:
                            KillSnake(losslifes);
                            break;
                        case Reverse reverse:
                            Texture body = mySnake.snake.Last().render.textures;
                            Texture head = mySnake.snake.First().render.textures;
                            mySnake.snake.Reverse();
                            mySnake.snake.Last().render.textures = body;
                            mySnake.snake.First().render.textures = head;
                            mySnake.SwapDirection();
                            foreach (SnakePart part in mySnake.snake)
                            {
                                part.myPositions.Clear();
                            }
                            break;
                        case Portal portal:
                            Random rnd = new Random();
                            portals.Remove(portal);
                            Vector2 offset = new Vector2(0, 0);
                            switch (mySnake._direction)
                            {
                                case "Left":
                                    offset = new Vector2(-10, 0);
                                    break;
                                case "Right":
                                    offset = new Vector2(10, 0);
                                    break;
                                case "Down":
                                    offset = new Vector2(0, 10);
                                    break;
                                case "Up":
                                    offset = new Vector2(0, -10);
                                    break;
                            }
                            mySnake.snake.First().transform.position = portals[rnd.Next(0, portals.Count)].transform.position + offset;
                            portals.Add(portal);
                            break;
                    }
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
            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
            mySnake._speed = 1f / GameManager.Instance.SnakeSpeed;
            speedMult = GameManager.Instance.SnakeSpeed;
            lossLife.Invoke();
            NewSnake();
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }
    public class Level4 : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> pickUPs;
        public static List<Portal> portals;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public static int speedMult = GameManager.Instance.SnakeSpeed;

        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Level4()
        {

        }
        public override void Inizialize()
        {
            grid = new int[50, 50];
            pickUPs = new List<PickUP>();
            portals = new List<Portal>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            pickUPs.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(250, 300)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(260, 110)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(270, 390)));

            foreach (PickUP pickUP in pickUPs)
            {
                if (pickUP is Portal portal)
                    portals.Add((Portal)portal);
            }
            for (int i = 0; i < 10; i++)
            {
                walls.Add(new Wall(new Transform(1 + i * 10, 100), "Sprites/rect4.png"));//palo arriba izquierda
                walls.Add(new Wall(new Transform(100 + i * 10, 100), "Sprites/rect4.png"));//palo arriba izquierda

                walls.Add(new Wall(new Transform(320 + i * 10, 100), "Sprites/rect4.png"));//palo arriba derecha
                walls.Add(new Wall(new Transform(400 + i * 10, 100), "Sprites/rect4.png"));//palo arriba derecha

                walls.Add(new Wall(new Transform(0 + i * 10, 400), "Sprites/rect4.png"));//palo abajo izquierda
                walls.Add(new Wall(new Transform(100 + i * 10, 400), "Sprites/rect4.png"));//palo abajo izquierda2

                walls.Add(new Wall(new Transform(320 + i * 10, 400), "Sprites/rect4.png"));//palo abajo derecha
                walls.Add(new Wall(new Transform(400 + i * 10, 400), "Sprites/rect4.png"));//palo abajo derecha2

                walls.Add(new Wall(new Transform(260, 130 + i * 10), "Sprites/rect4.png"));//palo vertical derecha arriba


                walls.Add(new Wall(new Transform(260, +i * 10), "Sprites/rect4.png"));//palo vertical izquierda arriba

                walls.Add(new Wall(new Transform(260, 280 + i * 10), "Sprites/rect4.png"));//palo vertical izquieda abajo


                walls.Add(new Wall(new Transform(260, 410 + i * 10), "Sprites/rect4.png"));//palo vertical derecha abajo
                walls.Add(new Wall(new Transform(260, 280 + i * 10), "Sprites/rect4.png"));//palo vertical derecha abajo
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

            if (GameManager.Instance.points >= 10)
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
                if (Collision.RectRect(pickUPs.First().transform.position.x, pickUPs.First().transform.position.y, pickUPs.First().render.imgSize.x * 2, pickUPs.First().render.imgSize.y * 2,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    pickUPs.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            Fruit fruit = (Fruit)pickUPs.First();
            eatFruit += fruit.Reproducer;
            for (int i = 0; i < pickUPs.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                pickUPs[i].transform.position.x, pickUPs[i].transform.position.y, pickUPs[i].render.imgSize.x, pickUPs[i].render.imgSize.y))
                {
                    switch (pickUPs[i])
                    {
                        case Fruit fruits:
                            if (GameManager.Instance.leaveTrash)
                            {
                                Trash trash = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.transform.position.x, (int)fruits.transform.position.y));
                                trash.active = false;
                                pickUPs.Add(trash);
                            }
                            fruits.ChangeToRandomPosition();
                            foreach (PickUP item in pickUPs)
                            {
                                if (fruits != item && Collision.RectRect(fruits.transform.position.x, fruits.transform.position.y, fruits.render.imgSize.x, fruits.render.imgSize.y,
                                                                      item.transform.position.x, item.transform.position.y, item.render.imgSize.x, item.render.imgSize.y))
                                {
                                    fruits.ChangeToRandomPosition();
                                }
                            }
                            if (speedMult < 30)
                            {
                                speedMult++;
                                mySnake._speed = 1f / speedMult;
                            }
                            eatFruit();
                            GameManager.Instance.points++;
                            getPoints();
                            break;
                        case Trash trash:
                            KillSnake(losslifes);
                            break;
                        case Reverse reverse:
                            Texture body = mySnake.snake.Last().render.textures;
                            Texture head = mySnake.snake.First().render.textures;
                            mySnake.snake.Reverse();
                            mySnake.snake.Last().render.textures = body;
                            mySnake.snake.First().render.textures = head;
                            mySnake.SwapDirection();
                            foreach (SnakePart part in mySnake.snake)
                            {
                                part.myPositions.Clear();
                            }
                            break;
                        case Portal portal:
                            Random rnd = new Random();
                            portals.Remove(portal);
                            Vector2 offset = new Vector2(0, 0);
                            switch (mySnake._direction)
                            {
                                case "Left":
                                    offset = new Vector2(-10, 0);
                                    break;
                                case "Right":
                                    offset = new Vector2(10, 0);
                                    break;
                                case "Down":
                                    offset = new Vector2(0, 10);
                                    break;
                                case "Up":
                                    offset = new Vector2(0, -10);
                                    break;
                            }
                            mySnake.snake.First().transform.position = portals[rnd.Next(0, portals.Count)].transform.position + offset;
                            portals.Add(portal);
                            break;
                    }
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
            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
            mySnake._speed = 1f / GameManager.Instance.SnakeSpeed;
            speedMult = GameManager.Instance.SnakeSpeed;
            lossLife.Invoke();
            NewSnake();
        }
        public override void Reset()
        {
            LevelsManager.Instance.CurrentLevel.updates.Clear();
            LevelsManager.Instance.CurrentLevel.updates.Clear();
        }
    }
    public class Level5 : Levels, PlayableLevel
    {
        public static Snake mySnake;
        public static int[,] grid;
        public static List<PickUP> pickUPs;
        public static List<Portal> portals;
        public static List<Wall> walls;
        public static CombatUi combatUI;
        public static int speedMult = GameManager.Instance.SnakeSpeed;

        public VoidEvent getPoint { get; set; }
        public VoidEvent lossLife { get; set; }
        public Level5()
        {

        }
        public override void Inizialize()
        {
            grid = new int[50, 50];
            pickUPs = new List<PickUP>();
            portals = new List<Portal>();
            walls = new List<Wall>();
            combatUI = new CombatUi(this);
            GameManager.Instance.lives = 3;
            GameManager.Instance.points = 0;


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            pickUPs.Add((Fruit)FruitFactory.CreateFruit(FruitFactory.fruit.apple, new Vector2(250, 300)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(450, 20)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(50, 470)));
            pickUPs.Add((Portal)FruitFactory.CreateFruit(FruitFactory.fruit.portal, new Vector2(450, 470)));
            pickUPs.Add((Reverse)FruitFactory.CreateFruit(FruitFactory.fruit.revese, new Vector2(150, 150)));
            pickUPs.Add((Reverse)FruitFactory.CreateFruit(FruitFactory.fruit.revese, new Vector2(340, 400)));

            foreach (PickUP pickUP in pickUPs)
            {
                if (pickUP is Portal portal)
                    portals.Add((Portal)portal);
            }
        for (int i = 0; i < 10; i++)
        {

            walls.Add(new Wall(new Transform(0 + i * 10, 430), "Sprites/rect4.png"));
            //walls.Add(new Wall(new Transform(200 + i * 10, 450), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(400 + i * 10, 430), "Sprites/rect4.png"));

            walls.Add(new Wall(new Transform(100 + i * 10, 350), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(290 + i * 10, 350), "Sprites/rect4.png"));

            walls.Add(new Wall(new Transform(100 + i * 10, 270), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(290 + i * 10, 270), "Sprites/rect4.png"));

            walls.Add(new Wall(new Transform(100 + i * 10, 190), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(290 + i * 10, 190), "Sprites/rect4.png"));

            walls.Add(new Wall(new Transform(100 + i * 10, 110), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(290 + i * 10, 110), "Sprites/rect4.png"));

            walls.Add(new Wall(new Transform(0 + i * 10, 50), "Sprites/rect4.png"));
            //walls.Add(new Wall(new Transform(200 + i * 10, 30), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(400 + i * 10, 50), "Sprites/rect4.png"));
        }

        for (int i = 0; i < 9; i++)
        {
            walls.Add(new Wall(new Transform(200 + i * 10, 50), "Sprites/rect4.png"));
            walls.Add(new Wall(new Transform(200 + i * 10, 430), "Sprites/rect4.png"));
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

            if (GameManager.Instance.points >= 10)
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
                if (Collision.RectRect(pickUPs.First().transform.position.x, pickUPs.First().transform.position.y, pickUPs.First().render.imgSize.x * 2, pickUPs.First().render.imgSize.y * 2,
               wall.transform.position.x, wall.transform.position.y, wall.render.imgSize.x, wall.render.imgSize.y))
                {
                    pickUPs.First().ChangeToRandomPosition();
                }
            }
            VoidEvent eatFruit = null;
            eatFruit += addSnakePiece;
            Fruit fruit = (Fruit)pickUPs.First();
            eatFruit += fruit.Reproducer;
            for (int i = 0; i < pickUPs.Count; i++)
            {
                if (Collision.RectRect(mySnake.snake.First().transform.position.x, mySnake.snake.First().transform.position.y, mySnake.snake.First().render.imgSize.x, mySnake.snake.First().render.imgSize.y,
                pickUPs[i].transform.position.x, pickUPs[i].transform.position.y, pickUPs[i].render.imgSize.x, pickUPs[i].render.imgSize.y))
                {
                    switch (pickUPs[i])
                    {
                        case Fruit fruits:
                            if (GameManager.Instance.leaveTrash)
                            {
                                Trash trash = (Trash)FruitFactory.CreateFruit(FruitFactory.fruit.rottenApple, new Vector2((int)fruits.transform.position.x, (int)fruits.transform.position.y));
                                trash.active = false;
                                pickUPs.Add(trash);
                            }
                            fruits.ChangeToRandomPosition();
                            foreach (PickUP item in pickUPs)
                            {
                                if (fruits != item && Collision.RectRect(fruits.transform.position.x, fruits.transform.position.y, fruits.render.imgSize.x, fruits.render.imgSize.y,
                                                                      item.transform.position.x, item.transform.position.y, item.render.imgSize.x, item.render.imgSize.y))
                                {
                                    fruits.ChangeToRandomPosition();
                                }
                            }
                            if (speedMult < 30)
                            {
                                speedMult++;
                                mySnake._speed = 1f / speedMult;
                            }
                            eatFruit();
                            GameManager.Instance.points++;
                            getPoints();
                            break;
                        case Trash trash:
                            KillSnake(losslifes);
                            break;
                        case Reverse reverse:
                            Texture body = mySnake.snake.Last().render.textures;
                            Texture head = mySnake.snake.First().render.textures;
                            mySnake.snake.Reverse();
                            mySnake.snake.Last().render.textures = body;
                            mySnake.snake.First().render.textures = head;
                            mySnake.SwapDirection();
                            foreach (SnakePart part in mySnake.snake)
                            {
                                part.myPositions.Clear();
                            }
                            break;
                        case Portal portal:
                            Random rnd = new Random();
                            portals.Remove(portal);
                            Vector2 offset = new Vector2(0, 0);
                            switch (mySnake._direction)
                            {
                                case "Left":
                                    offset = new Vector2(-10, 0);
                                    break;
                                case "Right":
                                    offset = new Vector2(10, 0);
                                    break;
                                case "Down":
                                    offset = new Vector2(0, 10);
                                    break;
                                case "Up":
                                    offset = new Vector2(0, -10);
                                    break;
                            }
                            mySnake.snake.First().transform.position = portals[rnd.Next(0, portals.Count)].transform.position + offset;
                            portals.Add(portal);
                            break;
                    }
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
            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
            mySnake._speed = 1f / GameManager.Instance.SnakeSpeed;
            speedMult = GameManager.Instance.SnakeSpeed;
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



            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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

            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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


            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);
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

            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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
            buttons.Add(new Button(new Transform(125, 200, 0, 5, 5f), "Sprites/Head_1.png"));
            buttons.Add(new Button(new Transform(125, 275, 0, 5, 5f), "Sprites/Head_2.png"));
            buttons.Add(new Button(new Transform(125, 350, 0, 5, 5f), "Sprites/Head_3.png"));

            _body = new Text(new Transform(285, 150, 0, 0.5f, 0.5f), "Body");
            buttons.Add(new Button(new Transform(325, 200, 0, 5, 5f), "Sprites/Body_1.png"));
            buttons.Add(new Button(new Transform(325, 275, 0, 5, 5f), "Sprites/Body_2.png"));
            buttons.Add(new Button(new Transform(325, 350, 0, 5, 5f), "Sprites/Body_3.png"));

            mySnake = new Snake(GameManager.Instance.SnakeSpeed, 5);

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

            if (mySnake.snake.First().transform.position.x < 10)
                mySnake.snake.First().transform.position.x = 490;
            if (mySnake.snake.First().transform.position.x > 490)
                mySnake.snake.First().transform.position.x = 10;
            if (mySnake.snake.First().transform.position.y < 10)
                mySnake.snake.First().transform.position.y = 490;
            if (mySnake.snake.First().transform.position.y > 490)
                mySnake.snake.First().transform.position.y = 10;
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