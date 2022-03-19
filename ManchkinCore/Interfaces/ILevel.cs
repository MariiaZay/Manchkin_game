namespace ManchkinCore.Interfaces;

public interface ILevel
{
    public int Value { get; set; }
    public void Increase();
    public void Reduce();
}