public class Grid<T>
{
    private int columns;
    private int rows;
    private T[,] cells;

    public int Columns => columns;
    public int Rows => rows;

    public Grid(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;
        cells = new T[columns, rows];
    }

    public void SetCell(int column, int row, T value)
    {
        if (IsInBounds(column, row))
        {
            cells[column, row] = value;
        }
    }

    public T? GetCell(int column, int row)
    {
        if (IsInBounds(column, row))
        {
            return cells[column, row];
        }
        return default(T);
    }

    private bool IsInBounds(int column, int row)
    {
        return column >= 0 && column < columns && row >= 0 && row < rows;
    }
}