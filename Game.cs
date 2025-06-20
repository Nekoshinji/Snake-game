using Raylib_cs;
using static Raylib_cs.Raylib;

enum Scene { Game, GameOver, Start }

class Program
{
    static void Main()
    {
        // Paramètres principaux
        int screenWidth = 800;
        int screenHeight = 600;
        int cellSize = 30;

        InitWindow(screenWidth, screenHeight, "Snake");

        SceneStart startScene = new SceneStart();
        GameSnake tempSnake = new GameSnake(new Coordinates(10, 10));
        SceneGameOver gameOverScene = new SceneGameOver(tempSnake, GetFontDefault(), Color.White);
        Game game = new Game(gameOverScene, cellSize, screenWidth, screenHeight);
        SetTargetFPS(60);

        Scene currentScene = Scene.Start;

        while (!WindowShouldClose())
        {
            switch (currentScene)
            {
                case Scene.GameOver:
                    gameOverScene.Update(game.Score);
                    if (gameOverScene.RestartRequested)
                    {
                        game.Restart();
                        gameOverScene.Reset();
                        currentScene = Scene.Game;
                    }
                    break;
                case Scene.Start:
                    startScene.Update();
                    if (startScene.GameStarted)
                        currentScene = Scene.Game;
                    break;
                case Scene.Game:
                    game.Update();

                    if (game.IsGameOver)
                        currentScene = Scene.GameOver;
                    break;
            }

            BeginDrawing();
            switch (currentScene)
            {
                case Scene.GameOver:
                    gameOverScene.Draw(game.Score);
                    break;
                case Scene.Start:
                    startScene.Draw();
                    break;
                case Scene.Game:
                    game.Draw();
                    break;
            }
            EndDrawing();
        }

        CloseWindow();
    }
}

// --- CLASSE GAME ---
public class Game
{
    private Grid<int> grid;
    public GameSnake snake;
    private Apple apple;
    private Random rng = new Random();
    private float timer = 0f;
    private float moveDelay = 0.15f;
    private int score = 0;
    public int Score => score;
    public int columns;
    public int rows;
    public bool IsGameOver { get; set; } = false;
    private SceneGameOver sceneGameOver;
    private int cellSize;
    private int applesEaten = 0;

    public Game(SceneGameOver sceneGameOverInstance, int cellSize, int screenWidth, int screenHeight)
    {
        this.cellSize = cellSize;
        columns = screenWidth / cellSize;
        rows = screenHeight / cellSize;
        grid = new Grid<int>(columns, rows);
        snake = new GameSnake(new Coordinates(columns / 2, rows / 2));
        apple = new Apple(grid, rng, snake.Body);
        sceneGameOver = sceneGameOverInstance;
    }

    public void Update()
    {
        if (IsGameOver)
        {
            sceneGameOver.Update(score);
            if (sceneGameOver.RestartRequested)
            {
                Restart();
                sceneGameOver.Reset();
            }
            return;
        }

        // input
        snake.Direction = Input.GetDirection(snake.Direction);

        timer += GetFrameTime();
        if (timer >= moveDelay)
        {
            timer = 0f;

            var nextHead = new Coordinates(
                snake.Body[0].Column + snake.Direction.Column,
                snake.Body[0].Row + snake.Direction.Row
            );

            // Collision mur
            if (nextHead.Column < 0 || nextHead.Column >= grid.Columns ||
                nextHead.Row < 0 || nextHead.Row >= grid.Rows)
            {
                IsGameOver = true;
                return;
            }

            // Collision soi-même
            if (snake.Body.Skip(1).Any(part => part.Column == nextHead.Column && part.Row == nextHead.Row))
            {
                IsGameOver = true;
                return;
            }

            // Collision pomme
            if (nextHead.Column == apple.Position.Column && nextHead.Row == apple.Position.Row)
            {
                snake.Body.Insert(0, nextHead);
                apple.Respawn(grid, rng, snake.Body);
                score++;
                applesEaten++;
                if (applesEaten % 5 == 0) // Augmenter la vitesse tous les 5 pommes
                {
                    moveDelay *= 0.92f; // 8% plus rapide
                }
                return;
            }

            // Déplacement normal
            snake.Body.Insert(0, nextHead);
            snake.Body.RemoveAt(snake.Body.Count - 1);
        }
    }

    public void Draw()
    {
        if (IsGameOver)
        {
            sceneGameOver.Draw(score);
            return;
        }

        ClearBackground(Color.DarkGray);

        apple.Draw(cellSize);
        snake.Draw(cellSize);

        // Afficher le score
        DrawText($"Score : {score}", 10, 10, 32, Color.Yellow);
    }

    internal void Restart()
    {
        snake = new GameSnake(new Coordinates(columns / 2, rows / 2));
        apple.Respawn(grid, rng, snake.Body);
        score = 0;
        applesEaten = 0;
        moveDelay = 0.15f; // Remet la vitesse de base
        IsGameOver = false;
    }
}