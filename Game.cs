using Raylib_cs;
using static Raylib_cs.Raylib;


public class Game
{
    private Grid<int> grid;
    public GameSnake snake; 
    private Apple apple;
    private Random rng = new Random();
    private int cellSize = 30  ;
    private float timer = 0f;
    private float moveDelay = 0.15f;
    private int score = 0; // Ajout d'un score
    public int Score => score;
    public int columns; // 20
    public int rows;    // 15
    public bool IsGameOver { get; set; } = false;
    private SceneGameOver sceneGameOver;

    public Game(SceneGameOver sceneGameOverInstance)
    {
        columns = 800 / cellSize;
        rows = 600 / cellSize;
        grid = new Grid<int>(columns, rows);
        snake = new GameSnake(new Coordinates(10, 10));
        apple = new Apple(grid, rng);
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

        HandleInput();

        timer += Raylib.GetFrameTime();
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
                return;
            }

            // Déplacement normal
            snake.Body.Insert(0, nextHead);
            snake.Body.RemoveAt(snake.Body.Count - 1);
        }
    }

    private void HandleInput()
    {
        // Utilisez W, A, S, D pour la compatibilité QWERTY/AZERTY
        if (IsKeyPressed(KeyboardKey.W) && snake.Direction != Coordinates.down)
            snake.Direction = Coordinates.up;
        else if (IsKeyPressed(KeyboardKey.S) && snake.Direction != Coordinates.up)
            snake.Direction = Coordinates.down;
        else if (IsKeyPressed(KeyboardKey.A) && snake.Direction != Coordinates.right)
            snake.Direction = Coordinates.left;
        else if (IsKeyPressed(KeyboardKey.D) && snake.Direction != Coordinates.left)
            snake.Direction = Coordinates.right;
    }

    public void Draw()
    {
        if (IsGameOver)
        {
            sceneGameOver.Draw(score);
            return;
        }

        Raylib.ClearBackground(Color.DarkGray);

       
        apple.Draw(cellSize);
        snake.Draw(cellSize);

        // Afficher le score
        Raylib.DrawText($"Score : {score}", 10, 10, 24, Color.Yellow);
    
    }

    internal void Restart()
    {
        snake = new GameSnake(new Coordinates(columns / 2, rows / 2));
        apple.Respawn(grid, rng);
        score = 0;
        IsGameOver = false;
    }
}

enum Scene { Game, GameOver, Start }

class Program
{
    static void Main()
    {
        int screenWidth = 800;
        int screenHeight = 600;
        InitWindow(screenWidth, screenHeight, "Snake");
        SceneStart startScene = new SceneStart();
        // Créez d'abord un serpent temporaire pour initialiser SceneGameOver
        GameSnake tempSnake = new GameSnake(new Coordinates(10, 10));
        SceneGameOver gameOverScene = new SceneGameOver(tempSnake, GetFontDefault(), Color.White);
        Game game = new Game(gameOverScene);
        // Mettez à jour le serpent de gameOverScene pour qu'il corresponde à celui du jeu
        gameOverScene.snake = game.snake;
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

                    // Vérifie si le serpent touche les bords de l'écran ou se mord lui-même
                    if (game.snake.Body[0].Column < 0 || game.snake.Body[0].Column >= game.columns ||
                        game.snake.Body[0].Row < 0 || game.snake.Body[0].Row >= game.rows ||
                        game.snake.Body.Skip(1).Any(part => part.Column == game.snake.Body[0].Column && part.Row == game.snake.Body[0].Row))
                    {
                        game.IsGameOver = true;
                        currentScene = Scene.GameOver;
                    }
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

