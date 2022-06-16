using ManchkinCore.GameLogic.Interfaces.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Implementation.Manchkin;

public class Hands : IHands
{
    public IStuff? RightHand { get; private set; }
    public IStuff? LeftHand { get; private set; }

    public Hands()
    {
        RightHand = null;
        LeftHand = null;
    }

    public void TakeInRightHand(IStuff weapon) => RightHand = weapon;

    public void DropFromRightHand()
    {
        RightHand = null;
    }

    public void TakeInLeftHand(IStuff weapon) => LeftHand = weapon;

    public void DropFromLeftHand()
    {
        LeftHand = null;
    }

    public void TakeInBothHands(IStuff weapon)
    {
        RightHand = weapon;
        LeftHand = weapon;
    }

    public void DropFromBothHands()
    {
        DropFromLeftHand();
        DropFromRightHand();
    }
}