namespace ManchkinCore;

public class Level : ILevel
{
    public int Value { get; private set; }

    public void IncreaseLevel()
    {
        Value++;
    }

    public void ReduceLevel()
    {
        if (Value > 1) Value--;
    }
}