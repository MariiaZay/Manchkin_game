namespace ManchkinCore.Interfaces;

public interface IHands
{
    public IStuff? RightHand { get;}
    public IStuff? LeftHand { get; }

    public void TakeInRightHand(IStuff? weapon);
    public void TakeInLeftHand(IStuff? weapon);
    public void TakeInBothHands(IStuff? weapon);
}