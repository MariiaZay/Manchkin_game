using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Level: ILevel
{
    public int Value { get; private set; }
    public void Increase()
    {
        Value++;
    }

    public void Reduce()
    {
        Value--;
    }
}