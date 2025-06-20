using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Snake.Scene
{
    public class SceneGame : SceneBase
    {
        private readonly int screenWidth = 800;
        private readonly int screenHeight = 600;
        private readonly int columns = 20;
        private readonly int rows = 15;
        private readonly int cellSize;
        private readonly int offsetX;
        private readonly int offsetY;
        private readonly Grid<int> grid;
        private readonly GameSnake snake;
        private readonly Apple apple;
        private readonly Random rng = new();
        private float timer = 0f;
        private float moveDelay = 0.15f;
        private int score = 0;
        private int applesEaten = 0;

        public SceneGame()
        {
            cellSize = screenWidth / columns; // 40
            offsetX = (screenWidth - (columns * cellSize)) / 2;
            offsetY = (screenHeight - (rows * cellSize)) / 2;

            grid = new Grid<int>(columns, rows);
            snake = new GameSnake(new Coordinates(10, 10));
            apple = new Apple(grid, rng);
        }

        public override void Update()
        {
            timer += GetFrameTime();
            if (timer >= moveDelay)
            {
                timer = 0f;
                snake.Move();

                // Si le serpent mange la pomme
                if (snake.Body[0].Column == apple.Position.Column && snake.Body[0].Row == apple.Position.Row)
                {
                    snake.Grow();
                    apple.Respawn(grid, rng);
                    score++;
                    applesEaten++;
                    // Augmente la vitesse toutes les 5 pommes
                    if (applesEaten % 5 == 0)
                        moveDelay *= 0.92f; // 8% plus rapide
                }

                // Collision avec soi-même ou les murs
                if (snake.IsCollidingWithSelf() ||
                    snake.Body[0].Column < 0 || snake.Body[0].Column >= grid.Columns ||
                    snake.Body[0].Row < 0 || snake.Body[0].Row >= grid.Rows)
                {
                    IsFinished = true;
                }
            }
        }

        public override void Draw()
        {
            ClearBackground(Color.DarkGray);

            // Dessine la grille centrée
            for (int row = 0; row < grid.Rows; row++)
                for (int col = 0; col < grid.Columns; col++)
                    DrawRectangle(offsetX + col * cellSize, offsetY + row * cellSize, cellSize, cellSize, Color.Gray);

            apple.Draw(cellSize, offsetX, offsetY);
            snake.Draw(cellSize, offsetX, offsetY);

            DrawText($"Score : {score}", 10, 10, 24, Color.Yellow);
        }
    }
}