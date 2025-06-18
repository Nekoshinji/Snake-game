using System.Numerics;

public struct Coordinates
{
  public int Column;
  public int Row;

  public Coordinates(int column, int row)
  {
    this.Column = column;
    this.Row = row;
  }

  public static Coordinates[] vonNeumanNeighborhood = {
    new Coordinates(0,-1),
    new Coordinates(-1,0),
    new Coordinates(1,0),
    new Coordinates(0,1)
  };

  public static Coordinates[] moorNeighborhood = {
    new Coordinates(-1,-1),
    new Coordinates(0,-1),
    new Coordinates(1,-1),
    new Coordinates(-1,0),
    new Coordinates(1,0),
    new Coordinates(-1,1),
    new Coordinates(0,1),
    new Coordinates(1,1)
  };

  public static Coordinates zero => new(0, 0);
  public static Coordinates left => new(-1, 0);
  public static Coordinates right => new(1, 0);
  public static Coordinates up => new(0, -1);
  public static Coordinates down => new(0, 1);

  public Vector2 ToVector2 => new Vector2(Column, Row);

  public static Coordinates FromVector2(Vector2 vector) => new Coordinates((int)Math.Round(vector.X), (int)Math.Round(vector.Y)); 

  public static Coordinates operator +(Coordinates a, Coordinates b)
  {
    return new Coordinates(a.Column + b.Column, a.Row + b.Row);
  }

  public static Coordinates operator -(Coordinates a, Coordinates b)
  {
    return new Coordinates(a.Column - b.Column, a.Row - b.Row);
  }

  public static Coordinates operator *(Coordinates coord, int scalar)
  {
    return new Coordinates(coord.Column * scalar, coord.Row * scalar);
  }

    public static Coordinates operator *(int scalar, Coordinates coord)
    {
        return new Coordinates(coord.Column * scalar, coord.Row * scalar);
  }

    public static Coordinates operator -(Coordinates coord)
    {
        return new Coordinates(-coord.Column, -coord.Row);
    }

  public override bool Equals(object? obj)
  {
    if (obj is Coordinates other)
      return Column == other.Column && Row == other.Row;
    return false;
  }

  public override int GetHashCode()
  {
    return Column * 397 ^ Row;
  }

  public static bool operator ==(Coordinates a, Coordinates b)
      => a.Column == b.Column && a.Row == b.Row;

  public static bool operator !=(Coordinates a, Coordinates b)
      => !(a == b);

  public override string ToString()
  {
    return $"({Column},{Row})";
  }

  public static double Distance(Coordinates a, Coordinates b)
  {
    return Math.Sqrt(DistanceSqrt(a, b));
  }

  public static double DistanceSqrt(Coordinates a, Coordinates b)
  {
    int deltaX = a.Column - b.Column;
    int deltaY = a.Row - b.Row;
    return deltaX * deltaX + deltaY * deltaY;
  }

  public static Coordinates Random(int maxColumn, int maxRow)
  {
    Random rnd = new Random();
    int column = rnd.Next(0, maxColumn);
    int row = rnd.Next(0, maxRow);
    return new(column, row);
  }
}