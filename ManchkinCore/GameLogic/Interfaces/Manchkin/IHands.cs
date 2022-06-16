using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface IHands
{
    public IStuff? RightHand { get; }
    public IStuff? LeftHand { get; }

    public void TakeInRightHand(IStuff weapon);
    public void DropFromRightHand();
    public void TakeInLeftHand(IStuff weapon);
    public void DropFromLeftHand();
    public void TakeInBothHands(IStuff weapon);
    public void DropFromBothHands();
}