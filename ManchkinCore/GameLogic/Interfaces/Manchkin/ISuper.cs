namespace ManchkinCore.Interfaces;

public interface ISuper
{
    public ISuperManchkin SuperManchkin { get; }
    public bool IsSuperManchkin { get; }
    public void BecameSuperManchkin(IClass second);
    public void BecameSuperManchkin();
    public void RefuseSuperManchkin();
}