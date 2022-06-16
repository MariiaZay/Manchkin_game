using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface ISuper
{
    public ISuperManchkin SuperManchkin { get; }
    public bool IsSuperManchkin { get; }
    public void BecameSuperManchkin(IClass second);
    public void BecameSuperManchkin();
    public void RefuseSuperManchkin();
}