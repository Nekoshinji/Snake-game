
using Raylib_cs;
using static Raylib_cs.Raylib;

// Add the necessary using directives for your project types
// using YourProjectNamespace.Models; // Uncomment and adjust as needed

public class SceneGame
{
    private Grid<int> grid;
    private GameSnake snake;
    private Apple apple;
    private Random rng = new Random();

    public SceneGame()
    {
        grid = new Grid<int>(20, 20);
        snake = new GameSnake(new Coordinates(10, 10));
        apple = new Apple(grid, rng);
    }

    public void Update()
    {
        // À compléter : gestion des entrées clavier pour changer la direction du serpent

        snake.Move();

        // Si le serpent mange la pomme
        if (snake.Body[0].Column == apple.Position.Column && snake.Body[0].Row == apple.Position.Row)
        {
            snake.Grow();
            apple.Respawn(grid, rng);
        }

        // Collision avec soi-même ou les murs
        if (snake.IsCollidingWithSelf() ||
            snake.Body[0].Column < 0 || snake.Body[0].Column >= grid.Columns ||
            snake.Body[0].Row < 0 || snake.Body[0].Row >= grid.Rows)
        {
            // Game Over : reset the game
            snake = new GameSnake(new Coordinates(10, 10));
            apple.Respawn(grid, rng);
        }
    }

    public void Draw()
    {
        // Draw grid
        for (int row = 0; row < grid.Rows; row++)
        {
            for (int col = 0; col < grid.Columns; col++)
            {
                DrawRectangle(col * 20, row * 20, 20, 20, Color.DarkGray);
            }
        }

        // Draw snake
        foreach (var part in snake.Body)
        {
            DrawRectangle(part.Column * 20, part.Row * 20, 20, 20, Color.Green);
        }

        // Draw apple
        DrawCircle(apple.Position.Column * 20 + 10, apple.Position.Row * 20 + 10, 10, Color.Red);
    }
}