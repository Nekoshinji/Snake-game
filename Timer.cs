public class Timer
{
    private float elapsedTime;
    private float duration;

    public Timer(float duration)
    {
        this.duration = duration;
        Reset();
    }

    public void Reset()
    {
        elapsedTime = 0f;
    }

    public void Update(float deltaTime)
    {
        elapsedTime += deltaTime;
        if (elapsedTime > duration)
        {
            elapsedTime = duration;
        }
    }

    public bool IsComplete()
    {
        return elapsedTime >= duration;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}