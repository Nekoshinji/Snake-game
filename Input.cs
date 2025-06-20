using Raylib_cs;
using static Raylib_cs.Raylib;

public static class Input
{
    public static Coordinates GetDirection(Coordinates currentDirection)
    {
        // Empêche le demi-tour direct
        if ((IsKeyPressed(KeyboardKey.W) || IsKeyPressed(KeyboardKey.Up)) && currentDirection != Coordinates.down)
            return Coordinates.up;
        else if ((IsKeyPressed(KeyboardKey.S) || IsKeyPressed(KeyboardKey.Down)) && currentDirection != Coordinates.up)
            return Coordinates.down;
        else if ((IsKeyPressed(KeyboardKey.A) || IsKeyPressed(KeyboardKey.Left)) && currentDirection != Coordinates.right)
            return Coordinates.left;
        else if ((IsKeyPressed(KeyboardKey.D) || IsKeyPressed(KeyboardKey.Right)) && currentDirection != Coordinates.left)
            return Coordinates.right;
        // Pas de changement
        return currentDirection;
    }
}