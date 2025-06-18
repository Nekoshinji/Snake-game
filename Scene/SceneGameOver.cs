using Raylib_cs;
using System.Numerics;


public class SceneGameOver
{
    public GameSnake snake;
    private Font font;
    private Color color;
    public bool RestartRequested { get; private set; } = false;
    private int highScore = 0;

    public SceneGameOver(GameSnake snake, Font font, Color color)
    {
        this.snake = snake;
        this.font = font;
        this.color = color;
        this.RestartRequested = false;
    }

    public void Update(int currentScore)
    {
        if (currentScore > highScore)
            highScore = currentScore;

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            RestartRequested = true;
    }

    public void Draw(int score)
    {
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        // Dégradé de fond (du violet foncé en haut au bleu foncé en bas)
        for (int y = 0; y < screenHeight; y++)
        {
            float t = (float)y / screenHeight;
            Color color = Raylib.ColorLerp(Color.DarkPurple, Color.DarkBlue, t);
            Raylib.DrawLine(0, y, screenWidth, y, color);
        }

        // Titre Game Over avec ombre
        string gameOver = "GAME OVER";
        int goFontSize = 64;
        int goWidth = Raylib.MeasureText(gameOver, goFontSize);
        int goX = (screenWidth - goWidth) / 2;
        int goY = 100;
        Raylib.DrawText(gameOver, goX + 4, goY + 4, goFontSize, Color.Black); // ombre
        Raylib.DrawText(gameOver, goX, goY, goFontSize, Color.Orange);

        // Score actuel
        string scoreMsg = $"Score : {score}";
        int scoreFontSize = 36;
        int scoreWidth = Raylib.MeasureText(scoreMsg, scoreFontSize);
        int scoreX = (screenWidth - scoreWidth) / 2;
        int scoreY = goY + 90;
        Raylib.DrawText(scoreMsg, scoreX + 2, scoreY + 2, scoreFontSize, Color.Black); // ombre
        Raylib.DrawText(scoreMsg, scoreX, scoreY, scoreFontSize, Color.Lime);

        // High Score
        string highScoreMsg = $"High Score : {highScore}";
        int hsFontSize = 28;
        int hsWidth = Raylib.MeasureText(highScoreMsg, hsFontSize);
        int hsX = (screenWidth - hsWidth) / 2;
        int hsY = scoreY + 50;
        Raylib.DrawText(highScoreMsg, hsX + 2, hsY + 2, hsFontSize, Color.Black); // ombre
        Raylib.DrawText(highScoreMsg, hsX, hsY, hsFontSize, Color.Yellow);

        // Instruction pour recommencer (avec effet clignotant)
        string restartMsg = "Appuie sur ENTRÉE pour recommencer";
        int restartFontSize = 26;
        int restartWidth = Raylib.MeasureText(restartMsg, restartFontSize);
        int restartX = (screenWidth - restartWidth) / 2;
        int restartY = hsY + 70;
        Color blinkColor = ((Raylib.GetTime() % 1.0) < 0.5) ? Color.SkyBlue : Color.LightGray;
        Raylib.DrawText(restartMsg, restartX + 2, restartY + 2, restartFontSize, Color.Black); // ombre
        Raylib.DrawText(restartMsg, restartX, restartY, restartFontSize, blinkColor);
    }

    public void Reset()
    {
        RestartRequested = false;
    }
}