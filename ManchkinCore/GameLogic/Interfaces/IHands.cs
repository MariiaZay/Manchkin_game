namespace ManchkinCore.Interfaces;

public interface IHands
{
    public IStuff RightHand { get; }
    public IStuff LeftHand { get; }

    public void ChangeWeaponRightHand(IStuff? weapon);
    public void ChangeWeaponLeftHand(IStuff? weapon);
}