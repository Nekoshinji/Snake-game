using Raylib_cs;
using static Raylib_cs.Raylib;

public class SceneGameOver : SceneBase
{
    private GameSnake snake;
    private Font font;
    private Color color;
    public bool RestartRequested { get; private set; } = false;
    private static int highScore = 0;
    private int score;

    public SceneGameOver(GameSnake snake, int score, Font font, Color color)
    {
        this.snake = snake;
        this.score = score;
        this.font = font;
        this.color = color;

        if (score > highScore)
            highScore = score;
    }
  
    public override void Update()
    {
        if (IsKeyPressed(KeyboardKey.Enter))
        {
            RestartRequested = true;
            IsFinished = true;
        }
    }

    public override void Draw()
    {
        int screenWidth = GetScreenWidth();
        int screenHeight = GetScreenHeight();

        // Dégradé de fond
        for (int y = 0; y < screenHeight; y++)
        {
            float t = (float)y / screenHeight;
            Color color = ColorLerp(Color.DarkPurple, Color.DarkBlue, t);
            DrawLine(0, y, screenWidth, y, color);
        }

        // Titre Game Over
        string gameOver = "GAME OVER";
        int goFontSize = 64;
        int goWidth = MeasureText(gameOver, goFontSize);
        int goX = (screenWidth - goWidth) / 2;
        int goY = 100;
        DrawText(gameOver, goX + 4, goY + 4, goFontSize, Color.Black); // ombre
        DrawText(gameOver, goX, goY, goFontSize, Color.Orange);

        // Score actuel
        int currentScore = score;
        string scoreMsg = $"Score : {currentScore}";
        int scoreFontSize = 36;
        int scoreWidth = MeasureText(scoreMsg, scoreFontSize);
        int scoreX = (screenWidth - scoreWidth) / 2;
        int scoreY = goY + 90;
        DrawText(scoreMsg, scoreX + 2, scoreY + 2, scoreFontSize, Color.Black); // ombre
        DrawText(scoreMsg, scoreX, scoreY, scoreFontSize, Color.Lime);

        // High Score
        string highScoreMsg = $"High Score : {highScore}";
        int hsFontSize = 28;
        int hsWidth = MeasureText(highScoreMsg, hsFontSize);
        int hsX = (screenWidth - hsWidth) / 2;
        int hsY = scoreY + 50;
        DrawText(highScoreMsg, hsX + 2, hsY + 2, hsFontSize, Color.Black); // ombre
        DrawText(highScoreMsg, hsX, hsY, hsFontSize, Color.Yellow);

        // Instruction pour recommencer
        string restartMsg = "Appuie sur ENTRÉE pour recommencer";
        int restartFontSize = 26;
        int restartWidth = MeasureText(restartMsg, restartFontSize);
        int restartX = (screenWidth - restartWidth) / 2;
        int restartY = hsY + 70;
        Color blinkColor = ((GetTime() % 1.0) < 0.5) ? Color.SkyBlue : Color.LightGray;
        DrawText(restartMsg, restartX + 2, restartY + 2, restartFontSize, Color.Black); // ombre
        DrawText(restartMsg, restartX, restartY, restartFontSize, blinkColor);
    }

    public void Reset()
    {
        RestartRequested = false;
    }
}