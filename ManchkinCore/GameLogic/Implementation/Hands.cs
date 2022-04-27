using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Hands :  IHands
{
    public IStuff? RightHand { get; private set; }
    public IStuff? LeftHand { get; private set; }

    public Hands()
    {
        RightHand = null;
        LeftHand = null;
    }
    
    public void TakeInRightHand(IStuff? weapon) => RightHand = weapon;


    public void TakeInLeftHand(IStuff? weapon) => LeftHand = weapon;

    public void TakeInBothHands(IStuff? weapon)
    {
        RightHand = weapon;
        LeftHand = weapon;
    }
}