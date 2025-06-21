using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace SnakeGame
{

public class Game
{
    private readonly Grid<int> grid;
    public GameSnake snake; 
    private readonly Apple apple;
    private readonly Random rng = new();
    private readonly int cellSize = 25;
    private float timer = 0f;
    private float moveDelay = 0.15f;
    private int score = 0;
    private int applesEaten = 0;
    public int Score => score;
    public int columns;
    public int rows;
    public bool IsGameOver { get; set; } = false;
    private readonly SceneGameOver sceneGameOver;

    public Game(SceneGameOver sceneGameOverInstance)
    {
        int screenWidth = 800;
        int screenHeight = 600;
        cellSize = screenWidth / 25;
        // Offset pour centrer la grille à l'écran
        int offsetX = (screenWidth - (columns * cellSize)) / 2;
        int offsetY = (screenHeight - (rows * cellSize)) / 2;
        columns = 25;
        rows = 20;
        grid = new Grid<int>(columns, rows);
        snake = new GameSnake(new Coordinates(10, 10));
        apple = new Apple(grid, rng);
        sceneGameOver = sceneGameOverInstance;
    }

    public void Update()
    {
        if (IsGameOver)
        {
            sceneGameOver.Update();
            if (sceneGameOver.RestartRequested)
            {
                Restart();
                sceneGameOver.Reset();
            }
            return;
        }

        HandleInput();

        timer += GetFrameTime();
        if (timer >= moveDelay)
        {
            timer = 0f;

            var nextHead = new Coordinates(
                snake.Body[0].Column + snake.Direction.Column,
                snake.Body[0].Row + snake.Direction.Row
            );

            // Collision mur
            if (nextHead.Column < 0 || nextHead.Column >= columns ||
                nextHead.Row < 0 || nextHead.Row >= rows)
            {
                IsGameOver = true;
                return;
            }

            // Collision soi-même
            if (snake.Body.Contains(nextHead))
            {
                IsGameOver = true;
                return;
            }

            // Collision pomme
            if (nextHead.Column == apple.Position.Column && nextHead.Row == apple.Position.Row)
            {
                snake.Body.Insert(0, nextHead);
                apple.Respawn(grid, rng);
                score++;
                applesEaten++;
                if (applesEaten % 2 == 0)
                    moveDelay *= 0.92f; // accélère de 8% toutes les 2 pommes
                return;
            }

            // Déplacement normal
            snake.Body.Insert(0, nextHead);
            snake.Body.RemoveAt(snake.Body.Count - 1);
        }
    }

    private void HandleInput()
    {
        // Utilisation du module Input.cs pour gérer les entrées
        var direction = Input.GetDirection(snake.Direction);
        if (direction != Coordinates.zero && direction != -snake.Direction)
        {
            snake.Direction = direction;
        }
    }

    public void Draw()
    {
        if (IsGameOver)
        {
            sceneGameOver.Draw();
            return;
        }

        ClearBackground(DarkGray);
       
        apple.Draw(cellSize);
        snake.Draw(cellSize);

        // Afficher le score
        DrawText($"Score : {score}", 10, 10, 24, Yellow);
    
    }

    internal void Restart()
    {
        snake = new GameSnake(new Coordinates(columns / 2, rows / 2));
        apple.Respawn(grid, rng);
        score = 0;
        applesEaten = 0; // Réinitialiser le compteur de pommes mangées
        IsGameOver = false;
    }
}

enum Scene { Game, GameOver, Start }

public class SceneGame : SceneBase
{
    public GameSnake Snake => game.snake;
    public int Score => game.Score;
    private readonly Game game;

    public SceneGame()
    {
        game = new Game(new SceneGameOver(new GameSnake(new Coordinates(10, 10)), 0, GetFontDefault(), White));
    }

    public override void Update()
    {
        game.Update();
        if (game.IsGameOver)
            IsFinished = true;
    }

    public override void Draw()
    {
        game.Draw();
    }
}
class Program
{
    static void Main()
    {
        int screenWidth = 800;
        int screenHeight = 600;
        int columns = 25;
        int rows = 20;
        int cellSize = screenWidth / columns;
        int cellHeight = screenHeight / rows;
        
        InitWindow(screenWidth, screenHeight, "Snake");
        SetTargetFPS(60);

        SceneBase currentScene = new SceneStart();

        while (!WindowShouldClose())
        {
            currentScene.Update();

            // Transition de scène
            if (currentScene.IsFinished)
            {
                if (currentScene is SceneStart)
                    currentScene = new SceneGame();
                else if (currentScene is SceneGame sceneGame)
                    currentScene = new SceneGameOver(sceneGame.Snake, sceneGame.Score, GetFontDefault(), White);
                else if (currentScene is SceneGameOver)
                    currentScene = new SceneGame();
            }

            BeginDrawing();
            currentScene.Draw();
            EndDrawing();
        }

        CloseWindow();
    }
}
}

