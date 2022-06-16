using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface IHalfBloodAble
{
    public IHulfblood? HalfBlood { get; }
    public bool IsHalfBlood { get; }
    public void BecameHalfBlood(IRace? second);
    public void BecameHalfBlood();
    public void RefuseHalfblood();
}