using Snake.Scene;

public class SceneManager
{
    private Snake.Scene.IScene? currentScene;

    public void ChangeScene(IScene newScene)
    {
        currentScene?.OnExit();
        currentScene = (Snake.Scene.IScene)newScene;
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

    internal void ChangeScene(object game)
    {
        throw new NotImplementedException();
    }
}