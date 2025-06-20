using Raylib_cs;
using static Raylib_cs.Raylib;


public class GameSnake
{
    public int Score { get; set; }
    public List<Coordinates> Body { get; private set; }
    public Coordinates Direction { get; set; }

    public GameSnake(Coordinates startPosition)
    {
        Body = new List<Coordinates> { startPosition };
        Direction = Coordinates.right;
    }

    public void Move()
    {
        var newHead = new Coordinates(
            Body[0].Column + Direction.Column,
            Body[0].Row + Direction.Row
        );
        Body.Insert(0, newHead);
        Body.RemoveAt(Body.Count - 1);
    }

    public void Grow()
    {
        Body.Add(Body[Body.Count - 1]);
    }

    public bool IsCollidingWithSelf()
    {
        var head = Body[0];
        for (int i = 1; i < Body.Count; i++)
        {
            if (Body[i].Column == head.Column && Body[i].Row == head.Row)
                return true;
        }
        return false;
    }

    public void Draw()
    {
        int cellSize = 40;

        // Corps (du plus foncé au plus clair)
        for (int i = Body.Count - 1; i > 0; i--)
        {
            var part = Body[i];
            Color bodyColor = (i % 2 == 0) ? Color.DarkGreen : Color.Green;
            DrawRectangleRounded(
                new Rectangle(part.Column * cellSize, part.Row * cellSize, cellSize, cellSize),
                0.5f, 6, bodyColor
            );
        }

        // Tête (plus claire et arrondie)
        var head = Body[0];
        DrawRectangleRounded(
            new Rectangle(head.Column * cellSize, head.Row * cellSize, cellSize, cellSize),
            0.8f, 8, Color.Lime
        );

        // Yeux
        int eyeRadius = cellSize / 8;
        int offsetX = cellSize / 4;
        int offsetY = cellSize / 4;
        // Positionne les yeux selon la direction
        int eye1X = head.Column * cellSize + offsetX;
        int eye2X = head.Column * cellSize + cellSize - offsetX - eyeRadius * 2;
        int eyeY = head.Row * cellSize + offsetY;

        DrawCircle(eye1X, eyeY, eyeRadius, Color.White);
        DrawCircle(eye2X, eyeY, eyeRadius, Color.White);
        DrawCircle(eye1X, eyeY, eyeRadius / 2, Color.Black);
        DrawCircle(eye2X, eyeY, eyeRadius / 2, Color.Black);
    }

    // Ajoutez ici les propriétés et méthodes de GameSnake

    public void Draw(int cellSize, int offsetX = 0, int offsetY = 0)
    {
        // Dégradé du corps (du foncé à la tête)
        for (int i = Body.Count - 1; i > 0; i--)
        {
            var part = Body[i];
            float t = (float)i / Body.Count;
            Color bodyColor = ColorLerp(Color.DarkGreen, Color.Lime, 1 - t);

            // Segments allongés
            DrawRectangleRounded(
                new Rectangle(
                    offsetX + part.Column * cellSize + cellSize * 0.15f,
                    offsetY + part.Row * cellSize + cellSize * 0.25f,
                    cellSize * 0.7f,
                    cellSize * 0.5f
                ),
                0.2f, 8, bodyColor
            );
        }

        // Tête (plus large, arrondie, couleur vive)
        var head = Body[0];
        DrawRectangleRounded(
            new Rectangle(
                offsetX + head.Column * cellSize + cellSize * 0.05f,
                offsetY + head.Row * cellSize + cellSize * 0.1f,
                cellSize * 0.9f,
                cellSize * 0.8f
            ),
            0.5f, 8, Color.Lime
        );

        // Yeux rapprochés
        int eyeRadius = cellSize / 10;
        int eyeOffsetX = cellSize / 4;
        int eyeOffsetY = cellSize / 4;
        int eyeY = (int)(head.Row * cellSize + cellSize * 0.3f);
        int eye1X = (int)(head.Column * cellSize + cellSize * 0.4f);
        int eye2X = (int)(head.Column * cellSize + cellSize * 0.6f);
        DrawCircle(eye1X, eyeY, eyeRadius, Color.White);
        DrawCircle(eye2X, eyeY, eyeRadius, Color.White);
        DrawCircle(eye1X, eyeY, eyeRadius / 2, Color.Black);
        DrawCircle(eye2X, eyeY, eyeRadius / 2, Color.Black);

        // Langue en V
        int tongueX = (int)(head.Column * cellSize + cellSize * 0.5f);
        int tongueY = (int)(head.Row * cellSize + cellSize * 0.95f);
        DrawLine(tongueX, tongueY, tongueX - eyeRadius, tongueY + eyeRadius * 2, Color.Red);
        DrawLine(tongueX, tongueY, tongueX + eyeRadius, tongueY + eyeRadius * 2, Color.Red);
    }
}

