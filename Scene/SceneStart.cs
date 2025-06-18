using Raylib_cs;

public class SceneStart
{
    public bool GameStarted { get; private set; } = false;

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space) || Raylib.IsKeyPressed(KeyboardKey.Enter))
            GameStarted = true;
    }

    public void Draw()
    {
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        // Dégradé de fond du bleu au violet
        for (int y = 0; y < screenHeight; y++)
        {
            float t = (float)y / screenHeight;
            Color color = Raylib.ColorLerp(Color.DarkBlue, Color.DarkPurple, t);
            Raylib.DrawLine(0, y, screenWidth, y, color);
        }

        // Titre avec ombre
        string title = "SNAKE";
        int titleFontSize = 80;
        int titleWidth = Raylib.MeasureText(title, titleFontSize);
        int titleX = (screenWidth - titleWidth) / 2;
        int titleY = 100;
        Raylib.DrawText(title, titleX + 4, titleY + 4, titleFontSize, Color.Black); // ombre
        Raylib.DrawText(title, titleX, titleY, titleFontSize, Color.Lime);

        // Sous-titre contrôles
        string controls = "Contrôles : ZQSD ou Flèches";
        int controlsFontSize = 28;
        int controlsWidth = Raylib.MeasureText(controls, controlsFontSize);
        int controlsX = (screenWidth - controlsWidth) / 2;
        int controlsY = titleY + 100;
        Raylib.DrawText(controls, controlsX + 2, controlsY + 2, controlsFontSize, Color.Black); // ombre
        Raylib.DrawText(controls, controlsX, controlsY, controlsFontSize, Color.SkyBlue);

        // Message pour démarrer (clignotant)
        string startMsg = "Appuie sur ESPACE ou ENTRÉE pour commencer";
        int startFontSize = 28;
        int startWidth = Raylib.MeasureText(startMsg, startFontSize);
        int startX = (screenWidth - startWidth) / 2;
        int startY = controlsY + 80;
        Color blinkColor = ((Raylib.GetTime() % 1.0) < 0.5) ? Color.Yellow : Color.LightGray;
        Raylib.DrawText(startMsg, startX + 2, startY + 2, startFontSize, Color.Black); // ombre
        Raylib.DrawText(startMsg, startX, startY, startFontSize, blinkColor);
    }
}