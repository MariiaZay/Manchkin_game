namespace ManchkinCore.Interfaces;

public interface ILevel
{
    public int Value { get; }
    public void Increase();
    public void Reduce();
}