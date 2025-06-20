public interface IScene
{
    void OnEnter();
    void OnExit();
    void Update();
    void Draw();
}

public class SceneManager
{
    private IScene? currentScene;

    public void ChangeScene(IScene newScene)
    {
        currentScene?.OnExit();
        currentScene = newScene;
        currentScene.OnEnter();
    }

    public void Update()
    {
        currentScene?.Update();
    }

    public void Draw()
    {
        currentScene?.Draw();
    }
}