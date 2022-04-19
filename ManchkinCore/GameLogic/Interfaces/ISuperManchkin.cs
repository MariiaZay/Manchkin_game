using ManchkinCore.Enums;

namespace ManchkinCore.Interfaces;

public interface ISuperManchkin
{
    public HalfTypes HalfType { get; }
    public IClass FirstClass { get; }
    public IClass SecondClass { get; }
}