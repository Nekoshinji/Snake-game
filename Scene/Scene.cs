public abstract class SceneBase
{
    public bool IsFinished { get; protected set; } = false;
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public abstract void Update();
    public abstract void Draw();
}
   