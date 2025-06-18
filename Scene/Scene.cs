namespace Snake.Scene
{
    public interface IScene
    {
        void Update();
        void Draw();
        void OnEnter();
        void OnExit();
    }
}
   