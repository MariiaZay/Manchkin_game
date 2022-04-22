namespace ManchkinCore.Interfaces;

public interface ISuper
{
    public ISuperManchkin SuperManchkin { get; }
    public bool IsSuperManchkin();
    public void BecameSuperManchkin(IClass second);
    public void BecameSuperManchkin();
}