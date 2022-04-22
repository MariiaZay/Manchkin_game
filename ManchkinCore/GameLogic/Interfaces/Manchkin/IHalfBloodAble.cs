namespace ManchkinCore.Interfaces;

public interface IHalfBloodAble
{
    public IHulfblood HalfBlood { get; }
    public bool IsHalfBlood();
    public void BecameHalfBlood(IRace second);
    public void BecameHalfBlood();
}