using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Mercenary : IMercenary
{
    public IStuff? Item { get; private set; }
    public int Damage => 1;

    public Mercenary() => Item = null;
    public Mercenary(IStuff? stuff) => Item = stuff;
    
    public void ChangeEquipment(IStuff? stuff) =>Item = stuff;
}