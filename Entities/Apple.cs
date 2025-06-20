using Raylib_cs;
using static Raylib_cs.Raylib;
public class Apple
{
    private Grid<int> grid;

    public Coordinates Position { get; private set; }

    public Apple(Grid<int> grid, Random rng)
    {
        this.grid = grid;
        Respawn(grid, rng);
    }

    public Apple(Grid<int> grid)
    {
        this.grid = grid;
    }

    public void Respawn(Grid<int> grid, Random rng)
    {
        int col = rng.Next(grid.Columns);
        int row = rng.Next(grid.Rows);
        Position = new Coordinates(col, row);
    }

    public void Draw(int cellSize, int offsetX = 0, int offsetY = 0)
    {
        int centerX = Position.Column * cellSize + cellSize / 2;
        int centerY = Position.Row * cellSize + cellSize / 2;
        int radius = cellSize / 2 - 2;

        // Ombre sous la pomme
        DrawEllipse(centerX, centerY + radius / 2, radius * 0.8f, radius * 0.3f, new Color(80, 60, 60, 80));

        // Pomme (cœur rouge foncé)
        DrawCircle(centerX, centerY, radius, new Color(180, 0, 0, 255));
        // Pomme (bord rouge clair)
        DrawCircleLines(centerX, centerY, radius, Color.Red);

        // Tige marron
        DrawLine(centerX, centerY - radius, centerX, centerY - radius - cellSize / 6, new Color(120, 80, 40, 255));

        // Feuille verte
        DrawEllipse(centerX + cellSize / 6, centerY - radius - cellSize / 10, cellSize / 6, cellSize / 10, Color.Lime);
    }
}