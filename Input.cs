using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

public class Input 
{
    public static bool IsKeyPressed(KeyboardKey key)
    {
        return Raylib.IsKeyPressed(key);
    }

    public static bool IsKeyDown(KeyboardKey key)
    {
        return Raylib.IsKeyDown(key);
    }

    public static bool IsKeyReleased(KeyboardKey key)
    {
        return Raylib.IsKeyReleased(key);
    }

    public static bool IsMouseButtonPressed(MouseButton button)
    {
        return Raylib.IsMouseButtonPressed(button);
    }

    public static Vector2 GetMousePosition()
    {
        return Raylib.GetMousePosition();
    }
}
